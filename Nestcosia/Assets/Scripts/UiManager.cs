using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{

    public GameObject hud;
    public GameObject gameOverUi;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        hud.SetActive(false);
        gameOverUi.SetActive(true);
    }
}
