using JetBrains.Rider.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickCube : MonoBehaviour, IPickable
{
    Rigidbody cubeRB;

    // Start is called before the first frame update
    void Start()
    {
        cubeRB = GetComponent<Rigidbody>();    
    }

    void IPickable.OnDropped()
    {
        cubeRB.isKinematic = false;
        cubeRB.useGravity = true;
        transform.SetParent(null);
    }

    void IPickable.OnPicked(Transform attachTransform)
    {
        transform.position = attachTransform.position;
        transform.rotation = attachTransform.rotation;
        transform.SetParent(attachTransform);
        cubeRB.isKinematic = true;
        cubeRB.useGravity = false;
    }
}
