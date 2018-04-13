using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class load_grid_numbers_canvas : MonoBehaviour {

	public GameObject number;

	private List< Vector3> points;
	private GameObject camera_reflect;
	private List< GameObject> numbers;
	 
	void Start () {
		points = new List< Vector3> ();
		numbers = new List< GameObject> ();

		camera_reflect = GameObject.FindGameObjectWithTag ("Reflect");

		for (int i = 1; i < 10; i++) {
			points.Add (new Vector3 ( i * 2.0f, 0, 0));   	// positive x values
			points.Add (new Vector3 ( -i * 2.0f, 0, 0));  	// negative x values
			points.Add (new Vector3 ( 0, 0, i * 2.0f)); 	// positive y values
			points.Add (new Vector3 ( 0, 0, -i * 2.0f));	// negative y-values
		}

		foreach (Vector3 point in points) {
			GameObject load_number = Instantiate ( number, point, Quaternion.identity, transform);
			load_number.transform.GetChild(0).GetComponent< Text>().text = point.x != 0.0f ? point.x.ToString() : point.z.ToString();
			load_number.transform.localPosition = point;

			numbers.Add (load_number);
		}

		//transform.position = new Vector3( transform.position.x, transform.position.y -3.0f, transform.position.z);
	}

	void Update () {
		foreach (GameObject number in numbers) {
			number.transform.LookAt ( camera_reflect.transform);
		}
	}
}
