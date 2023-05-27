using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryReceiver : MonoBehaviour
{
    public List<Transform> socketPositions = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            socketPositions.Add(transform.GetChild(i));
        }
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
