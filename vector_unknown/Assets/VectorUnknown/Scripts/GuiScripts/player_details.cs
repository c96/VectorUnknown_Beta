using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_details : MonoBehaviour {

	private Text details;
	public GameObject puzzle_manager;

	void Start () {
		details = transform.GetChild(0).GetComponent< Text> ();
	}

	void Update () {
		details.text = puzzle_manager.GetComponent< UFO_PuzzleManager>().game_details ();
	}
}
