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
        artwork.sprite = item.artwork;
        itemName.text = item.name;
        itemDescription.text = item.description;
    }

    public void NewDisplay(Item newItem){
        item = newItem;
        artwork.sprite = item.artwork;
        itemName.text = item.name;
        itemDescription.text = item.description;
    }
}
