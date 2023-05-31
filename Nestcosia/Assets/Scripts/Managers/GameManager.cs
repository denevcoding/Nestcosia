using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int batteryAmount;
    public int plantsAmount;
    public static GameManager instance;

    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

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

    public void loadGame()
    {
        SceneManager.LoadScene("level", LoadSceneMode.Single);
    }

    public void loadCharacters()
    {
        SceneManager.LoadScene("characters", LoadSceneMode.Single);
    }

    public void loadStart()
    {
        SceneManager.LoadScene("level_menuStart", LoadSceneMode.Single);
    }

    public void loadCredits()
    {
        SceneManager.LoadScene("credits", LoadSceneMode.Single);
    }
}
