using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField] private float moveDistance;
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool liftIsRaised;

    private bool isMoving;
    private Vector3 targetPosition;

    public void ToggleLift() 
        {
            if (isMoving)
            {
                return;

            }
            if(liftIsRaised)
            {
                targetPosition = transform.localPosition - new Vector3(0, moveDistance, 0);
                liftIsRaised = false;   
            }
            else
            {
                targetPosition = transform.localPosition + new Vector3(0, moveDistance, 0);
                liftIsRaised = true;
            }
        isMoving = true;
        }

    private void Update()
    {
        if (isMoving)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, moveSpeed * Time.deltaTime);
        }
        if(Vector3.Distance(transform.localPosition, targetPosition) < 0.05f) 
        {
            isMoving = false;
        }
    
    }
}
