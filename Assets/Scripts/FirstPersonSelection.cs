using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonSelection : MonoBehaviour
{
    Ray RayOrigin;
    RaycastHit HitInfo;
    private GameObject selectedObject;

    // Start is called before the first frame update
    void Start()
    {
        selectedObject = null;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Transform cameraTransform = Camera.main.transform;

        // selecting object
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out HitInfo, 100.0f)
            && HitInfo.collider.gameObject.GetComponent<ObjectSelection>() != null && HitInfo.collider.gameObject != selectedObject)
        {
            selectedObject = HitInfo.collider.gameObject;
            selectedObject.GetComponent<ObjectSelection>().Select();
        }
        else if (HitInfo.collider == null || HitInfo.collider.gameObject.GetComponent<ObjectSelection>() == null)// nothing to select, so deselect
        {
            if (selectedObject != null)
            {
                selectedObject.GetComponent<ObjectSelection>().Deselect();
                selectedObject = null;
            }
        }

        // interact
        if (Input.GetKeyDown(KeyCode.Mouse0) && selectedObject != null)
        {
            selectedObject.GetComponent<ObjectSelection>().Interact();
        }
    }

    public GameObject GetCurrentSelectedObject()
    {
        return selectedObject;
    }
}
