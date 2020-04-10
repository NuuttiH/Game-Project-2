using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour{
    public static MenuController Instance;
    public Canvas canvas; 
    public GameObject baseMenuGroup;
    public GameObject savesMenuGroup;

    private Button saveButton;
    private Button loadButton;
    private Button mainMenuButton;
    private Button closeMenuButton;

    private List<GameObject> saveSlots = new List<GameObject>();
    private Button returnButton;


    void Awake(){
        Instance = this;
    }

    void Start(){
        CloseMenu();

        // Cache private variables
        List<GameObject> children = new List<GameObject>();
        foreach (Transform t in baseMenuGroup.transform){
            children.Add(t.gameObject);
        }
        saveButton = children[0].GetComponent<Button>();
        loadButton = children[1].GetComponent<Button>();
        mainMenuButton = children[2].GetComponent<Button>();
        closeMenuButton = children[3].GetComponent<Button>();

        children = new List<GameObject>();
        foreach (Transform t in savesMenuGroup.transform){
            children.Add(t.gameObject);
        }
        for(int i=0;;i++){
            if(children[i].name == "ReturnButton"){
                returnButton = children[i].GetComponent<Button>();
                break;
            }
            else saveSlots.Add(children[i]);
        }

        // Add listeners that don't need midgame modifications
        saveButton.onClick.AddListener(OpenSaveMenu);
        loadButton.onClick.AddListener(OpenLoadMenu);
        mainMenuButton.onClick.AddListener(SetSceneMainMenu);
        closeMenuButton.onClick.AddListener(CloseMenu);
        returnButton.onClick.AddListener(OpenMenu);

    }

    void Update(){
        
    }


    public void OpenMenu(){
        GameMaster.Instance.menuOpen = true;
        EnableCanvasGroup(baseMenuGroup);
        DisableCanvasGroup(savesMenuGroup);
        canvas.enabled = true;
    }

    public void CloseMenu(){
        GameMaster.Instance.menuOpen = false;
        canvas.enabled = false;
    }

    void OpenSaveMenu(){
        for(int i=0; i<saveSlots.Count; i++){
            saveSlots[i].GetComponentInChildren<Text>().text = "Save to slot " + i;
        }

        DisableCanvasGroup(baseMenuGroup);
        EnableCanvasGroup(savesMenuGroup);
    }

    void OpenLoadMenu(){
        for(int i=0; i<saveSlots.Count; i++){
            saveSlots[i].GetComponentInChildren<Text>().text = "Load from slot " + i;
        }
        DisableCanvasGroup(baseMenuGroup);
        EnableCanvasGroup(savesMenuGroup);
    }

    void SetSceneMainMenu(){
        GameMaster.Instance.StartScene(0);
    }

    public void EnableCanvasGroup(GameObject gameObject){
        CanvasGroup canvasGroup = gameObject.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
    void DisableCanvasGroup(GameObject gameObject){
        CanvasGroup canvasGroup = gameObject.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
