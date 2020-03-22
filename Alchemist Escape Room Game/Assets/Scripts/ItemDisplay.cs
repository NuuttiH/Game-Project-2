using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour{
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
            return;
        }
        item = newItem;
        artwork.sprite = item.artwork;
        artwork.enabled = true;
        itemName.text = item.name;
        itemDescription.text = item.description;
    }
}
