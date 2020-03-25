using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractiveObject : MonoBehaviour{
    public Item item;
    public SpriteRenderer spriteRenderer;
    public bool hasPickup;
    public bool disappearOnAction;
    public Dialogue actionDialogue;
    public PuzzleCombine puzzleCombine;


    void Start(){
        spriteRenderer.sprite = item.artwork;
    }

    void OnMouseOver(){
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            Debug.Log("Distance between player and object: " + Vector3.Distance(
            GameObject.FindWithTag("Player").transform.position,this.transform.position));
            
            if(Vector3.Distance(GameObject.FindWithTag("Player").transform.position,
            this.transform.position) < GameMaster.Instance.objectActivationDistance){
  
                actionDialogue.Trigger();
                if(hasPickup) GameMaster.Instance.PickupItem(item);
                if(disappearOnAction) Destroy(gameObject);
                if(puzzleCombine!=null){
                    PuzzleCombineController.Instance.OpenPuzzle(puzzleCombine);
                }
            }
        }
    }
}
