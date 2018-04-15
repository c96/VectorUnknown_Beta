using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollision : MonoBehaviour {

	public UFO_PuzzleManager manager;

	void Start () {
		//manager = GameObject.FindGameObjectWithTag ("Manager").GetComponent< UFO_PuzzleManager>();
	}

	public void OnTriggerEnter( ){
		Debug.Log ("triggered");
	}
}
