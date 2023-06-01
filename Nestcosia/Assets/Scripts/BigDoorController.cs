using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigDoorController : MonoBehaviour
{
    public Animator BigDoorOpening;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entro trigger " + other.name);
        if (other.CompareTag("Player"))
        {
            BigDoorOpening.SetBool("BigdoorOpened", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BigDoorOpening.SetBool("BigdoorOpened", false);
        }
    }


}
