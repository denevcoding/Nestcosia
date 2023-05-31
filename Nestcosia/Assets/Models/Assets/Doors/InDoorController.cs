using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InDoorController : MonoBehaviour
{
    public Animator InDoorAController;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entro trigger " + other.name);
        if (other.CompareTag("Player"))
        {
            InDoorAController.SetBool("InDoorOpen", true);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InDoorAController.SetBool("InDoorOpen", false);
        }
    }


}
