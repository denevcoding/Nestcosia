using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int batteryAmount;
    public int plantsAmount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPlantita()
    {
        plantsAmount+=1;
    }

    public void AddEnergy()
    {
        batteryAmount += 1;
    }
}
