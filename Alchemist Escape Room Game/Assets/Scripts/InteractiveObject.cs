using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractiveObject : MonoBehaviour{
    [Header("Optional Sprite Renderer reference")]
    public SpriteRenderer spriteRenderer;

    [Header("Item Behaviour")]
    public Item item;
    public bool pickupOnAction;
    public bool zoomOnAction;
    public List<Dialogue> actionDialogue = new List<Dialogue>();

    [Header("Attached puzzle")]
    public PuzzleCombine1 puzzleCombine1;
    public PuzzleCombine2 puzzleCombine2;
    public CombinationLock combinationLock;
    public Spellbook puzzleSpellbook;

    [HideInInspector]
    public Transform t;


    void Start(){
        // Destroy on startup if already on inventory because of saved data
        if(pickupOnAction && item!=null 
        && GameMaster.Instance.items.Find(x => x.name==item.name)){
            Destroy(this.gameObject);
        }
        else{
            if(spriteRenderer!=null) spriteRenderer.sprite = item.artwork;
            t = transform;
        }
    }


    public bool DoDialogue(){
        bool success = false;
        
        foreach(Dialogue dialogue in actionDialogue){
            success = dialogue.Trigger();
            if(success) break;
        }

        return success;
    }

    void OnMouseOver(){
        PlayerController.Instance.mouseOverInteractiveObject = this;
    }

    void OnMouseExit(){
        PlayerController.Instance.mouseOverInteractiveObject = null;
    }
}
