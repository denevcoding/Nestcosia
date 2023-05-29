using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interacter : MonoBehaviour
{
    public Elevator elevator;
    public BatteryReceiver receiver;
    public List<Collectible> batteries = new List<Collectible>();
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
            receiver.AddCollectibles(batteries);
            Debug.Log("Solto baterias");
        }
       


        if (elevator)
        {
            elevator.ActivateElevator();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BatteryReceiver>()) 
        {
            receiver = other.gameObject.GetComponent<BatteryReceiver>();
            Debug.Log("Estoy en un receiver");
        }

        if (other.gameObject.GetComponent<Elevator>())
        {
            elevator = other.GetComponent<Elevator>();
            Debug.Log("Estoy en un elevador");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<BatteryReceiver>())
        {
            receiver = null;
        }

        if (other.gameObject.GetComponent<Elevator>())
        {
            elevator = null;
        }
    }

    public void AddBattery(Collectible colectible)
    {
        if (!batteries.Contains(colectible))
        {
            batteries.Add(colectible);
        }
    }

}
