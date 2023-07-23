using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // static positions for reference
    public Vector3 openPos;
    private Vector3 closePos;

    // keeping track of positions at runtime
    private Vector3 lastPos;
    private Vector3 targetPos;

    public float openingClosingSpeed = 10f;
    public AnimationCurve curve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
    private bool open;
    private bool lerping;
    private float lerpCounter;

    private void Start()
    {
        closePos = transform.position;
        lastPos = closePos;
        open = false;
        lerping = false;
        lerpCounter = 0.0f;
    }

    private void Update()
    {
        if (lerping)
        {
            lerpCounter += Time.deltaTime * openingClosingSpeed;
            transform.position = Vector3.Lerp(lastPos, targetPos, curve.Evaluate(lerpCounter));
            if (lerpCounter >= 1.0f)
            {
                lerping = false;
            }
        }
    }

    public void Open()
    {
        Reset();
        targetPos = openPos;
        open = true;
    }

    public void Close()
    {
        Reset();
        targetPos = closePos;
        open = false;
    }

    private void Reset()
    {
        lastPos = transform.position;
        lerpCounter = 0.0f;
        lerping = true;
    }
}
