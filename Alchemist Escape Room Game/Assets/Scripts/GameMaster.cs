using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour{
    public static GameMaster Instance;
    public int sceneNumber;
    public Vector3 startLocation;
    
    [HideInInspector]
    public int puzzleOpen; // 0 = no puzzle, 1 = PuzzleCombine, ...

    public Item emptyItem;

    [Header("Inventory")]
    [HideInInspector]
    public InventoryManager inventoryManager;
    [HideInInspector]
    public bool inventoryOpen;
    [HideInInspector]
    public int inventoryOffset;

    public List<Item> items = new List<Item>();
    
    [Header("Size of dialogue memory")]
    public int dialogueMemorySize = 100;
    [HideInInspector]
    public bool[] dialogueMemory;

    [HideInInspector]
    public bool[] eventMemory;


    void Awake(){
        if(Instance==null) Instance = this;
        else{
            Destroy(this.gameObject);
            return;
        }
        
        DontDestroyOnLoad(this.gameObject);
    }

    void Start(){
        inventoryOpen = false;
        inventoryOffset = 0;

        dialogueMemory = new bool[dialogueMemorySize];
        eventMemory = new bool[GameEventHandler.Instance.eventCount];
    }

    public void LoadGame(){
        GameMaster.Instance.sceneNumber = 1;
        SceneManager.LoadScene("Intro");
    }

    public void PickupItem(Item item){
        // Assume inventory is large enough to hold all items
        Debug.Log("Picked up " + item.name);
        if(!items.Find(x => x.name==item.name)){
            items.Add(item);
            inventoryManager.DrawInventory();
        }
    }

    public void RemoveItem(Item item){
        Debug.Log("Removing " + item.name);
        items.Remove(item);
        inventoryManager.DrawInventory();
    }
}
