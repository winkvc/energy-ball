using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowEnergy : MonoBehaviour {

	public GameObject target;
	public GameObject projectile;
	float speed = 1.0f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.frameCount % 200 == 0) {
			GameObject newProjectile = Instantiate(projectile, transform.position + new Vector3(0.0f, 0.0f, -0.3f), Quaternion.identity);

			Vector3 direction = (target.transform.position - this.transform.position).normalized;
			newProjectile.GetComponent<Rigidbody> ().velocity = direction * speed;
		}
	}
}
