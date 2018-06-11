using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class puzzle_info : MonoBehaviour {

	public Vector3 player_position; 		//player's starting position

	//public List< Vector3> goal_positions; 	//target position. May have multiple endpoints
	//public List< Vector2> choices; 			//Choices to be displayed on the UI

	//public Puzzle puzzle; 					//Puzzle ScriptableObject
	public int attempt_count;
	public int display_upcoming_path;
	public int display_past_paths;
	public int game_mode;

	public void Start(){
		Reset (); // initializes all data structures for base game
		//Debug.Log( "GameMode: " + ( game_mode == 0 ? "Standard" : "Tour"));

		UFO_PuzzleManager manager = GameObject.FindGameObjectWithTag ("Manager").GetComponent< UFO_PuzzleManager>();
		manager.debug_new_puzzle ();
		manager.set_models ( game_mode);

	}

	public void Reset(){
		
		player_position = new Vector3 (0, 0, 0); //inital starting point of <0, 0, 0>
		//goal_positions = new List<Vector3> ();
		//choices = new List< Vector2> ();

		GameObject level_data = GameObject.Find ("LevelData");
		gui_select data = level_data.GetComponent< gui_select> ();

		attempt_count = data.attempt_count;
		display_upcoming_path = data.display_upcoming_path;
		display_past_paths = data.display_past_paths;
		game_mode = data.load_game_mode;

		//Destroy (level_data);
	}

	public bool game_over(){
		return (attempt_count == 0); 
	}

	public int GetDisplayUpcomingPath()
	{
		return display_upcoming_path;
	}

	public void SetDisplayUpcomingPath(int choice)
	{
		if (choice == 0 || choice == 1)
			display_upcoming_path = choice;
	}

    public int GetDisplayPastPaths()
    {
		return display_past_paths;
    }

    public void SetDisplayPastPaths(int choice)
    {
        if (choice == 0 || choice == 1)
			display_past_paths = choice;
    }

}
