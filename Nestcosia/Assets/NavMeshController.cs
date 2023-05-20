using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
    public Transform currentObjective;
    public GameObject player;

    public LayerMask detectionMask;
    public LayerMask playerMask;

    public List<Transform> wayPaints = new List<Transform>();
    NavMeshAgent bot;


    private void Awake()
    {
        bot = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentObjective = wayPaints[0];
        //NavMeshAgent bot = GetComponent<NavMeshAgent>();
        bot.destination = currentObjective.position;
    }

    private void OnDrawGizmos()
    {
        if (currentObjective != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(currentObjective.position, 1.5f);
            Gizmos.DrawWireSphere(player.transform.position, 1.5f);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckDistancePlayer() < 6.0) {
            Debug.Log("CAPTURADO");
            bot.destination = player.transform.position;
        }





        //bot.destination = objective.position;
        if (CheckDistanceToPoint() < 1.5)
        {

            foreach (Transform position in wayPaints)
            {
                //Debug.Log("Searching");
                if (position != currentObjective)
                {
                    currentObjective = position;
                    bot.destination = currentObjective.position;
                    return;
                }
            }
        }
    }


    //public void FixedUpdate()
    //{
      //  Physics.SphereCast(transform.position, );
    //}

    //para el recorrido de la patrulla
    public float CheckDistanceToPoint()
    {
        Vector3 charPoint = Vector3.zero;
        if (currentObjective != null)
        {
            charPoint = currentObjective.position - transform.position;

            Debug.DrawLine(transform.position , currentObjective.position, Color.green);
            return charPoint.magnitude;
        }

        return -1;
    }



    //para capturar al jugador
    public float CheckDistancePlayer()
    {
        Vector3 enemyDistance = Vector3.zero;

        enemyDistance =   player.transform.position - transform.position;

        //Debug.Log(enemyDistance);
        Debug.DrawLine(transform.position, player.transform.position, Color.blue);
        return enemyDistance.magnitude;
    }

}
