using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour

{
    public static GameManager instance;

    //private GameState currentState;
    private  
    // Start is called before the first frame update
    void Start()
    {
     if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this; 
    }

    public static GameManager GetInstance()
    {
        return instance;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
