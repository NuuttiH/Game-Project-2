using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour{
    [Header("GUI Reference")]
    public Item item;
    public Image artwork;
    public Text itemName;
    public Text itemDescription;

    void Start(){
        if(item == null){
            artwork.enabled = false;
            return;
        }
        artwork.sprite = item.artwork;
        itemName.text = item.name;
        itemDescription.text = item.description;
    }

    public void NewDisplay(Item newItem){
        if(newItem == null){
            item = null;
            artwork.enabled = false;
        }
        else if(newItem == GameMaster.Instance.emptyItem) EmptyDisplay();
        else if(newItem == GameMaster.Instance.emptyUIItem) EmptyUIDisplay();
        else{
            item = newItem;
            artwork.sprite = item.artwork;
            artwork.enabled = true;
            artwork.GetComponent<ItemTooltip>().enabled = true;
            artwork.GetComponent<ItemDragHandler>().enabled = true;
            itemName.text = item.name;
            itemDescription.text = item.description;
        }
    }

    public void EmptyDisplay(){
        item = GameMaster.Instance.emptyItem;
        artwork.GetComponent<ItemTooltip>().enabled = false;
        artwork.GetComponent<ItemDragHandler>().enabled = false;
    }

    public void EmptyUIDisplay(){
        item = GameMaster.Instance.emptyUIItem;
        artwork.sprite = item.artwork;
        //artwork.GetComponent<SpriteRenderer>().color = Color.blue;
        artwork.GetComponent<ItemTooltip>().enabled = false;
        artwork.GetComponent<ItemDragHandler>().enabled = false;
    }
}
