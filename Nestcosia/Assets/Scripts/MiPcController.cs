using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MiPcController : MonoBehaviour
{


    public Rigidbody body;
    public float moveSpeed;
    public InputAction PlayerControls;

    Vector2 inputDirection = Vector2.zero;

    public void OnEnable()
    {
        PlayerControls.Enable();
    }

    public void OnDisable()
    {
        PlayerControls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputDirection = PlayerControls.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(inputDirection.x * moveSpeed, inputDirection.y * moveSpeed);
        //body.AddForce(inputDirection * movForce, ForceMode.Force);
    }
}
