using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameConstants : MonoBehaviour {

	static GameConstants Instance;

    public static bool locked = true;                   // For determining whether the player is locked to the grid or not

	static public string version = "";
	static public int GridSpacing = 1;					//Grid Spacing on the Game Board
	static public float Height = 2.5f;					//Y Value of Player and Goal
	static public float Speed = 5.0f;					//Speed of player
	static public Vector2[] BaseVectors = new Vector2[9]{//The Basic Vectors that the game will use to create puzzles
		new Vector3 (0, 1),//initialize base vector
		new Vector3 (1, 0),//all vectors should point to first quandrant ( positive values)
		new Vector3 (1, 1),
		new Vector3 (1, 2),
		new Vector3 (2, 1),
		new Vector3 (1, 3),
		new Vector3 (3, 1),
		new Vector3 (2, 3),
		new Vector3 (3, 2),
	}; 

	// Use this for initialization
	void Awake () {
		Instance = this;
	}

	public void load_menu(){
		SceneManager.LoadScene ("menu_scene");
	}

    public void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
