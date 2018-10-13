using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMotionTest : MonoBehaviour {

	private Vector3[] positions;
	private float[] times;
	private int lowPassFrameRate = 3;
	private Vector3 measuredVelocity;

	// Use this for initialization
	void Start () {
		positions = new Vector3[lowPassFrameRate];
		times = new float[lowPassFrameRate];
		for (int i = 0; i < lowPassFrameRate; i++) {
			positions [i] = transform.position;
			times [i] = -0.1f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		// change position (simulate position-setting that ARCore does)
		//SwingPosition ();

		// calculate velocity, classic dx/dt way
		//Debug.Log("position is " + transform.position.z.ToString() + " and old position is " + positions [0].z.ToString());
		//Debug.Log("diff in time" + (Time.time - times[0]).ToString());
		measuredVelocity = (transform.position - positions [0]) / (Time.time - times[0]);

		// slide everything over
		for (int i = 0; i < lowPassFrameRate - 1; i++) {
			positions [i] = positions [i + 1];
			times [i] = times [i + 1];
		}

		// add in new values
		positions [lowPassFrameRate - 1] = transform.position;
		times [lowPassFrameRate - 1] = Time.time;
	}

	// sine-wave position
	void SwingPosition() {
		Vector3 position = transform.position;
		position.z = -1.0f + 0.3f * Mathf.Sin (1 * Time.time);
		transform.position = position;
	}


	void OnTriggerEnter (Collider collider)
	{
		GameObject other = collider.gameObject;
		if (other.CompareTag("ScreenPhysical")) {
			// do some math.. ya ready?
			Vector3 otherVelocity = other.GetComponent<Rigidbody>().velocity;
			Debug.Log ("measuredVelocity is" + measuredVelocity.ToString("F3"));
			Debug.Log ("otherVelocity is" + otherVelocity.ToString("F3"));

			// first, take the other's velocity and subtract the screenproxy velocity.
			// This is so that the velocity we're working with is relative to the screenproxy.
			Vector3 relativeVelocity = otherVelocity - measuredVelocity;
			Debug.Log ("relativeVelocity is" + relativeVelocity.ToString("F3"));

			// then, rotate the velocity into the screenproxy's coordinate frame, such that if 
			Vector3 transformedVelocity = Quaternion.Inverse(transform.rotation) * relativeVelocity;
			Debug.Log ("transformedVelocity is" + transformedVelocity.ToString("F3"));

			// negate the direction along the axis facing outward [+z, prolly]
			if (transformedVelocity.z < 0) {
				transformedVelocity.z *= -1;
				other.GetComponent<ReboundFlag> ().hasRebounded = true;
			}
			Debug.Log ("transformedVelocity is" + transformedVelocity.ToString("F3"));

			// rotate back into worldspace
			relativeVelocity = transform.rotation * transformedVelocity;
			Debug.Log ("relativeVelocity is" + relativeVelocity.ToString("F3"));

			// add back the screenproxy velocity.
			otherVelocity = relativeVelocity + measuredVelocity;
			Debug.Log ("otherVelocity is" + otherVelocity.ToString("F3"));
			other.GetComponent<Rigidbody> ().velocity = otherVelocity;
		}
	}
}


