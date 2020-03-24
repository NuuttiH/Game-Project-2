using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour{
    public static GameMaster Instance;
    public Vector3 startLocation; // = (0, 0, 0); expect player to spawn here
    
    public float objectActivationDistance = 3.8f;

    public bool puzzleOpen;

    [Header("Inventory")]
    public bool inventoryOpen;
    public InventoryManager inventoryManager;
    public int inventoryOffset;
    public Item emptyItem;

    [Header("Items")]
    public List<Item> items = new List<Item>();
    
    
    void Awake(){
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start(){
        inventoryOpen = false;
        inventoryOffset = 0;

        PuzzleController.Instance.ClosePuzzle();
    }

    public void PickupItem(Item item){
        // Assume inventory is large enough to hold all items
        Debug.Log("Picked up " + item.name);
        items.Add(item);
        inventoryManager.DrawInventory();
    }

    public void RemoveItem(Item item){
        // Assume inventory is large enough to hold all items
        Debug.Log("Removing " + item.name);
        items.Remove(item);
        inventoryManager.DrawInventory();
    }
}
