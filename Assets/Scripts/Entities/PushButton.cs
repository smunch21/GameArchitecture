using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButton : MonoBehaviour, ISelectable
{
    /*  [SerializeField] private Material defaultMaterial;
      [SerializeField] private Material hoverMaterial;
      [SerializeField] private MeshRenderer buttonRender;*/

    public UnityEvent onHoverEnter;
    public UnityEvent onHoverExit;

    public UnityEvent OnPush;

    public void OnHoverEnter()
    {
        onHoverEnter?.Invoke();
    }

    public void OnHoverExit()
    {
        onHoverExit?.Invoke();
    }

    public void OnSelect()
    {
        OnPush?.Invoke();
    }
}
