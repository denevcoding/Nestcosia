using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantoxController : MonoBehaviour
{
    CharacterController controller;
    PlayerInput playerInput;

    public float moveSpeed;
    public Transform cam;

    //private Vector3 screenMovementSpace = Quaternion.identity;
    //private Vector3 screenMovementForward, screenMovementRight;
    private Vector3 inputDirection;

    //Gameplay Variables using Old Input system
    float h;
    float v;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerInput = new PlayerInput();
        playerInput.Gameplay.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        ////get movement axis relative to camera
        //screenMovementForward = screen * cam.up;
        //screenMovementRight = screenMovementSpace * Cam.right;

        inputDirection = (cam.up * playerInput.Gameplay.Move.ReadValue<Vector2>().y) + (cam.right * playerInput.Gameplay.Move.ReadValue<Vector2>().x);
        //Debug.Log(inputDirection);

        controller.SimpleMove((inputDirection * moveSpeed));


   
    }

    //public void HandleRotation(Vector3 lookDir, float turnSpeed)
    //{
    //    //turnSpeed *= 1.6f;
    //    Quaternion dirQ = Quaternion.LookRotation(lookDir);
    //    Quaternion slerp = Quaternion.Slerp(transform.rotation, dirQ, turnSpeed * Time.deltaTime);
    //    //transform =slerp;
    //}

    public void Transport(Vector3 newPos)
    {
        controller.enabled = false;
        transform.position = newPos;
        controller.enabled = true;
    }
}
