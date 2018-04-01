using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/* Author : Nate Cortes
 * Load Level Methods. Made for CPI441 capstone. 
 */

public class gui_select : MonoBehaviour {

	public void load_game(){
		SceneManager.LoadScene ("VectorGame");
	}

	public void level_one(){
		puzzle_settings (-1, 1, 1);
		SceneManager.LoadScene ("VectorGame");
	}

	public void level_two(){
		puzzle_settings (-1, 0, 1);
		SceneManager.LoadScene ("VectorGame");
	}

	public void level_three(){
		puzzle_settings ( 5, 1, 1);
		SceneManager.LoadScene ("VectorGame");
	}

	public void level_four(){
		puzzle_settings ( 5, 0, 0);
		SceneManager.LoadScene ("VectorGame");
	}


	/***************************************/
	/* Helper Method, sets values of Puzzle*/
	/***************************************/
	private void puzzle_settings( int attempts, int future_paths, int previous_paths){
		Puzzle puzzle = AssetDatabase.LoadAssetAtPath<Puzzle>("Assets/VectorUnknown/Data/Puzzle.asset");

		puzzle.attempt_count = attempts;
		puzzle.display_upcoming_path = future_paths;
		puzzle.display_past_paths = previous_paths;

		Debug.Log ("Loading a new puzzle\n: " + puzzle.ToString ());

		AssetDatabase.SaveAssets ();
	}

}
