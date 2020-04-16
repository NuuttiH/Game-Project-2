using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour{
    public static GameMaster Instance;
    public AudioSource musicPlayer;
    public int sceneNumber;
    public Vector3 startLocation;
    public float camHeight; // 0
    public Texture2D cursorTexture;
    public Vector2 cursorFix = Vector2.zero;
    
    [HideInInspector]
    public int puzzleOpen;  // 0 = no puzzle, 1 = PuzzleCombine1, 2 = PuzzleCombine2
                            // 3 = CombinationLock, 4 = Spellbook
    [HideInInspector]
    public bool menuOpen;
    [HideInInspector]
    public bool imageZoomOpen;

    public Item emptyItem;
    public Item emptyUIItem;

    [Header("Inventory")]
    [HideInInspector]
    public InventoryManager inventoryManager;
    [HideInInspector]
    public bool inventoryOpen;
    [HideInInspector]
    public int inventoryOffset;

    public List<Item> items = new List<Item>();
    public List<Item> hiddenItems = new List<Item>();
    
    [Header("Size of dialogue memory (EDIT PREFAB)")]
    public int dialogueMemorySize = 100;
    [HideInInspector]
    public bool[] dialogueMemory;

    public int eventCount = 2;
    [HideInInspector]
    public bool[] eventMemory;

    [HideInInspector]
    public string saveLocation;

    private bool musicPlaying;


    void Awake(){
        if(Instance==null) Instance = this;
        else{
            Destroy(this.gameObject);
            return;
        }
        
        DontDestroyOnLoad(this.gameObject);

        saveLocation = Application.dataPath + "/Saves/";
        if(!Directory.Exists(saveLocation)) Directory.CreateDirectory(saveLocation);

        inventoryOpen = false;
        inventoryOffset = 0;
        menuOpen = false;
        imageZoomOpen = false;

        dialogueMemory = new bool[dialogueMemorySize];
        eventMemory = new bool[eventCount];
        musicPlaying = false;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Mouse1)){
            if(menuOpen){
                MenuController.Instance.CloseMenu();
            }
            else if(imageZoomOpen){
                ImageZoom.Instance.CloseImageZoom();
            }
            else if(puzzleOpen!=0){
                switch(puzzleOpen){
                    case 1:
                        PuzzleCombine1Controller.Instance.ClosePuzzle();
                        break;
                    case 2:
                        PuzzleCombine2Controller.Instance.ClosePuzzle();
                        break;
                    case 3:
                        CombinationLockController.Instance.ClosePuzzle();
                        break;
                    case 4:
                        SpellbookController.Instance.ClosePuzzle();
                        break;
                    default:
                        break;
                }
            }
        }
        else if(Input.GetKeyDown(KeyCode.Escape)){
            if(puzzleOpen!=0){
                switch(puzzleOpen){
                    case 1:
                        PuzzleCombine1Controller.Instance.ClosePuzzle();
                        break;
                    case 2:
                        PuzzleCombine2Controller.Instance.ClosePuzzle();
                        break;
                    case 3:
                        CombinationLockController.Instance.ClosePuzzle();
                        break;
                    case 4:
                        SpellbookController.Instance.ClosePuzzle();
                        break;
                    default:
                        break;
                }
            }
            else if(imageZoomOpen){
                ImageZoom.Instance.CloseImageZoom();
            }
            else{
                if(menuOpen){
                    MenuController.Instance.CloseMenu();
                }
                else{
                    MenuController.Instance.OpenMenu();
                }
            }
        }
    }


    public void StartScene(int newSceneNumber){
        sceneNumber = newSceneNumber;
        StartCoroutine(StartSceneCoroutine());
    }
    IEnumerator StartSceneCoroutine(){
        Transition.Instance.FadeOut();
        yield return new WaitForSeconds(1.5f);
        if(sceneNumber<2){
            musicPlayer.Stop();
            musicPlaying = false;
        }
        SceneManager.LoadScene(sceneNumber);
        yield return new WaitForSeconds(0.1f);
        Debug.Log("New scene loaded");

        if(sceneNumber>1 && !musicPlaying){
            musicPlayer.Play();
        }
    }

    public void PickupItem(Item item){
        // Assume inventory is large enough to hold all items
        Debug.Log("Picked up " + item.name);
        if(!items.Find(x => x.name==item.name)){
            items.Add(item);
            inventoryManager.DrawInventory();
            Save(0); // Autosave
        }
    }

    public void RemoveItem(Item item){
        // Add item to hideItem to prevent it from being displayed
        Debug.Log("Hiding item " + item.name);
        if(items.Find(x => x.name==item.name)){
            items.Remove(item);
            hiddenItems.Add(item);
            inventoryManager.DrawInventory();
        }
    }

    public void Save(int saveID){
        string currentSaveLocation = saveLocation;
        if(saveID==0) currentSaveLocation += "autosave.json";
        else currentSaveLocation += "save" + saveID + ".json";

        Debug.Log("Writing save...");

        float playerLocationX = GameObject.FindGameObjectWithTag("Player").transform.position.x;

        List<string> itemsByName = new List<string>();
        foreach(Item item in items){
            Debug.Log("Saving item:  (" + item.name + ")");
            itemsByName.Add(item.name);
        }
        List<string> hiddenItemsByName = new List<string>();
        foreach(Item item in hiddenItems){
            Debug.Log("Saving hidden item:  (" + item.name + ")");
            hiddenItemsByName.Add(item.name);
        }

        SaveObject saveObject = new SaveObject{
            playerLocationX = playerLocationX,
            sceneNumber = sceneNumber,
            itemsByName = itemsByName,
            hiddenItemsByName = hiddenItemsByName,
            dialogueMemory = dialogueMemory,
            eventMemory = eventMemory
        };

        string saveString = JsonUtility.ToJson(saveObject);
        File.WriteAllText(currentSaveLocation, saveString);
        Debug.Log("Save written!");
    }

    public void Load(int saveID){
        string currentSaveLocation = saveLocation;
        if(saveID==0) currentSaveLocation += "autosave.json";
        else currentSaveLocation += "save" + saveID + ".json";

        if(File.Exists(currentSaveLocation)){
            Debug.Log("Loading file...  (" + currentSaveLocation + ")");
            string saveString = File.ReadAllText(currentSaveLocation);
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);

            items = new List<Item>();
            foreach(string itemName in saveObject.itemsByName){
                Debug.Log("Loading item:  (Items/" + itemName + ")");
                items.Add(Resources.Load<Item>("Items/" + itemName));
            }

            hiddenItems = new List<Item>();
            foreach(string itemName in saveObject.hiddenItemsByName){
                Debug.Log("Loading item:  (Items/" + itemName + ")");
                hiddenItems.Add(Resources.Load<Item>("Items/" + itemName));
            }
            // Inventory Drawing managed by InventoryManager.Start()
            // Picked up item removal handled by InteractiveObject.Start()
            dialogueMemory = saveObject.dialogueMemory;
            eventMemory = saveObject.eventMemory;  
            // Event loading managed by GameEventHandler.Start()
            startLocation = new Vector3(saveObject.playerLocationX, camHeight, 0f);

            StartScene(saveObject.sceneNumber);
        }
        else{
            Debug.Log("Save file not found!");
        }
    }

    private class SaveObject{
        public float playerLocationX;
        public int sceneNumber;

        public List<string> itemsByName = new List<string>();
        public List<string> hiddenItemsByName = new List<string>();

        public bool[] dialogueMemory;
        public bool[] eventMemory;
    }
}
