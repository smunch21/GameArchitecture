using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeValue = 120f;
    public TMP_Text text;
    private LevelManager LevelManager;

    private void Update()
    {
        if(timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
        DisplayTime(timeValue);
    }

    void DisplayTime(float timeToDisplay)
    {
        if(timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    

}
