using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUI_InfoController : MonoBehaviour {

    public GameObject InfoOverlay;
	public TextAsset controls, success, failure;

	void Awake(){
		ShowInfoOverlay ();
	}

    public void ShowInfoOverlay() // Instructions Panel
    {
		InfoOverlay.GetComponentInChildren< Text> ().text = controls.text;

		Button overlay_button =	InfoOverlay.GetComponentInChildren< Button> ();
		overlay_button.onClick.RemoveAllListeners ();
		overlay_button.onClick.AddListener( HideInfoOverlay);

        InfoOverlay.SetActive(true);
    }

	public void ShowSuccessOverlay()// Success Panel
    {
		InfoOverlay.GetComponentInChildren< Text> ().text = success.text;

		Button overlay_button =	InfoOverlay.GetComponentInChildren< Button> ();
		overlay_button.onClick.RemoveAllListeners ();
		overlay_button.onClick.AddListener( GameOver);

		InfoOverlay.SetActive(true);
    }

    public void ShowFailureOverlay()// Failure Panel
    {
		InfoOverlay.GetComponentInChildren< Text> ().text = failure.text;

		Button overlay_button =	InfoOverlay.GetComponentInChildren< Button> ();
		overlay_button.onClick.RemoveAllListeners ();
		overlay_button.onClick.AddListener( GameOver);

		InfoOverlay.SetActive(true);
    }
		
	public void HideInfoOverlay() // Resets panel contents and tucks it away
	{
		InfoOverlay.GetComponentInChildren< Text> ().text = "";

		Button overlay_button =	InfoOverlay.GetComponentInChildren< Button> ();
		overlay_button.onClick.RemoveAllListeners ();

		InfoOverlay.SetActive(false);
	}

	public void GameOver(){
		SceneManager.LoadScene ("level_load_scene");
	}
}
