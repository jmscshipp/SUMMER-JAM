using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyController : MonoBehaviour
{
    public FirefliesObject fireflies;
    private bool holdingFireflies;
    private FirstPersonSelection selector;
    private Vector3 fireflyHoldingPos;

    // Start is called before the first frame update
    void Start()
    {
        holdingFireflies = true;
        fireflyHoldingPos = fireflies.transform.localPosition;
        GetComponent<FireflyTarget>().targetPos = fireflyHoldingPos;
        selector = GetComponent<FirstPersonSelection>();
    }

    public Vector3 GetFireflyHoldingPos()
    {
        return fireflyHoldingPos;
    }

    public void SetTarget(FireflyTarget target)
    {
        holdingFireflies = false;
        fireflies.GoToTarget(target.GetComponent<FireflyTarget>());
    }

    public void ReturnFireflies()
    {
        holdingFireflies = true;
        fireflies.transform.localPosition = fireflyHoldingPos;
    }
}
