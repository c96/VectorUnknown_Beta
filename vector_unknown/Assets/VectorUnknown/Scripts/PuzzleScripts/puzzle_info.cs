using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class puzzle_info : MonoBehaviour {

	public Vector3 player_position; 		//player's starting position

	public List< Vector3> goal_positions; 	//target position. May have multiple endpoints
	public List< Vector2> choices; 			//Choices to be displayed on the UI

	public Puzzle puzzle; 					//Puzzle ScriptableObject
			
	public void Start(){
		Reset (); 							// initializes all data structures for base game
	}

	public void Reset(){
		
		player_position = new Vector3 (0, 0, 0); //inital starting point of <0, 0, 0>
		goal_positions = new List<Vector3> ();
		choices = new List< Vector2> ();
		puzzle = AssetDatabase.LoadAssetAtPath<Puzzle>("Assets/VectorUnknown/Data/Puzzle.asset");

		Debug.Log (puzzle.ToString ());
	}

	public bool game_over(){
		return (puzzle.attempt_count == 0); 
	}

	public int decrement_attempts(){

		puzzle.attempt_count--;

		if (puzzle.attempt_count == 0)
			return 1; // returns true, max attempts reached

		return 0; // game continues
	}

	public void log( string path){

		StreamWriter log = new StreamWriter ( path);
		log.Write ("");
		log.Close ();

		log = new StreamWriter ( path, true);

		log.WriteLine ("Starting Position : " + player_position.ToString ());
		log.WriteLine ("Goal points : ");
		foreach( Vector3 goal in goal_positions)
			log.WriteLine ("\t" + goal.ToString());
		log.WriteLine ("Choices :");
		foreach (Vector2 choice in choices)
			log.WriteLine ("\t"+choice.ToString ());
		
		log.WriteLine (puzzle.ToString ());
		log.Close ();
	}

	public int GetDisplayUpcomingPath()
	{
		return puzzle.display_upcoming_path;
	}

	public void SetDisplayUpcomingPath(int choice)
	{
		if (choice == 0 || choice == 1)
			puzzle.display_upcoming_path = choice;
	}

    public int GetDisplayPastPaths()
    {
		return puzzle.display_past_paths;
    }

    public void SetDisplayPastPaths(int choice)
    {
        if (choice == 0 || choice == 1)
			puzzle.display_past_paths = choice;
    }

}
