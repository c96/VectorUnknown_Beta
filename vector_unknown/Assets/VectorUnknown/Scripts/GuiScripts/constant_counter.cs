using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/* Author : Nate Cortes
 * Holds a counter for a constant value. Made for CPI441 capstone. 
 */

public class constant_counter : MonoBehaviour {
	[SerializeField]
	public int constant = 0;// constant value 
	private bool change = false;
	private GameObject text_display;

	void Awake(){
		text_display = transform.GetChild (0).gameObject;

		GameObject temp = transform.GetChild (1).gameObject;
		temp.GetComponent< Button>().onClick.AddListener (decrement);
		temp = transform.GetChild (2).gameObject;
		temp.GetComponent< Button>().onClick.AddListener (increment);
	}

	void Update(){
		if( change)
			text_display.GetComponent< Text>().text = constant.ToString ();

		change = false;
	}

	public void increment(){
		if (constant < 9) {
			constant = constant + 1;
			change = true;
		}
	}

	public void decrement(){
		if (constant > -9) {
			constant = constant - 1;
			change = true;
		}
	}

	public void reset(){
		constant = 0;
		change = true;
	}
}
