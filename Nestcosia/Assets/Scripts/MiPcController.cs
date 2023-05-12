using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MiPcController : MonoBehaviour
{


    public Rigidbody body;
    public float moveSpeed;
    public PlayerInput playerInput;


    //public InputAction PlayerControls;

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
        playerInput.UI.Disable();


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputDirection = playerInput.Gameplay.Move.ReadValue<Vector2>();
        Debug.Log("X: " + inputDirection.x + "Y: " + inputDirection.y);
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector3(inputDirection.x * moveSpeed, 0, inputDirection.y * moveSpeed);
        //body.AddForce(inputDirection * movForce, ForceMode.Force);
    }
}
