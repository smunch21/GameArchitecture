using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
     [SerializeField] private LevelManager leveltoEnd;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            leveltoEnd.EndLevel();
            Destroy(gameObject);
        
            }
        }
}
