using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Nate Cortes
 * 	Rotate camera script for IOLA vector game
 */

public class rotate_camera : MonoBehaviour {

	[SerializeField]
	private Vector3 target;//center of game board
	private GameObject follow;//transform to smoothly follow
	private float v_lock = 0.0f;//vertical rotation limit, will not rotate >36 degrees above the board
	private Vector3 velocity = Vector3.zero;//camera follow velocity
	private bool keypress = false;

	void Start () {
		target = GameObject.FindGameObjectWithTag("Center").transform.position;
		follow = GameObject.FindGameObjectWithTag ("Follow");

		transform.RotateAround(target, transform.TransformDirection(Vector3.right), 45.0f);
		follow.transform.position = transform.position;
		follow.transform.LookAt(target);
		transform.LookAt(target);
	}

	void Update () {
		//horizontal rotations
		if (Input.GetKeyDown (KeyCode.LeftArrow) && !keypress) {
			follow.transform.RotateAround (target, Vector3.up, 45.0f);
			keypress = true;
		}
		if (Input.GetKeyDown (KeyCode.RightArrow) && !keypress) {
			follow.transform.RotateAround (target, Vector3.down, 45.0f);
			keypress = true;
		}
		if( Input.GetKeyDown( KeyCode.R))
			reset_camera();
		
		//vetrical rotations
		if (Input.GetKeyDown (KeyCode.UpArrow) && v_lock < 36.0f && !keypress) {
			spin_vertical (12.0f, 1);
			keypress = true;
		}
		if (Input.GetKeyDown (KeyCode.DownArrow) && v_lock > 0.0f && !keypress) {
			spin_vertical (12.0f, -1);
			keypress = true;
		}

		//SmoothDamp to move the camera to the follow position, then retarget the center of the board
		transform.position = Vector3.SmoothDamp (transform.position, follow.transform.position, ref velocity, 0.3f);
		transform.LookAt (target);
		keypress = false;
	}

	void spin_vertical( float degree, int direction){
		if (direction > 0) { // if positive, rotate up
			follow.transform.RotateAround(target, transform.TransformDirection(Vector3.right), degree);
			v_lock += degree;
		} else { // if negative, rotate down
			follow.transform.RotateAround(target, transform.TransformDirection(Vector3.left), degree);
			v_lock -= degree;
		}
	}

	void reset_camera(){
		follow.transform.position = new Vector3 (0f, 36.06245f, -34.64823f);
	}
}
