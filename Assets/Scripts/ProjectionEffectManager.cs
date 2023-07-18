using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ProjectionEffectManager : MonoBehaviour
{
    private List<ObjectProjectionEffectControl> objects = new List<ObjectProjectionEffectControl>();
    private bool projecting;
    private Volume postProcessing;

    // singleton setup
    public static ProjectionEffectManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null) { return; }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        postProcessing = GameObject.FindWithTag("Post Processing").GetComponent<Volume>();
        projecting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (projecting)
                EndProjection();
            else
                BeginProjection();
        }
    }

    public void AddObjectToProjectionList(ObjectProjectionEffectControl prop)
    {
        objects.Add(prop);
    }

    public void RemoveObjectFromProjectionList(ObjectProjectionEffectControl prop)
    {
        objects.Remove(prop);
    }

    private void BeginProjection()
    {
        postProcessing.enabled = true;
        foreach (ObjectProjectionEffectControl obj in objects)
            obj.EnableProjectionEffect();

        projecting = true;
    }

    private void EndProjection()
    {
        postProcessing.enabled = false;
        foreach (ObjectProjectionEffectControl obj in objects)
            obj.DisableProjectionEffect();

        projecting = false;
    }
}
