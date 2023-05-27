using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectibleType
{
    BlueCell,
    GreenCell,
}
public class Collectible : MonoBehaviour
{
    public GameManager gameManager;
    public CollectibleType type;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.tag == "Player")
        {
            if (type == CollectibleType.BlueCell)
            {
                gameManager.AddEnergy();

            }
            if (type == CollectibleType.GreenCell)
            {
                gameManager.AddPlantita();
            }
      
            Destroy(this.gameObject);
        }
    }
}
