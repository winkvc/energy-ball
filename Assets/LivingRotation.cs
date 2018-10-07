using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingRotation : MonoBehaviour {

	Quaternion[] rotations;

	int LOOP_DURATION = 30; // frames
	int ROTATIONS_LENGTH = 3; // kinds of rotations.

	// Use this for initialization
	void Start () {
		rotations = new Quaternion[ROTATIONS_LENGTH];
		for (int i = 0; i < ROTATIONS_LENGTH; i++) {
			rotations [i] = Random.rotationUniform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		// where are we in the loop?
		int frameInLoop = Time.frameCount % LOOP_DURATION;

		// maybe get a new goal rotation and shift the rest down.
		if (frameInLoop == 0) {
			for (int i = ROTATIONS_LENGTH - 1; i > 0; i--) {
				rotations [i] = rotations [i - 1];
			}
			rotations[0] = Random.rotationUniform;
			for (int i = 0; i < ROTATIONS_LENGTH; i++) {
				//Debug.Log (i + " " + rotations [i]);
			}
		}

		// calculate how to interpolate.
		float progressInLoop = frameInLoop / (float)LOOP_DURATION;
		float[] weight = new float[ROTATIONS_LENGTH];
		float loopsToRadians = Mathf.PI / (ROTATIONS_LENGTH - 1);
		for (int i = 0; i < ROTATIONS_LENGTH; i++) {
			// note this does not normalize to 1.
			// That's all taken care of by the next step.
			weight[i] = Mathf.Cos(Mathf.Max(0, ROTATIONS_LENGTH - i - progressInLoop - 1) * loopsToRadians) - 
				Mathf.Cos(Mathf.Min(ROTATIONS_LENGTH - i - progressInLoop, ROTATIONS_LENGTH - 1) * loopsToRadians);

			//Debug.Log (i + " " + weight [i]);

		}

		// now actually "interpolate"
		for (int i = ROTATIONS_LENGTH - 1; i > 0; i--) {
			transform.rotation = Quaternion.Slerp(
				transform.rotation,
				rotations[i],
				weight[i] / ((ROTATIONS_LENGTH - 1) * LOOP_DURATION)
			);
			Debug.Log ("weight is: " + weight[i] / LOOP_DURATION);
		}

	}
}
