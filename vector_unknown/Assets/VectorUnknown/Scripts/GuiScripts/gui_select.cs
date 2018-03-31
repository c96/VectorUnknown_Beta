using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

/* Author : Nate Cortes
 * Creates the  settings menu screen through script. Made for CPI441 capstone. 
 */

public class gui_select : MonoBehaviour {

	public void load_game(){
		SceneManager.LoadScene ("VectorGame");
	}

	/*public GUISkin style_skin;
	public Texture lego_brick;
	private float brick_height, brick_width;

	void OnGUI( ){
		Rect gui_mod = gui_modified_screen_rect ();

		ResolutionManager resolutionManager = ResolutionManager.Instance;

		GUI.skin = style_skin;
		if (AspectUtility.screenHeight > 1200)
			GUI.skin.button.fontSize = 48;

		GUI.BeginGroup (AspectUtility.screenRect);

		GUI.Box (gui_mod , GameConstants.version);

		gui_mod.x += gui_mod.width / 8.0f;
		gui_mod.y += gui_mod.height / 8.0f;
		brick_height = gui_mod.height = gui_mod.height / 8.0f;
		brick_width = gui_mod.width = 3.0f * gui_mod.width / 4.0f;

		GUI.Label (gui_mod, "Level Select");

		GUI.EndGroup ();
	}

	void draw_bricks( Rect guide){//adds elements to the ends of a button rect
		Rect brick_location = new Rect();

		brick_location.height = brick_height * 1.2f;
		brick_location.width = brick_width / 8.0f;
		brick_location.x = guide.x - brick_location.width / 12f;
		brick_location.y = guide.y  - brick_location.height / 12f;

		GUI.DrawTexture (brick_location, lego_brick);

		brick_location.x += guide.width - (brick_location.width / 2);

		GUI.DrawTexture (brick_location, lego_brick);
	}

	private Rect gui_modified_screen_rect(){ //creates a partition of the screen
		Rect base_rect = AspectUtility.screenRect;

		float x_rect = base_rect.x + AspectUtility.screenWidth * ( 1f / 10f);
		float y_rect = base_rect.y + AspectUtility.screenHeight * (1f / 10f);
		float g_width = AspectUtility.screenWidth * (8f / 10f);
		float h_width = AspectUtility.screenHeight * (8f / 10f);

		return new Rect ( x_rect, y_rect, g_width, h_width);

	}*/
}
