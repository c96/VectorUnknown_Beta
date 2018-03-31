using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHide : MonoBehaviour {

	public GameObject puzzle_info;

	public LineRenderer rend;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown(KeyCode.F))
		{
			puzzle_info.GetComponent<puzzle_info>().SetDisplayUpcomingPath(1);
		}

		if (puzzle_info.GetComponent<puzzle_info>().GetDisplayUpcomingPath() == 1)
        {
            rend.enabled = true;
		}

		if (Input.GetKeyDown(KeyCode.G))
		{ 
			puzzle_info.GetComponent<puzzle_info>().SetDisplayUpcomingPath(0);
		}
        if (puzzle_info.GetComponent<puzzle_info>().GetDisplayUpcomingPath() == 0)
        {
            rend.enabled = false;
        }
	}
}
