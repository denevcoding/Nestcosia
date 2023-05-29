using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BatteryReceiver : MonoBehaviour
{
    public List<Transform> socketPositions = new List<Transform>();
    public List<Collectible> bateries = new List<Collectible>();
    public int MaxBatteryAmount;

    public UnityEvent onChargerFull;

    // Start is called before the first frame update
    void Start()
    {
        //Fill socket positions
        for (int i = 0; i < transform.childCount; i++)
        {
            socketPositions.Add(transform.GetChild(i));
        }
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddCollectibles(List<Collectible> colectibles)
    {
        if (colectibles.Count > 0)
        {
            foreach (Collectible colect in colectibles)
            {
                if (bateries.Count >= MaxBatteryAmount)
                {
                    Debug.Log("Este cargador esta full");
                    return;
                }
                if (bateries.Contains(colect))
                {
                    Debug.Log("Esta pila ya esta en la lista");
                    continue;
                }
                if (colect.isCaptured)
                {
                    Debug.Log("Esta pila ya esta en otro cargador");
                    continue;
                }
             

                bateries.Add(colect);
                colect.isCaptured = true;
                RefreshBatteriePositions();

                if (bateries.Count == MaxBatteryAmount)
                    onChargerFull.Invoke();

            }
        }
        else
        {
            Debug.Log("No tienes pilas en tu inventario");
        }

      
    }



    public void RefreshBatteriePositions()
    {
        foreach (Collectible item in bateries)
        {
            //Battery is active in the wrold or not
            if (item.isCaptured)
            {
                int listPosition = bateries.IndexOf(item);
                item.gameObject.transform.position = socketPositions[listPosition].position;
                item.gameObject.SetActive(true);
            }
          
        }
    }




}
