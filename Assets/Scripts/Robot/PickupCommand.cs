using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using UnityEngine;
using UnityEngine.AI;

public class PickupCommand : Command
{
    private IPickable pickableObject;
    private Transform attachTransform;
    private bool isExecuted;
    private NavMeshAgent agent;

    public PickupCommand(NavMeshAgent _agent, IPickable _pickableObject, Transform _attachTransform)
    {
        pickableObject = _pickableObject;
        agent = _agent;
        attachTransform = _attachTransform;

    }

    public override void Execute()
    {
        if (!isExecuted)
        {
            pickableObject.OnPicked(attachTransform);
            isExecuted = true;
        }
    }

    public override bool isComplete
    {
        get { return isExecuted; }
    }

}
