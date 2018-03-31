using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

/* Author : Nate Cortes
 * Creates the  settings menu screen through script. Made for CPI441 capstone. 
 */

public class gui_settings : MonoBehaviour {

	public GUISkin style_skin;
	public Texture lego_brick;
	private float brick_height, brick_width;
	private int resolution_location = 0;
	private bool fullscreen = false;

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

		GUI.Label (gui_mod, "Settings");

		gui_mod.y += gui_mod.height / 4.0f + gui_mod.height;

		if( GUI.Button( gui_mod, fullscreen ? "Fullscreen" : "Windowed")){
			fullscreen = !fullscreen;
		}

		//draw_bricks (gui_mod);

		gui_mod.y += gui_mod.height / 4.0f + gui_mod.height;

		float original_width = gui_mod.width;
		float original_x = gui_mod.x;

		gui_mod.width = original_width / 4.0f;

		if (GUI.Button (gui_mod, "<")) { 
			resolution_location = (resolution_location - 1);
			if( resolution_location < 0){
				resolution_location = ( fullscreen ? 
					resolutionManager.FullscreenResolutions.Count : 
					resolutionManager.WindowedResolutions.Count) + resolution_location;
			}
		}

		gui_mod.x += original_width / 4.0f;
		gui_mod.width = original_width  *  2.0f / 4.0f;

		if (GUI.Button (gui_mod,
			( fullscreen ? 
				res_format( resolutionManager.FullscreenResolutions[ resolution_location]) : 
				res_format( resolutionManager.WindowedResolutions[ resolution_location]))
			)
		) { }

		gui_mod.x +=  original_width  *  2.0f / 4.0f;
		gui_mod.width = original_width / 4.0f;

		if (GUI.Button (gui_mod, ">")) { 
			resolution_location = (resolution_location + 1) % 
				( fullscreen ? 
					resolutionManager.FullscreenResolutions.Count : 
					resolutionManager.WindowedResolutions.Count);
		}

		gui_mod.x = original_x;
		gui_mod.width = original_width;

		////////draw_bricks (gui_mod);

		gui_mod.y += gui_mod.height / 4.0f + gui_mod.height;

		if( GUI.Button( gui_mod, "Resolution: " + Screen.currentResolution.width+ "x"+Screen.currentResolution.height + " ("+(Screen.fullScreen ? "Full" : "Win") + ")")){	}

		//////draw_bricks (gui_mod);

		gui_mod.y += gui_mod.height / 4.0f + gui_mod.height;

		original_width = gui_mod.width;
		original_x = gui_mod.x;

		gui_mod.width = original_width / 2.0f;

		if( GUI.Button( gui_mod, "Main Menu")){
			SceneManager.LoadScene ("menu_scene");	
		}

		gui_mod.x = original_x + original_width / 2.0f;

		GUI.DrawTexture (gui_mod, lego_brick);

		if( GUI.Button( gui_mod, "Apply Changes")){
			resolutionManager.SetResolution (resolution_location, fullscreen);	
		}

		gui_mod.x = original_x;
		gui_mod.width = original_width;

		//////draw_bricks (gui_mod);

		GUI.EndGroup ();
	}

	private string res_format( Vector2 rese){//formats, e.x. 1920x1080
		return rese.x + "x" + rese.y;
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

	}
}
