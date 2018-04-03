using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gui_BasicText : MonoBehaviour {

    public TextAsset MyText;

    // Use this for initialization
    void Start () {
        GetComponent<Text>().text = ReadTextAsset();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    string ReadTextAsset()
    {
        string text = MyText.text;
        return text;
    }
}
