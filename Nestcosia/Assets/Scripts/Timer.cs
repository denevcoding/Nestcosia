using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{

    public TextMeshProUGUI timerText;

    public float currentTime;
    public bool countDown;

    //limit settings

    public bool hasLimit;
    public float timerLimit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (countDown)
        {
            currentTime -= Time.deltaTime;
        }

        else
        {
            currentTime += Time.deltaTime;
        }

        if(hasLimit && ((countDown && currentTime <= timerLimit) || (!countDown && currentTime >= timerLimit)))
        {
            currentTime = timerLimit;
            setTimerText();
            timerText.color = Color.red;
            enabled = false;

            UiManager manager = FindObjectOfType<UiManager>();

            manager.GameOver();
        }

        setTimerText();

    }

    private void setTimerText()
    {
        timerText.text = currentTime.ToString("0.00");
    }
}
