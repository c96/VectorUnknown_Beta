using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleJSON;

public class GUI_InfoController : MonoBehaviour {

    public GameObject InfoOverlay, infoText, failText, successText, scrollText, star1, star2, star3;
    public Text successTime, successScore, successStarAnnounce;
	public TextAsset controls, success, failure;
    //public Text infoText;

    private class score_unit
    {   //holds the score for using within GUI_InfoContorller
        public int _score;
        public float _time;
        [Range(1, 3)]//one, two or three stars
        public int _stars;
    }

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

        string score = PlayerPrefs.GetString("score");
        score_unit current_score = (score_unit) JsonUtility.FromJson<score_unit>(score) as score_unit;
        Debug.Log( string.Format("{0}, {1}, {2}", current_score._score, current_score._time, current_score._stars ));

        successText.SetActive(true);
        successTime.text = string.Format("Time: {0} seconds", current_score._time);
        successScore.text = string.Format("Score: {0} pts", current_score._score);

        if (current_score._stars >= 1)
            this.star1.SetActive(true);
        if (current_score._stars >= 2)
            this.star2.SetActive(true);
        if (current_score._stars >= 3)
            this.star3.SetActive(true);
        successStarAnnounce.text = string.Format("You earned {0} stars!", current_score._stars);

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
