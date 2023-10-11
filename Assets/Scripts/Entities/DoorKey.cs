using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class DoorKey : MonoBehaviour
{
    public UnityEvent keyCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            keyCollected?.Invoke();
            Destroy(gameObject);
        }    
    }
}
