using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_InfoController : MonoBehaviour {

    public GameObject InfoOverlay;
	public TextAsset controls, success, failure;

	void Awake(){
		HideInfoOverlay ();
	}

    public void ShowInfoOverlay() // Instructions Panel
    {
		InfoOverlay.GetComponentInChildren< Text> ().text = controls.text;
        InfoOverlay.SetActive(true);
    }

	public void ShowSuccessOverlay()// Success Panel
    {
		InfoOverlay.GetComponentInChildren< Text> ().text = success.text;
		InfoOverlay.SetActive(true);
    }

    public void ShowFailureOverlay()// Failure Panel
    {
		InfoOverlay.GetComponentInChildren< Text> ().text = failure.text;
		InfoOverlay.SetActive(true);
    }
		
	public void HideInfoOverlay() // Resets panel contents and tucks it away
	{
		InfoOverlay.GetComponentInChildren< Text> ().text = "";
		InfoOverlay.SetActive(false);
	}
}
