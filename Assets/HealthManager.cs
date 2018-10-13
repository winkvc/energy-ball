using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

	public Material ballMaterial;
	public ParticleSystem ps;
	public TrailRenderer[] trails;
	public float health = 0.5f;
	private float oldHealth;

	// Use this for initialization
	void Start () {
		UpdateColors ();
		oldHealth = health;
	}
	
	// Update is called once per frame
	void Update () {
		if (oldHealth != health) {
			UpdateColors ();
			oldHealth = health;
		}
	}

	public void UpdateColors () {
		Color theColor;
		if (health < 0.25f) { // between black and red
			theColor = new Color (health * 4.0f, 0.0f, 0.0f);
		} else if (health < 0.75f) { // between red and yellow, with orange in the middle.
			theColor = new Color (1.0f, (health - .25f) * 2, 0.0f);
		} else { // between yellow and white
			theColor = new Color (1.0f, 1.0f, (health - .75f) * 4.0f);
		}
			
		// change all the different things to that color.
		ballMaterial.color = theColor;
		ParticleSystem.MainModule main = ps.main;
		main.startColor = theColor;
		foreach (TrailRenderer tr in trails) {
			tr.startColor = theColor;
			tr.endColor = theColor;
		}
	}
}
