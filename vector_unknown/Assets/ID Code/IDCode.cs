using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IDCode : MonoBehaviour
{
    private string code = "None Given";
    public InputField text;

    public void go()
    {
        code = text.text;
        Psychometrics.logEvent(code);
        SceneManager.LoadScene("menu_scene");
    }
}
