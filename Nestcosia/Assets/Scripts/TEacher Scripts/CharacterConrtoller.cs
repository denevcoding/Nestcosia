using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterConrtoller : MonoBehaviour
{

    public GameManager gameManager;

    private Rigidbody rbCharacter;//Refernce to my own Rigid Bodie

    public bool sideScroller; // the game is 2.5D - Top Down?

    public Animator animController;

    //Gameplay Variables using Old Input system
    float h;
    float v;


    //With new input system
    PlayerInput playerInput;



    //Movement
    public float maxSpeed; //Mas speed of the character
    private float curAccel, curDecel;
    //Ground
    public float accel;
    public float decel;
    //Air
    public float airAccel;  
    public float airDecel;

    //Movement Vectors for animator and operations
    [HideInInspector] public Vector3 m_currentSpeed;
    [HideInInspector] public float m_DistanceToTarget;

    //Camera Interaction
    public Transform Cam;


    //Rotations
    private Quaternion screenMovementSpace = Quaternion.identity;
    private float curRotateSpeed;

    private Vector3  moveDirection, screenMovementForward, screenMovementRight;
    private Vector3 inputDirection;

    public float rotateSpeed;//Ground rotation speed;
    public float airRotateSpeed;

    private float slope;
    public float slopeLimit;
    public float slideAmount; // Velocity to feel down a slide


    public float movingPlatformFriction; //Friction of the object like moving platforms 
    private Vector3 movingObjSpeed; // Id we are on a platform



    //FloorCheks Floor Detection
    public Transform floorChecks;
    public Transform[] floorCheckers;
    private bool grounded;
    public LayerMask groundedLayerMask; //We can add any layers to this mask and combine to evaluate if we are touching players or something else to restore jump


    private void Awake()
    {
        //groundedLayerMask = LayerMask.GetMask("Floor");
        rbCharacter = GetComponent<Rigidbody>();
        InitFloorChecks();

        //nimController = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInput = new PlayerInput();
        playerInput.Gameplay.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = IsGrounded(); //Set the animator from here or inside the function
        CalculateInputs(); //Update and listen inputs and calculate vector based on sidescroller bool
       
    }

    private void FixedUpdate()
    {
        MoveTo(moveDirection, curAccel, 0.05f, true);
        //RotateVelocity(rotateSpeed, true);

        if (rotateSpeed != 0 && inputDirection.magnitude != 0)
            RotateToDirection(inputDirection, curRotateSpeed, true);


        ManageSpeed(curDecel, maxSpeed + movingObjSpeed.magnitude, true);
    }

    private void InitFloorChecks()
    {
        //Create a single floorcheck in centre of object, if none are assigned
        if (!floorChecks)
        {
            floorChecks = new GameObject().transform;
            floorChecks.name = "FloorChecks";
            floorChecks.parent = transform;
            floorChecks.position = transform.position;
            GameObject check = new GameObject();
            check.name = "Check1";
            check.transform.parent = floorChecks;
            check.transform.position = transform.position;
            Debug.LogWarning("No 'floorChecks' assigned to PlayerMove script, so a single floorcheck has been created", floorChecks);
        }

        //gets child objects of floorcheckers, and puts them in an array
        //later these are used to raycast downward and see if we are on the ground
        floorCheckers = new Transform[floorChecks.childCount];
        for (int i = 0; i < floorCheckers.Length; i++)
        {
            floorCheckers[i] = floorChecks.GetChild(i);
        }
    }



    void CalculateInputs()
    {
        rbCharacter.WakeUp();

        //adjust movement values if we're in the air or on the ground
        curAccel = (grounded) ? accel : airAccel;
        curDecel = (grounded) ? decel : airDecel;
        curRotateSpeed = (grounded) ? rotateSpeed : airRotateSpeed;

        //get movement axis relative to camera
        if (Cam != null)
        {
            //get movement axis relative to camera
            screenMovementForward = screenMovementSpace * Cam.up;
            screenMovementRight = screenMovementSpace * Cam.right;
        }
        else
        {
            screenMovementForward = screenMovementSpace * Vector3.forward;
            screenMovementRight = screenMovementSpace * Vector3.right;
        }

        //h = playerInput.Gameplay.Move.ReadValue<Vector2>().x;
        //v = playerInput.Gameplay.Move.ReadValue<Vector2>().y;
        Debug.Log(h + "," + v);

        //inputDirection = playerInput.Gameplay.Move.ReadValue<Vector2>();

        //only apply vertical input to movemement, if player is not sidescroller
        if (!sideScroller)
            inputDirection = (screenMovementForward * playerInput.Gameplay.Move.ReadValue<Vector2>().y) + (screenMovementRight * playerInput.Gameplay.Move.ReadValue<Vector2>().x);
        else
            inputDirection = Vector3.right * h;

        moveDirection = transform.position + inputDirection;

        if (inputDirection.magnitude <= 0)
        {
            animController.SetBool("isMoving", false);
            
        }
        else
        {
            animController.SetBool("isMoving", true);
        }

        Debug.DrawRay(transform.position, moveDirection, Color.green);

        //inputDirection = moveDirection - transform.position;
        Debug.DrawRay(transform.position, inputDirection, Color.red);

        animController.SetFloat("movSpeed", m_DistanceToTarget);
    }


    public bool MoveTo(Vector3 destination, float acceleration, float stopDistance, bool ignoreY) 
    {
        Vector3 relativePos = (destination - transform.position);
        if (ignoreY)
            relativePos.y = 0;

        m_DistanceToTarget = relativePos.magnitude;
        if (m_DistanceToTarget <= stopDistance)
            return true; //Deje d moverme y llegue al destino del input

        else
           rbCharacter.AddForce(relativePos * acceleration * Time.deltaTime, ForceMode.VelocityChange);

        return false; // Keep moving, input or IA are applying movement

    }

    public void ManageSpeed(float deceleration, float maxSpeed, bool ignoreY) 
    {//Controls the speed of our character on static floor, moving floors dynamicly friction 
        m_currentSpeed = rbCharacter.velocity;

        

        if (ignoreY)//if We want to handle the Y position manually
            m_currentSpeed.y = 0;

        if (m_currentSpeed.magnitude > 0)
        {           
            if (rbCharacter.velocity.magnitude > maxSpeed)
                rbCharacter.AddForce((m_currentSpeed * -1) * deceleration * Time.deltaTime, ForceMode.VelocityChange);
        }

    }


    public void RotateVelocity(float turnSpeed, bool ignoreY)
    {
        Vector3 direction;
        if (ignoreY)
        {
            direction = new Vector3(rbCharacter.velocity.x, 0f, rbCharacter.velocity.z);
        }
        else
        {
            direction = rbCharacter.velocity;
        }

        if (direction.magnitude > 0.1f)
        {
            Quaternion dirQ = Quaternion.LookRotation(direction);
            Quaternion slerp = Quaternion.Slerp(transform.rotation, dirQ, direction.magnitude * turnSpeed * Time.deltaTime);
            rbCharacter.MoveRotation(slerp);
        }
    }


    public void RotateToDirection(Vector3 lookDir, float turnSpeed, bool ignoreY)
    {

        Vector3 characterPos = transform.position;
        if (ignoreY)
        {
            characterPos.y = 0;
            lookDir.y = 0;
        }
        //Un n�mero mayor o menor a este har� que mire hacia el otro lado al voltear
        //lookDir.z = 0;


        turnSpeed *= 1.6f;
        Quaternion dirQ = Quaternion.LookRotation(lookDir);
        Quaternion slerp = Quaternion.Slerp(transform.rotation, dirQ, turnSpeed * Time.deltaTime);
        rbCharacter.MoveRotation(slerp);
    }

    public bool IsGrounded()
    {
        //get distance to ground, from centre of collider (where floorcheckers should be)
        float dist = GetComponent<Collider>().bounds.extents.y;

        foreach (Transform check in floorCheckers)
        {
            RaycastHit hit;

            if (Physics.Raycast(check.position, Vector3.down, out hit, dist + 0.05f, groundedLayerMask))
            {
                Debug.DrawRay(check.position, Vector3.down * (dist + 0.05f), Color.cyan, 0.05f);
                if (!hit.transform.GetComponent<Collider>().isTrigger)
                { //The object has a real collider not Trigger

                    slope = Vector3.Angle(hit.normal, Vector3.up); //slope control

                    //slide down slopes
                    if (slope > slopeLimit && hit.transform.tag != "Pusheable")
                    {
                        Vector3 slide = new Vector3(0f, -slideAmount, 0f);
                        rbCharacter.AddForce(slide, ForceMode.Force);
                    }

                    //Moving with platforms
                    //moving platforms
                    if (hit.transform.tag == "MovingPlatform" || hit.transform.tag == "Pushable")
                    {
                        movingObjSpeed = hit.transform.GetComponent<Rigidbody>().velocity;
                        movingObjSpeed.y = 0f;
                        //9.5f is a magic number, if youre not moving properly on platforms, experiment with this number
                        rbCharacter.AddForce(movingObjSpeed * movingPlatformFriction * Time.deltaTime, ForceMode.VelocityChange);
                    }
                    else
                    {
                        movingObjSpeed = Vector3.zero;
                    }
                    Debug.Log("Am touching the floor");
                    //yes our feet are on something    
                    return true;

                }
            }
        }

        movingObjSpeed = Vector3.zero;
        Debug.Log("Am Not touching the floor");
        //no none of the floorchecks hit anything, we must be in the air (or water)
        return false;
    }







}
