using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/* Author : Nate Cortes
 * Load Level Methods. Made for CPI441 capstone. 
 */

public class gui_select : MonoBehaviour {

	public int attempt_count = 0;
	public int display_upcoming_path = 0;
	public int display_past_paths = 0;

	void Awake(){
		DontDestroyOnLoad (transform.gameObject);
	}

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
		attempt_count = attempts;
		display_upcoming_path = future_paths;
		display_past_paths = previous_paths;
	}

}
