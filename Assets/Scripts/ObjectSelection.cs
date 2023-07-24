using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectSelection : MonoBehaviour
{
    public UnityEvent onInteract;
    private Outline outline;

    // Start is called before the first frame update
    void Start()
    {
        outline = GetComponent<Outline>();
        if (outline == null)
        {
            outline = gameObject.AddComponent<Outline>();
            outline.OutlineWidth = 10.0f;
        }
    }

    public void Select()
    {
        outline.OutlineMode = Outline.Mode.OutlineVisible;
    }

    public void Deselect()
    {
        outline.OutlineMode = Outline.Mode.Disabled;
    }

    public void Interact()
    {
        onInteract.Invoke();
    }
}
