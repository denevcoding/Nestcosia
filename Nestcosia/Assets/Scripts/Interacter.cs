using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interacter : MonoBehaviour
{
    public BatteryReceiver receiver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Interact(InputAction.CallbackContext context)
    {
        if (receiver)
        {
            Debug.Log("Solto baterias");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BatteryReceiver>()) 
        {
            receiver = other.gameObject.GetComponent<BatteryReceiver>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<BatteryReceiver>())
        {
            receiver = null;
        }
    }

}
