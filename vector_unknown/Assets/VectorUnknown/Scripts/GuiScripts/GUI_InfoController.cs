using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUI_InfoController : MonoBehaviour {

    public GameObject InfoOverlay, infoText, failText, successText, scrollText;
	public TextAsset controls, success, failure;
    //public Text infoText;

    public void ShowInfoOverlay() // Instructions Panel
    {
		//infoText.text = controls.text;

		Button overlay_button =	InfoOverlay.GetComponentInChildren< Button> ();
		overlay_button.onClick.RemoveAllListeners ();
		overlay_button.onClick.AddListener( HideInfoOverlay);

        infoText.SetActive(true);
        scrollText.SetActive(true);
        InfoOverlay.SetActive(true);
    }

	public void ShowSuccessOverlay()// Success Panel
    {
		//infoText.text = success.text;

		Button overlay_button =	InfoOverlay.GetComponentInChildren< Button> ();
		overlay_button.onClick.RemoveAllListeners ();
		overlay_button.onClick.AddListener( GameOver);

        successText.SetActive(true);
		InfoOverlay.SetActive(true);
    }

    public void ShowFailureOverlay()// Failure Panel
    {
		//infoText.text = failure.text;

		Button overlay_button =	InfoOverlay.GetComponentInChildren< Button> ();
		overlay_button.onClick.RemoveAllListeners ();
		overlay_button.onClick.AddListener( GameOver);

        failText.SetActive(true);
		InfoOverlay.SetActive(true);
    }
		
	public void HideInfoOverlay() // Resets panel contents and tucks it away
	{
		//infoText.text = "";

		Button overlay_button =	InfoOverlay.GetComponentInChildren< Button> ();
		overlay_button.onClick.RemoveAllListeners ();

        scrollText.SetActive(false);
        infoText.SetActive(false);
		InfoOverlay.SetActive(false);
	}

	public void GameOver(){
		SceneManager.LoadScene ("level_load_scene");
	}
}
