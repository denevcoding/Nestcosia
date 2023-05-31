using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Elevator : MonoBehaviour
{
    public List<BatteryReceiver> cargadores = new List<BatteryReceiver>();
    public int fullChargers = 0;

    public Animator animatorElevator;
    public GameObject textUi;



    // Start is called before the first frame update
    void Start()
    {
        animatorElevator = gameObject.GetComponent<Animator>();

        foreach (BatteryReceiver charger in cargadores)
        {
            charger.onChargerFull.AddListener(OnChargerFilled);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnChargerFilled()
    {
        Debug.Log("Un cargador esta lleno");
        fullChargers++;
    }


    public void ActivateElevator()
    {
        //Ya estan llenos todos los cargadores?
        if (fullChargers == cargadores.Count)
        {
            animatorElevator.SetBool("LiftUnlocked", true);

            UiManager manager = FindObjectOfType<UiManager>();

            manager.Winner();

        }
        else
        {
            Debug.Log("Algun cargador que activa este elevador aun no esta lleno");
        }
    }

    public void showUI(bool show)
    {
        textUi.SetActive(show);
    }

}
