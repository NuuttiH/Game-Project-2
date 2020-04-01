using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour{
    public static GameMaster Instance;
    public Vector3 startLocation;
    
    public float objectActivationDistance = 3.8f;

    [HideInInspector]
    public int puzzleOpen; // 0 = no puzzle, 1 = PuzzleCombine, ...

    public Item emptyItem;

    [Header("Inventory")]
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
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start(){
        inventoryOpen = false;
        inventoryOffset = 0;

        dialogueMemory = new bool[dialogueMemorySize];
        eventMemory = new bool[GameEventHandler.Instance.eventCount];

        PuzzleCombine1Controller.Instance.ClosePuzzle();
        PuzzleCombine2Controller.Instance.ClosePuzzle();
        PuzzleCombinationLockController.Instance.ClosePuzzle();
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
