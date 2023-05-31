using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    public AudioClip soundClickSFX;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        AudioManager.instance.PlaySFX(soundClickSFX);
        Debug.Log("me clickearon");
    }


    public void loadMenu()
    {
        GameManager.instance.loadStart();
    }

    public void loadCharacters()
    {
        GameManager.instance.loadCharacters();
    }

    public void loadGame()
    {
        GameManager.instance.loadGame();
    }

    public void loadCredits()
    {
        GameManager.instance.loadCredits();
    }
}
