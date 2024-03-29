using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private bool isFinalLevel;
    public UnityEvent onLevelStart;
    public UnityEvent onLevelEnd;

    public void StartLevel()
    {
        onLevelStart?.Invoke();
    }


    public void EndLevel()
    {
        onLevelEnd?.Invoke();

        if (isFinalLevel)
        {
            GameManager.GetInstance().ChangeStates(GameManager.GameState.GameEnd,this);


        }else
        {
            GameManager.GetInstance().ChangeStates(GameManager.GameState.LevelEnd, this);
        }
    }

    public void EndGame()
    {
        GameManager.GetInstance().ChangeStates(GameManager.GameState.GameOver, this);
    }
}
