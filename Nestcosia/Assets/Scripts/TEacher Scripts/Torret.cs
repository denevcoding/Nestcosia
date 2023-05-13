using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torret : MonoBehaviour
{
    public GameObject bullet;
    public Transform target;

    public Transform gunPoint;
    public float force;

    public float distanceTreshold;

    bool hasShoot = false;

    public float RotationSpeed;

    public float cadencyTime;
    public float currentTime;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 shootDirection = target.position - transform.position;

        float alignAmount = Vector3.Dot(shootDirection.normalized, transform.forward);
        Debug.Log("Alineacion " + alignAmount);

        Debug.DrawLine(transform.position, transform.position + (transform.forward * distanceTreshold) , Color.blue);
        Debug.DrawLine(transform.position, transform.position + (shootDirection));


        Quaternion lookOnLook =  Quaternion.LookRotation(shootDirection);
        transform.rotation =  Quaternion.Slerp(transform.rotation, lookOnLook, Time.deltaTime);


        currentTime += Time.deltaTime;

        if (currentTime >= cadencyTime)
        {
            if (shootDirection.magnitude < distanceTreshold)
            {
                if (alignAmount > 0.7)
                {
                   
                        ShootBullet(shootDirection);
                        currentTime = 0f;
                    

                }
            }
        }
        


       





    }

    public void ShootBullet(Vector3 dir)
    {
        GameObject bulletsita = Instantiate(bullet, gunPoint.position, Quaternion.identity);

        Rigidbody bulletBody = bulletsita.GetComponent<Rigidbody>();  
        

        bulletBody.AddForce(dir.normalized * force, ForceMode.Impulse);
        hasShoot = true;
    }


}
