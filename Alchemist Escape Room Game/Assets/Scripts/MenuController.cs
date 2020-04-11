using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour{
    public static MenuController Instance;
    public Canvas canvas; 
    public GameObject baseMenuGroup;
    public GameObject saveMenuGroup;
    public GameObject loadMenuGroup;

    private Button saveButton;
    private Button loadButton;
    private Button mainMenuButton;
    private Button closeMenuButton;

    private List<GameObject> saveSlots = new List<GameObject>();
    private Button saveMenuReturnButton;

    private List<GameObject> loadSlots = new List<GameObject>();
    private Button loadMenuReturnButton;


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
        foreach (Transform t in saveMenuGroup.transform){
            children.Add(t.gameObject);
        }
        for(int i=0;;i++){
            if(children[i].name == "ReturnButton"){
                saveMenuReturnButton = children[i].GetComponent<Button>();
                break;
            }
            else saveSlots.Add(children[i]);
        }

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

        // Add listeners
        saveButton.onClick.AddListener(OpenSaveMenu);
        loadButton.onClick.AddListener(OpenLoadMenu);
        mainMenuButton.onClick.AddListener(SetSceneMainMenu);
        closeMenuButton.onClick.AddListener(CloseMenu);
        saveMenuReturnButton.onClick.AddListener(delegate{ReturnToMenu(0);});
        loadMenuReturnButton.onClick.AddListener(delegate{ReturnToMenu(1);});

        for(int i=1; i<=saveSlots.Count; i++){
            int slotID = i;
            saveSlots[i-1].GetComponent<Button>().onClick.AddListener(delegate{TriggerSave(slotID);});
            loadSlots[i-1].GetComponent<Button>().onClick.AddListener(delegate{TriggerLoad(slotID);});
        }
        loadSlots[loadSlots.Count-1].GetComponent<Button>().onClick.AddListener(delegate{TriggerLoad(0);});
    }


    public void OpenMenu(){
        // Set text for save and load menu
        for(int i=1; i<=saveSlots.Count; i++){
            if(!File.Exists(GameMaster.Instance.saveLocation + "save" + i + ".json")){
                saveSlots[i-1].GetComponentInChildren<Text>().text = "Save to slot " + i;
                loadSlots[i-1].GetComponentInChildren<Text>().text = "(no savedata)";
            }
            else{
                saveSlots[i-1].GetComponentInChildren<Text>().text = "Overwrite save in slot " + i;
                loadSlots[i-1].GetComponentInChildren<Text>().text = "Load from slot " + i;
            }
        }
        if(!File.Exists(GameMaster.Instance.saveLocation + "autosave.json")){
            loadSlots[loadSlots.Count-1].GetComponentInChildren<Text>().text = "(no data)";
        }
        else{
            loadSlots[loadSlots.Count-1].GetComponentInChildren<Text>().text 
            = "Load from autosave slot";
        }
            

        GameMaster.Instance.menuOpen = true;
        EnableCanvasGroup(baseMenuGroup);
        DisableCanvasGroup(saveMenuGroup);
        DisableCanvasGroup(loadMenuGroup);
        canvas.enabled = true;
    }

    public void ReturnToMenu(int id){
        if(id==0) DisableCanvasGroup(saveMenuGroup);
        else DisableCanvasGroup(loadMenuGroup);
        EnableCanvasGroup(baseMenuGroup);
    }

    public void CloseMenu(){
        GameMaster.Instance.menuOpen = false;
        canvas.enabled = false;
    }

    void OpenSaveMenu(){
        DisableCanvasGroup(baseMenuGroup);
        EnableCanvasGroup(saveMenuGroup);
    }

    void OpenLoadMenu(){
        DisableCanvasGroup(baseMenuGroup);
        EnableCanvasGroup(loadMenuGroup);
    }


    void TriggerSave(int id){
        Debug.Log("Saving slot: " + id);
        GameMaster.Instance.Save(id);
        OpenMenu();
    }
    void TriggerLoad(int id){
        Debug.Log("Loading slot: " + id);
        if(id == loadSlots.Count) id = 0;
        GameMaster.Instance.Load(id);
    }
    void SetSceneMainMenu(){
        GameMaster.Instance.StartScene(0);
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
