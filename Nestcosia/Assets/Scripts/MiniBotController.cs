using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBotController : MonoBehaviour
{

    public bool useGravity;
    public int lifes;
    public float movForce;

    Vector3 inputDirection;

    float x, y;

    public Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
    

       
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        inputDirection = new Vector3(x, 0f, y);
        
    }

    private void FixedUpdate()
    {
        body.AddForce(inputDirection * movForce, ForceMode.Force);
    }
}
