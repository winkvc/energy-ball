using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsManager : MonoBehaviour {

	public Text mainText;
	public GameObject[] buttons;
	public int step = 0;
	public string[] instructionSteps;
	public ThrowEnergy throwing;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StepForwardInDialogue() {
		buttons [step].SetActive (false);
		step++;
		if (step < buttons.Length && buttons [step] && step < instructionSteps.Length) {
			mainText.text = instructionSteps [step];
			buttons [step].SetActive (true);
		} else {
			gameObject.SetActive (false);
			throwing.isSending = true;
		}
	}
}