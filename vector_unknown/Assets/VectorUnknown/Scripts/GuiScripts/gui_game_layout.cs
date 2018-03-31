using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class gui_game_layout : MonoBehaviour {

	[SerializeField]  RectTransform panel_transform;


	void Awake () { 
		panel_transform = transform.GetChild (0).GetComponent< RectTransform> ();
		Debug.Log ("Printing accessed recttransform");
		Debug.Log (panel_transform.ToString ());

		//Vector2 delta = new Vector2 (20, 20);
		Rect screen = AspectUtility.screenRect;
		panel_transform.rect.Set( screen.x, screen.y, screen.width, screen.height);


	}

	void Update () {
		
	}
}
