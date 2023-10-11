using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorKey : MonoBehaviour
{
    [SerializeField] private float pickUpRadius;
    [SerializeField] CapsuleCollider redKey;

    private bool hasRedKey = false;

    public UnityEvent redKeyCollected;

    // Start is called before the first frame update
    void Start()
    {
       redKey = GetComponent<CapsuleCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        other = redKey;
        other.gameObject.SetActive(false);
        hasRedKey = true;
        redKeyCollected?.Invoke();
    }
}
