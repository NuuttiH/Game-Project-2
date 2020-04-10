using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour{
    public Button startButton;
    public Button loadButton;
    public Button exitButton;

    void Start(){
        startButton.onClick.AddListener(StartGame);
        loadButton.onClick.AddListener(DisplayLoadMenu);
        exitButton.onClick.AddListener(ExitGame);
    }

    public void StartGame(){
        Debug.Log("Loading scene 'Room1'");
        GameMaster.Instance.StartScene(1);
    }
    public void ExitGame(){
        Debug.Log("Game is exiting...");
        Application.Quit();
    }
    public void DisplayLoadMenu(){
        Debug.Log("Not impelented yet");
    }
    public void DisplayMainMenu(){

    }
}
