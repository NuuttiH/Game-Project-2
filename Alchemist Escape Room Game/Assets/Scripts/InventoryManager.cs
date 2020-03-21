using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour{
    public static InventoryManager Instance;
    public Animator animator;

    public Button inventoryButton;

    [Header("GUI Item Reference")]
    public GameObject item1;
    public GameObject item2;
    public GameObject item3;
    public GameObject item4;
    public GameObject item5;
    public GameObject item6;

    void Awaken(){
        Instance = this;
    }

    void Start(){
        inventoryButton.onClick.AddListener(TaskOnClick);
    }

    public void TaskOnClick(){
        if(GameMaster.Instance.inventoryOpen) CloseInventory();
        else OpenInventory();
    }

    
    public void CloseInventory(){
        animator.SetBool("IsOpen", false);
        GameMaster.Instance.inventoryOpen = false;
    }
    public void OpenInventory(){
        animator.SetBool("IsOpen", true);
        GameMaster.Instance.inventoryOpen = true;
    }

    public void DrawInventory(){
        int offset = GameMaster.Instance.inventoryOffset;
        item1.GetComponent<ItemDisplay>().NewDisplay(GameMaster.Instance.items[0+offset]);
        /*item2.GetComponent<ItemDisplay>().NewDisplay(GameMaster.Instance.items[1+offset]);
        item3.GetComponent<ItemDisplay>().NewDisplay(GameMaster.Instance.items[2+offset]);
        item4.GetComponent<ItemDisplay>().NewDisplay(GameMaster.Instance.items[3+offset]);
        item5.GetComponent<ItemDisplay>().NewDisplay(GameMaster.Instance.items[4+offset]);
        item6.GetComponent<ItemDisplay>().NewDisplay(GameMaster.Instance.items[5+offset]);
    */}
}
