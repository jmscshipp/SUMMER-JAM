using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectProjectionEffectControl : MonoBehaviour
{
    public LayerMask defaultLayer;
    public LayerMask projectionEffectLayer;

    private bool projectionEffectActive;

    private void Start()
    {
        ProjectionEffectManager.instance.AddObjectToProjectionList(this);    
    }

    public void EnableProjectionEffect()
    {
        int layerNum = (int)Mathf.Log(projectionEffectLayer.value, 2);
        gameObject.layer = layerNum;

        if (transform.childCount > 0)
            SetLayerAllChildren(layerNum);
    }

    public void DisableProjectionEffect()
    {
        int layerNum = (int)Mathf.Log(defaultLayer.value, 2);
        gameObject.layer = layerNum;

        if (transform.childCount > 0)
            SetLayerAllChildren(layerNum);
    }

    private void SetLayerAllChildren(int layer)
    {
        Transform[] children = GetComponentsInChildren<Transform>(includeInactive: true);

        foreach (Transform child in children)
            child.gameObject.layer = layer;
    }
}
