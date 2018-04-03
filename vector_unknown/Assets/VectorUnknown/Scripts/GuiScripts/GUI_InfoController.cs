using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_InfoController : MonoBehaviour {

    static GUI_InfoController Instance;

    public GameObject InfoOverlay;
    public GameObject SuccessOverlay;
    public GameObject FailureOverlay;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowInfoOverlay()
    {
        InfoOverlay.SetActive(true);
    }

    public void ShowSuccessOverlay()
    {
        SuccessOverlay.SetActive(true);
    }

    public void ShowFailureOverlay()
    {
        FailureOverlay.SetActive(true);
    }

    public void HideInfoOverlay()
    {
        InfoOverlay.SetActive(false);
    }

    public void HideSuccessOverlay()
    {
        SuccessOverlay.SetActive(false);
    }

    public void HideFailureOverlay()
    {
        FailureOverlay.SetActive(false);
    }

    void Awake()
    {
        Instance = this;
    }
}
