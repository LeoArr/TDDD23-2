using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menu_controller : MonoBehaviour {

    public Button play;
    public Button settings;
    public Button exit;

    void PlayButton() {
        print("start game");
        SceneManager.LoadScene("simple_scene");

    }
    void SettingsButton()
    {
        //ska dene ens finnas? lul
        print("enter settings");

    }
    void ExitButton()
    {

        print("exit game");
        Application.Quit();

    }
    // Use this for initialization
    void Start () {
        print("starting menu");

        GameObject.FindWithTag("PlayButton").GetComponent<Button>().onClick.AddListener(PlayButton);
        GameObject.FindWithTag("SettingsButton").GetComponent<Button>().onClick.AddListener(SettingsButton);
        GameObject.FindWithTag("ExitButton").GetComponent<Button>().onClick.AddListener(ExitButton);
    }

    // Update is called once per frame
    void Update () {

	}
}
