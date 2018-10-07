using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingRotation : MonoBehaviour {

	Quaternion[] rotations;

	int loopDuration; // frames
	int ROTATIONS_LENGTH = 3; // kinds of rotations.
	int framesUntilNewLoop = 0;

	void SetLoopDuration () {
		loopDuration = (int)(Random.value * 20) + 15;
		framesUntilNewLoop = loopDuration;
	}

	// Use this for initialization
	void Start () {
		rotations = new Quaternion[ROTATIONS_LENGTH];
		for (int i = 0; i < ROTATIONS_LENGTH; i++) {
			rotations [i] = Random.rotationUniform;
		}
		SetLoopDuration ();
	}
	
	// Update is called once per frame
	void Update () {
		// where are we in the loop?
		if (framesUntilNewLoop == 0) {
			SetLoopDuration();
		}
		int frameInLoop = loopDuration - framesUntilNewLoop;

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
		float progressInLoop = frameInLoop / (float)loopDuration;
		float[] weight = new float[ROTATIONS_LENGTH];
		float loopsToRadians = Mathf.PI / (ROTATIONS_LENGTH - 1);
		for (int i = 0; i < ROTATIONS_LENGTH; i++) {
			float t = (i + progressInLoop) / ROTATIONS_LENGTH;
			weight [i] = 3 * (1 - t) * t * t * 0.3f;
		}

		// now actually "interpolate"
		for (int i = ROTATIONS_LENGTH - 1; i > 0; i--) {
			transform.rotation = Quaternion.Slerp(
				transform.rotation,
				rotations[i],
				weight[i]
			);
			//Debug.Log ("weight is: " + weight[i] / LOOP_DURATION);
		}

		framesUntilNewLoop--;

	}
}
