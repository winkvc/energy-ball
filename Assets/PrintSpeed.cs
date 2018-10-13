using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintSpeed : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.frameCount % 30 == 0) {
			Debug.Log (GetComponent<Rigidbody> ().velocity.ToString("F3"));
		}
	}
}
