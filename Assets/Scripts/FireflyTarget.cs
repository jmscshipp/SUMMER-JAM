using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FireflyTarget : MonoBehaviour
{
    public UnityEvent onFireflyActivation;
    public UnityEvent onFireflyDeactivation;
    private FireflyController controller;
    public Vector3 targetPos; // if we don't want firefly to go straight to center of the object, specify a location

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<FireflyController>();
    }

    public void SetThisAsTarget()
    {
        controller.SetTarget(this);
    }
}
