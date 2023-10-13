using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal;
using UnityEngine;

public class GameManager : MonoBehaviour

{
    [SerializeField] private LevelManager[] levels;
    public static GameManager instance;

    private GameState currentState;
    private LevelManager currentlevel;
    
    private int currentLevelIndex = 0;
    private bool isInputActive = false;
    private Timer timer;

    public enum GameState
    {
        Briefing,
        LevelStart,
        LevelIn,
        LevelEnd,
        GameOver,
        GameEnd
    }
    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(instance);
            return;
        }

        instance = this; 
    }

    public static GameManager GetInstance()
    {
        return instance;
    }
    
    public bool IsInputActive()
    {
        return isInputActive;
    }

    public void ChangeStates(GameState state,  LevelManager level)
    {
        currentState = state;
        currentlevel = level;

        switch (currentState)
        {
            case GameState.Briefing:
                StartBreifing(); 
                break;
            case GameState.LevelStart:
                InitialLevel();
                break;
            case GameState.LevelIn:
                RunLevel();
                break;
            case GameState.LevelEnd:
                CompleteLevel();
                break;
            case GameState.GameOver:
                GameOver();
                break;
            case GameState.GameEnd:
                GameEnd();
                break;
        }
    }

    private void StartBreifing()
    {
        isInputActive = false;

        ChangeStates(GameState.LevelStart, currentlevel);
    }

    private void InitialLevel()
    {
        Debug.Log("LevelStart");
        isInputActive = true;
        currentlevel.StartLevel();
        ChangeStates(GameState.LevelIn, currentlevel);
    }
    
    private void RunLevel()
    {
        Debug.Log("Level In " + currentlevel.gameObject.name);
    }

    private void CompleteLevel()
    {
        Debug.Log("LevelEnd");
        ChangeStates(GameState.LevelStart, levels[++currentLevelIndex]);
    }


    private void GameOver()
    {
        isInputActive = false;
    }
    private void GameEnd()
    {
        Debug.Log("Game Over, you Win!");
    }
}
