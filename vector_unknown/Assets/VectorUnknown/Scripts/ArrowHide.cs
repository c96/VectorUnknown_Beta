using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHide : MonoBehaviour {

	public GameObject puzzle_info;

	public LineRenderer future, past;

	// Update is called once per frame
	void Update() {

		if (puzzle_info.GetComponent<puzzle_info> ().GetDisplayUpcomingPath () == 1)
			future.enabled = true;
		else
			future.enabled = false;

		if (puzzle_info.GetComponent<puzzle_info> ().GetDisplayPastPaths () == 1)
			past.enabled = true;
		else
			past.enabled = false;

	}
}
