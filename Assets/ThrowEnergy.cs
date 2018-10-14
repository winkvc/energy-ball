using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowEnergy : MonoBehaviour {

	public GameObject target;
	public GameObject projectile;
	float speed = 1.0f;
	public bool isSending = false;
	public GameObject instructionsManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.frameCount % 200 == 0 && isSending) {
			if (GetComponent<HealthManager> ().health < 0.1f) {
				// game over!
				instructionsManager.SetActive(true);
				Text mainText = instructionsManager.GetComponentInChildren<Text> ();
				mainText.text = "I don't think I can carry on...";
				isSending = false;

			}
			GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);

			Vector3 offset = target.transform.position - this.transform.position;

			Vector3 direction = (offset + 0.1f * Random.insideUnitSphere * offset.magnitude).normalized;
			newProjectile.GetComponent<Rigidbody> ().velocity = direction * speed;
			GetComponent<HealthManager> ().health -= 0.1f;
		}
	}

	void OnTriggerEnter(Collider collider) {
		GameObject other = collider.gameObject;
		if (other.CompareTag ("ScreenPhysical") && other.GetComponent<ReboundFlag>().hasRebounded) {
			GetComponent<HealthManager>().health += 0.1f * other.GetComponent<Rigidbody> ().velocity.magnitude;
			Destroy (other);
			if (GetComponent<HealthManager> ().health > 1.0f) {
				instructionsManager.SetActive (true);
				Text mainText = instructionsManager.GetComponentInChildren<Text> ();
				mainText.text = "Thank you! Now I can continue on my journey back home!";
				isSending = false;
			}
		}
	}
}
