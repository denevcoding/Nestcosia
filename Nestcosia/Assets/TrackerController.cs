using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerController : MonoBehaviour
{
    public Transform currentObjective;

    public List<Transform> targetPoints = new List<Transform>();

    public Vector3 rotationTest;

    [Header("Tracker Properties")]
    public float RotationSpeed;
    public float distanceTreshold;

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
        Debug.DrawLine(transform.position, currentObjective.position, Color.black);
        Debug.DrawLine(transform.position, transform.position + (transform.forward * 3f), Color.red);
        CalculateRotation();
       
    }

    public float CheckRadioScan()
    {
        Vector3 charPoint = Vector3.zero;
        if (currentObjective != null)
       {
            charPoint = currentObjective.position - transform.position;

            Debug.DrawLine(transform.position, currentObjective.position, Color.green);
            return charPoint.magnitude;
        }

        return -1;
    }



    public void CalculateRotation() {

        //Build a vector to aim the target and measure the alignment
        Vector3 shootDirection = currentObjective.position - transform.position;
        float alignAmount = Vector3.Dot(shootDirection.normalized, transform.forward);
        Debug.Log("Alignment: " + alignAmount);


        Quaternion lookOnLook = Quaternion.LookRotation(shootDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, Time.deltaTime);

        if (alignAmount >= distanceTreshold)
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
