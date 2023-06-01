using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator OpenCloselittledoor;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entro trigger " + other.name);
        if (other.CompareTag("Player"))
        {
            OpenCloselittledoor.SetBool("DoorOpening", true);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OpenCloselittledoor.SetBool("DoorOpening", false);
        }
    }


}
