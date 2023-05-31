using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum BotState
{
    Wander,
    Pursuing
}

public class NavMeshController : MonoBehaviour
{
    public BotState state;

    public Transform currentObjective;
    public GameObject player;

    public LayerMask detectionMask;
    public LayerMask playerMask;

    public float rayDistance;
    public LayerMask damageMask;

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
        switch (state)
        {
            case BotState.Wander:
                Wandering();
                break;

                case BotState.Pursuing:
                Pursuing();
                break;
        }

     

       
    }


    private void FixedUpdate()
    {

        //Pursuing();
       


    }

    public void Wandering()
    {
       
        bot.destination = currentObjective.position;
        //bot.destination = objective.position;
        if (CheckDistanceToPoint() < 1.5)
        {
            foreach (Transform position in wayPaints)
            {
                //Debug.Log("Searching");
                if (position != currentObjective)
                {
                    currentObjective = position;
                    //bot.destination = currentObjective.position;
                    return;
                }
            }
        }

        if (currentObjective != null)
        {
            bot.destination = currentObjective.position;
        }

        RaycastHit hit;
        bool hitted = Physics.Raycast(transform.position, transform.forward * rayDistance, out hit, rayDistance, playerMask);

        Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.cyan);

        if (/*CheckDistancePlayer() < 6.0 &&*/ hitted)
        {
            Debug.Log("CAPTURADO y con RAYOO");
            state = BotState.Pursuing;

        }

    }


    public void Pursuing()
    {
        Debug.Log("Pursuing");
     
        bot.destination = player.transform.position;

        Vector3 dir = player.transform.position - transform.position;
        Vector3 rayPos = transform.position;
        rayPos.y += 1.2f;
        RaycastHit hit;
        bool hitted = Physics.Raycast(rayPos, dir * rayDistance, out hit, rayDistance, detectionMask);

        Debug.DrawRay(rayPos, dir * rayDistance, Color.red);

        if (/*CheckDistancePlayer() < 6.0 &&*/ hitted)
        {
            Debug.Log(hit.collider.gameObject.name + " Lo perdi");
            if (hit.collider != player.GetComponent<Collider>())
            {
                
                state = BotState.Wander;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterConrtoller>())
        {
            UiManager manager = FindObjectOfType<UiManager>();

            manager.GameOver();
        }
    }

}
