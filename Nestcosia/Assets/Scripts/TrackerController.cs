using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerController : MonoBehaviour
{
    public Transform currentObjective;
    public GameObject player;

    public List<Transform> targetPoints = new List<Transform>();

    public Vector3 rotationTest;

    [Header("Tracker Properties")]
    public float RotationSpeed;
    public float alignTresshold;
    public float rayDistance;

    public LayerMask damageMask;

    
    // Start is called before the first frame update
    void Start()
    {
        currentObjective = targetPoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawLine(transform.position, currentObjective.position, Color.green);
        //Debug.DrawLine(currentObjective.position, targetPoints[0].position, Color.cyan);
        //Debug.DrawLine(transform.position, currentObjective.position, Color.black);
        //Debug.DrawLine(transform.position, transform.position + (transform.forward * 6f), Color.red);
        CalculateRotation();
        

        //if (CheckRadioMagnitude() < 8.0 && CheckRadioScan() >= distanceTreshold)
        //{
         //   Debug.Log("******ALERTA*****");
          
        //}

    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        bool hitted = Physics.Raycast(transform.position, transform.forward, out hit, rayDistance, damageMask);
        Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.green);
        

        if (hitted)
        {
            UiManager manager = FindObjectOfType<UiManager>();

            manager.GameOver();
            //Debug.Log("PlAYER CATCHED");
        }

        
    }


    public float CheckRadioScan() {

        Vector3 trackerToPlayer = player.transform.position - transform.position;
        float alignAmount = Vector3.Dot(trackerToPlayer.normalized, transform.forward);
        return alignAmount;
    }

    public float CheckRadioMagnitude()
    {
        Vector3 playerDistance = Vector3.zero;

        playerDistance = player.transform.position - transform.position;

        //Debug.Log(enemyDistance);
        Debug.DrawLine(transform.position, player.transform.position, Color.blue);
        return playerDistance.magnitude;
    }



    public void CalculateRotation() {

        //Build a vector to aim the target and measure the alignment
        Vector3 shootDirection = currentObjective.position - transform.position;
        float alignAmount = Vector3.Dot(shootDirection.normalized, transform.forward);
        //Debug.Log("Alignment: " + alignAmount);


        Quaternion lookOnLook = Quaternion.LookRotation(shootDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, Time.deltaTime);

        if (alignAmount >= alignTresshold)
        {
            foreach (Transform trans in targetPoints)
            {
                if (trans != currentObjective)
                {
                    currentObjective = trans;
                    return;
                }
            }
        }
    }
}
