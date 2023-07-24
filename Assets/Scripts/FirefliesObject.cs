using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirefliesObject : MonoBehaviour
{
	public GameObject markerPrefab;
    private FireflyTarget currentTarget;
	private FireflyController controller;
	private bool flying = false;

	private Vector3[] checkpoints = new Vector3[4];
	float counter = 0f;
	Vector3 posFromBezier;

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<FireflyController>();
	}

	// Update is called once per frame
	void Update()
    {
		if (flying)
        {
			counter += Time.deltaTime;
			GetBezier(out posFromBezier, checkpoints, counter);

			if (Vector3Equal(transform.localPosition, checkpoints[3]))
			{
				// arrived at destination
				GetComponent<SphereCollider>().enabled = true;
				currentTarget.onFireflyActivation.Invoke();
				flying = false;
			}
			else
				transform.localPosition = posFromBezier;
		}
	}

	public void GoToTarget(FireflyTarget newTarget, bool setParentToNull = true)
    {
		GetComponent<ObjectSelection>().Deselect();

		if (currentTarget != null)
			currentTarget.onFireflyDeactivation.Invoke();

		GetComponent<SphereCollider>().enabled = false;

        currentTarget = newTarget;
        flying = true;
		counter = 0.0f;

		if (setParentToNull)
        {
			transform.parent = null;
			GeneratePoints(transform.position, newTarget.targetPos);

		}
		else
        {
			GeneratePoints(transform.localPosition, newTarget.targetPos);

        }

	}

	public void ReturnToPlayer()
    {
		transform.parent = Camera.main.transform;
		GoToTarget(controller.gameObject.GetComponent<FireflyTarget>(), false);
    }

	// math functions for bezier curve
	void GeneratePoints(Vector3 start, Vector3 end)
	{
		checkpoints[0] = start;
		checkpoints[3] = end;

		float negative;
		Vector3 randOffset;

		// creating first checkpoint
		Vector3 one = start + (end - start) / 3.0f; // first third
		negative = Random.Range(0f, 1f);
		negative = (negative > 0.5f) ? 1f : -1f;
		randOffset = new Vector3(0f, Random.Range(0.5f, 1f) * negative);
		one += randOffset * Vector3.Distance(one, start);
		one.x = 0f;
		checkpoints[1] = one;

		

		// creating second checkpoint
		Vector3 two = end - (end - start) / 4.0f; // last quarter
		negative = Random.Range(0f, 1f);
		negative = (negative > 0.5f) ? 1f : -1f;
		randOffset = new Vector3(0f, Random.Range(0.5f, 1f) * negative);
		two += randOffset * Vector3.Distance(end, two);
		two.x = 0f;
		checkpoints[2] = two;

		for (int i = 0; i < checkpoints.Length; i++)
			Instantiate(markerPrefab, checkpoints[i], Quaternion.identity);

	}

	void GetBezier(out Vector3 pos, Vector3[] points, float time)
	{
		float tt = time * time;
		float ttt = time * tt;

		float u = 1f - time;
		float uu = u * u;
		float uuu = u * uu;

		pos = uuu * points[0];
		pos += 3f * uu * time * points[1];
		pos += 3f * u * tt * points[2];
		pos += ttt * points[3];
	}

	public bool Vector3Equal(Vector3 a, Vector3 b)
	{
		return Vector3.SqrMagnitude(a - b) < 0.1f;
	}
}
