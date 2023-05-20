using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MiPcController : MonoBehaviour
{

    //public Rigidbody body;
    public float moveSpeed;
    public PlayerInput playerInput;
    public Animator animatorController;




    //public InputAction PlayerControls;
    public bool movementPressed;
    Vector2 inputDirection = Vector2.zero;

    public void OnEnable()
    {
        //PlayerControls.Enable();
    }

    public void OnDisable()
    {
        //PlayerControls.Disable();
    }


    private void Awake()
    {
        //Input Initialization
        playerInput = new PlayerInput();
        playerInput.Gameplay.Enable();

        playerInput.Gameplay.Move.performed += ctx => {

            inputDirection = ctx.ReadValue<Vector2>();
            movementPressed = inputDirection.x != 0 || inputDirection.y != 0;
        };

        //playerInput.UI.Disable();


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //inputDirection = playerInput.Gameplay.Move.ReadValue<Vector2>();
        //Debug.Log("X: " + inputDirection.x + "Y: " + inputDirection.y);

        HandleRotation();
    }

    public void HandleMovement()
    {

    }

    public void HandleRotation()
    {
        Vector3 currentPos = transform.position;

        Vector3 newPosition = new Vector3(inputDirection.x, 0, inputDirection.y);

        Vector3 posToLookAt = currentPos + newPosition;

        transform.LookAt(posToLookAt);
    }

    private void FixedUpdate()
    {
        //body.velocity = new Vector3(inputDirection.x * moveSpeed, 0, inputDirection.y * moveSpeed);
        //body.AddForce(inputDirection * movForce, ForceMode.Force);
    }
}
