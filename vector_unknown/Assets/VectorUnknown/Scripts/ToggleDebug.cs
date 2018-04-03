using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleDebug : MonoBehaviour {

	public GameObject debug;

	void Start(){
		debug.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.D)) {
			debug.SetActive ( !debug.activeSelf);
		}
	}
}
