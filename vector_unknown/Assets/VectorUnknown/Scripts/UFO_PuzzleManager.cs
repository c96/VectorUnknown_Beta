using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UFO_PuzzleManager : MonoBehaviour {
	/******************/ 
	/* Create a Singleton instance */
	/******************/ 
	static public UFO_PuzzleManager instance;

	void Awake(){
		instance = this;
	}

	/******************/ 
	/* Game Constants */
	/******************/ 
	public GameObject Player;
	public GameObject Goal;
	public Vector3 GoalPosition;
	public Vector2[] Choices = new Vector2[4];			//Container for Vectors in the form used for UI Text
	public Vector2 Solution;
	public int number_attempts = -1;

	private int GameMode;

	public System.Random rnd = new System.Random ();
	public GameObject SuccessWarningGameObject;
	public GameObject FailureWarningGameObject;
	//public UFO_UIManager UI_Manager;
	public GameObject choice_panel; 
	public GameObject solution_text;

	public GameObject puzzle_info;

	// Use this for initialization
	void Start () {

		//UI_Manager = GetComponent< UFO_UIManager> ();

		choice_panel = GameObject.FindGameObjectWithTag ("Choices");
		solution_text = GameObject.FindGameObjectWithTag ("Solution");


		SetWarningsFalse ();
		
		GameMode = 0;
		ResetGame (); //Set Up Game Board
		NextPuzzle (); //Create 1st Puzzle
	}

	void SetWarningsFalse ()
	{
		SuccessWarningGameObject.SetActive (false);
		FailureWarningGameObject.SetActive (false);
	}

	void Update(){
		if( Input.GetKeyDown( KeyCode.Space)){
			ResetGame ();
			NextPuzzle ();
		}

		if (Input.GetKeyDown(KeyCode.F))
			puzzle_info.GetComponent<puzzle_info>().setFutureSight(1);
		if (Input.GetKeyDown(KeyCode.G))
			puzzle_info.GetComponent<puzzle_info>().setFutureSight(0);
	}


	public void NextPuzzle () {
		int[] Num = new int[2];					//Container for the random index numbers of the GameConstants.BaseVectors array

		// Randomly chooses two vectors from the GameConstants.BaseVectors array and stores them in the Choices array.
		int l = GameConstants.BaseVectors.Length;            // Num[i]= Random index number of GameConstants.BaseVectors array
		Num [0] = rnd.Next (0, l);             // Num[i] is used to select random vector
		if (Num [0] <= 1) {					   // If Num[0] --> <0,1> or <1,0>, then Num[1] should not be a duplicate of Num[0]
			Num [1] = rnd.Next (0, l - 1);     // So Num[1] should be selected from among l-1 index numbers
			if (Num [1] >= Num [0]) Num [1]++; // Adjust Num[1] depending on the value of Num[0], so that Num[0]!=Num[1] and 0<=Num[1]<=l
		}else Num [1] = rnd.Next (0, l);		 // If Num[0] !-> <0,1> or <1,0>, then Num[1] can be equal to Num[2]

		Choices [0] = GameConstants.BaseVectors [Num [0]]; // Choices[0]= First Random Vector (Will 	Eventually Become First Solution Vector)
		Choices [1] = GameConstants.BaseVectors [Num [1]]; // Choiecs[1]= Second Random Vector (Will Eventually Become Second Solution Vector)

		// Randomly changes the directions of the vectors in the Choices array, so that they point to different quadrants.
		int quad_1 = rnd.Next (1, 5); // Quad[0]= Random quadrant for the first solution vector, can be any quadrant 1-4
		int quad_2 = rnd.Next (1, 3); // Quad[1]= Random quadrant for the second solution vector, must be adjacent to Quad[0]
		enforce_quadrants( quad_1, quad_2);
			
		if (GameMode == 0) {// Calculate Solution and Goal Positions
			Solution = generate_solution( Num);
			GoalPosition= new Vector3 (Solution.x, GameConstants.Height / GameConstants.GridSpacing, Solution.y) * GameConstants.GridSpacing;
		} 

		//shuffle choices for random display in UI
		shuffle (Choices);
		string log_path = Application.dataPath + "/puzzle_manager_logfile.txt";
		log ( log_path);

		Goal.transform.position = new Vector3 (Solution.x, GameConstants.Height, Solution.y);

		update_choices ();
		update_solution ();
	}

	public void ResetGame () {

		Player.transform.position = new Vector3 (0, GameConstants.Height, 0); //Initialize Player Position
		if (GameMode == 0) {
			Solution = new Vector2 (0, 0);
			Goal.transform.position = new Vector3 (0, GameConstants.Height, 0);//GoalPosition
		}

	}

	/* First Puzzle Algorithms */
	public void shuffle( Vector2[] arr){
		for (int i = 0; i < arr.Length; i++) {
			Vector2 temp = arr [i];
			int r_index = Random.Range (0, arr.Length);
			arr [i] = arr [r_index];
			arr [r_index] = temp;
		}
	}

	public Vector2 generate_solution( int[] Num){
		int[] Mul = new int[2];	//Container for the random constant that extends the length of the vectors
		// Adds two new vectors to the Choices array that are proportional to the first two vectors
		// i.e. Choices[0] and Choices[2] are linearly dependent 
		//      Choices[1] and Choices[3] are linearly dependent 
		for (int i = 0; i < 2; i++) {			
			if (Num [i] <= 2) {              // If the vector is <1,1>, <1,0>, or <0,1>, then the Mul[i] can be from 2-10, 
				Mul [i] = rnd.Next (2, 11);  // Mul[i] = constant that will determine goal placement -- Max: <10,10>, <10,0>, <0,10>
				if (Mul [i] == 6) {          // To keep the vectors "nice", only certain constants can multiply Choices
					int n = rnd.Next (2, 5); // If Mul[i]=6, Choices can be multiplied by 2,3,6 (Since 6 is divisible by 2,3,6)
					if (n == 4) n = 6;       // 4 Maps to 6
					Choices [i + 2] = Choices [i] * n; // Add vector that is linearly dependent with Choices[i]
				} 
				else if (Mul [i] == 10) {    // If Mul[i]=10, Choices can be multiplied by 2,3,10
					int n = rnd.Next (2, 5); // Since 10 is divisible by 2,3,10
					if (n == 3) n = 5;       // 3 maps to 5
					if (n == 4) n = 10;      // 4 maps to 10
					Choices [i + 2] = Choices [i] * n; // Add vector that is linearly dependent with Choices[i]
				} 
				else if (Mul [i] == 4) Choices [i + 2] = Choices [i] * Mathf.Pow (2, rnd.Next (1, 3)); //4 is divisible by 2 and 4
				else if (Mul [i] == 8) Choices [i + 2] = Choices [i] * Mathf.Pow (2, rnd.Next (1, 4)); //8 is divisible by 2, 4, and 8
				else if (Mul [i] == 9) Choices [i + 2] = Choices [i] * Mathf.Pow (3, rnd.Next (1, 3)); //9 is divisible by 3 and 9
				else Choices [i + 2] = Choices [i] * Mul [i]; // All other possible numbers are only divisible by themselves
			} 
			else if (Num [i] <= 4) {       // If the vector is <1,2> or <2,1>
				Mul [i] = rnd.Next (2, 6); // Then Mul[i] can be 2-5, i.e. <5,10> and <10,5> are max
				if (Mul [i] == 4) Choices [i + 2] = Choices [i] * Mathf.Pow (2, rnd.Next (1, 3)); //4 is divisible by 2 and 4
				else Choices [i + 2] = Choices [i] * Mul [i]; // Add vector that is linearly dependent with Choices[i]
			} 
			else {						   // If the vector is <1,3>, <3,1>, <2,3>, or <3,2>
				Mul [i] = rnd.Next (2, 4); // Then Mul[i] can be 2-3, i.e. <3,9>, <9,3>, <6,9>, and <9,6>
				Choices[i+2] = Choices [i] * Mul [i]; // Add vector that is linearly dependent with Choices[i]
			}
			int s = rnd.Next (0, 2);           //Randomly change the sign of the new vectors
			if (s == 0) Choices [i + 2] *= -1; //0 --> sign change, 1 --> no sign change
		}

		return Mul [0] * Choices [0] + Mul [1] * Choices [1];
	}

	public void enforce_quadrants( int quad_1, int quad_2){
		if (quad_1 == 1 || quad_1 == 3) quad_2 *= 2; // Quad[0] = 1 or 3  -->  Quad[1] = 2 or 4
		else quad_2 = 2 * quad_2 - 1;				 // Quad[0] = 2 or 4  -->  Quad[1] = 1 or 3          // All vectors initially point to quadrant 1

		if (quad_1 == 2) Choices [0].x *= -1; // Change the sign of the x component, vector now points to quadrant 2
		if (quad_1 == 3) Choices [0] *= -1;   // Change the sign of the both components, vector now points to quadrant 3
		if (quad_1 == 4) Choices [0].y *= -1; // Change the sign of the y component, vector now points to quadrant 4

		if (quad_2 == 2) Choices [1].x *= -1; // Change the sign of the x component, vector now points to quadrant 2
		if (quad_2 == 3) Choices [1] *= -1;   // Change the sign of the both components, vector now points to quadrant 3
		if (quad_2 == 4) Choices [1].y *= -1; // Change the sign of the y component, vector now points to quadrant 4
	}

	/* End of First Puzzle Algorithms */

	public void ChangeGameMode (){

		//GameMode = 1 - GameMode;

	}

	public void update_choices( ){
		for (int i = 0; i < 4; i++) {
			choice_panel.transform.GetChild (i).transform.GetChild(0).GetComponent< choice_holder> ().update_choice (Choices [i]);
		}
	}

	public void update_solution(){
		solution_text.GetComponent< Text> ().text = "Goal : < " + Solution.x.ToString () + " ," + Solution.y.ToString () + ">";
	}

	public void decrement_attempts(){
		number_attempts--;
	}

	public void set_attempts( int num){
		number_attempts = num;
	}

	public string game_details(){
		Vector3 player_pos = Player.transform.position;
		Vector3 goal_pos = Goal.transform.position;

		return "Player Position: <" + player_pos.x.ToString ("0") + ", " + player_pos.z.ToString ("0") + ">\nGoal: <" + goal_pos.x.ToString ("0") + ", " + goal_pos.z.ToString ("0") + ">\n" + "Attempts: " + number_attempts;
	}

	public void log( string path){

		StreamWriter log = new StreamWriter ( path);
		log.Write ("");
		log.Close ();

		log = new StreamWriter ( path, true);

		log.WriteLine ("grid spacing : " + GameConstants.GridSpacing.ToString ());
		log.WriteLine ("height : " + GameConstants.Height.ToString ());
		log.WriteLine ("player object name: " + Player.name);
		log.WriteLine ("\t" + Player.transform.position.ToString());
		log.WriteLine ("Goal object name: " + Goal.name);
		log.WriteLine ("\t" + Goal.transform.position.ToString());
		log.WriteLine ("Choices :");
		foreach (Vector2 choice in Choices)
			log.WriteLine ("\t"+choice.ToString ());
		log.WriteLine ("Solution: " + Solution.ToString ());
		log.WriteLine ("Goal Positions :");
		log.WriteLine ("\t"+GoalPosition.ToString ());
		log.Close ();
	}

	public void TestSuccess(Vector3 endPosition)
	{
		bool succeeded = false;

		Vector2 endPositionVector2 = new Vector2 (endPosition.x, endPosition.z);

		Vector2 goalVector2 = new Vector2(GoalPosition.x, GoalPosition.z);

		if (goalVector2 == Vector2.zero) {

		} 
		//else if (goalVector2 == Solution) {
		//	succeeded = true;
		//}
		else if (goalVector2 == endPositionVector2) {
			succeeded = true;
		}

		if (succeeded) {
			SuccessWarningGameObject.SetActive (true);
		} else {
			FailureWarningGameObject.SetActive (true);
		}

	}
}
