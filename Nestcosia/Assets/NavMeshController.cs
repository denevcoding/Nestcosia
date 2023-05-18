using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{

    public Transform objective;

    // Start is called before the first frame update
    void Start()
    {
        //NavMeshAgent bot = GetComponent<NavMeshAgent>();
        //bot.destination = objective.position;

    }

    // Update is called once per frame
    void Update()
    {
        NavMeshAgent bot = GetComponent<NavMeshAgent>();
        bot.destination = objective.position;
    }
}
