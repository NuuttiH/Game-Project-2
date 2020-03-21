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
    public int inventorySize = 8;

    [Header("Items")]
    public Item[] items;
    
    
    void Awake(){
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start(){
        inventoryOpen = false;
        inventoryOffset = 0;
        items = new Item[inventorySize];
        for(int i=0; i<inventorySize; i++){
            items[i] = emptyItem;
        }
    }

    public void ItemPickup(Item item){
        // Assume inventory is large enough to hold all items
        Debug.Log("Picked up " + item.name);
        int i = 0;
        for(; i<inventorySize; i++){
            if(items[0] == emptyItem) break;
        }
        items[i] = item;
        inventoryManager.DrawInventory();
    }
}
