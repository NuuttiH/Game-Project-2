using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour{
    public GameObject baseMenuGroup;
    public GameObject loadMenuGroup;

    private Button startButton;
    private Button loadButton;
    private Button exitButton;

    private List<GameObject> loadSlots = new List<GameObject>();
    private Button loadMenuReturnButton;


    void Start(){
        // Cache private variables
        List<GameObject> children = new List<GameObject>();
        foreach (Transform t in baseMenuGroup.transform){
            children.Add(t.gameObject);
        }
        startButton = children[0].GetComponent<Button>();
        loadButton = children[1].GetComponent<Button>();
        exitButton = children[2].GetComponent<Button>();

        children = new List<GameObject>();
        foreach (Transform t in loadMenuGroup.transform){
            children.Add(t.gameObject);
        }
        for(int i=0;;i++){
            if(children[i].name == "ReturnButton"){
                loadMenuReturnButton = children[i].GetComponent<Button>();
                break;
            }
            else loadSlots.Add(children[i]);
        }

        // Add listeners and adjust text for load menu
        startButton.onClick.AddListener(StartGame);
        loadButton.onClick.AddListener(DisplayLoadMenu);
        exitButton.onClick.AddListener(ExitGame);
        loadMenuReturnButton.onClick.AddListener(DisplayMainMenu);

        for(int i=1; i<loadSlots.Count; i++){
            int slotID = i;
            loadSlots[i-1].GetComponent<Button>().onClick.AddListener(delegate{TriggerLoad(slotID);});
            
            if(!File.Exists(GameMaster.Instance.saveLocation + "save" + i + ".json")){
                loadSlots[i-1].GetComponentInChildren<Text>().text = "(no save data)";
            }
            else{
                loadSlots[i-1].GetComponentInChildren<Text>().text = "Load from slot " + i;
            }
        }
        loadSlots[loadSlots.Count-1].GetComponent<Button>().onClick.AddListener(delegate{TriggerLoad(0);});
        
        if(!File.Exists(GameMaster.Instance.saveLocation + "autosave.json")){
            loadSlots[loadSlots.Count-1].GetComponentInChildren<Text>().text = "(no autosave data)";
        }
        else{
            loadSlots[loadSlots.Count-1].GetComponentInChildren<Text>().text 
            = "Load from autosave slot";
        }
    }


    public void StartGame(){
        Debug.Log("Loading scene 'Room1'");
        GameMaster.Instance.StartScene(1);
    }
    public void ExitGame(){
        Debug.Log("Game is exiting...");
        Application.Quit();
    }

    void DisplayMainMenu(){
        DisableCanvasGroup(loadMenuGroup);
        EnableCanvasGroup(baseMenuGroup);
    }

    void DisplayLoadMenu(){
        DisableCanvasGroup(baseMenuGroup);
        EnableCanvasGroup(loadMenuGroup);
    }

    void TriggerLoad(int id){
        Debug.Log("Loading slot: " + id);
        if(id == loadSlots.Count) id = 0;
        GameMaster.Instance.Load(id);
    }

    private void EnableCanvasGroup(GameObject gameObject){
        CanvasGroup canvasGroup = gameObject.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
    private void DisableCanvasGroup(GameObject gameObject){
        CanvasGroup canvasGroup = gameObject.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
