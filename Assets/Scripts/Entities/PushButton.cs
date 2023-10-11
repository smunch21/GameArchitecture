using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButton : MonoBehaviour, ISelectable
{
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material hoverMaterial;
    [SerializeField] private MeshRenderer buttonRender;

    public UnityEvent OnPush;

    public void OnHoverEnter()
    {
        buttonRender.material = hoverMaterial;
    }

    public void OnHoverExit()
    {
        buttonRender.material = defaultMaterial;
    }

    public void OnSelect()
    {
        OnPush?.Invoke();
    }
}
