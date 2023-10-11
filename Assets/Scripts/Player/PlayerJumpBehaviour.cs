using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovementBehaviour))]
public class PlayerJumpBehaviour : Interactor
{
    [Header("Jump")]
    [SerializeField] private float jumpVelocity;
    

    private PlayerMovementBehaviour movementBehaviour;


    // Start is called before the first frame update
    void Start()
    {
            movementBehaviour = GetComponent<PlayerMovementBehaviour>();
    }

    public override void Interact()
    {
        if (playerInput.jumpPressed && movementBehaviour.isGrounded)
        {
            movementBehaviour.SetYVelocity(jumpVelocity);
        }
    }


}
