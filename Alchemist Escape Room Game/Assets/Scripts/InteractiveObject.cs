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
    public bool disappearOnAction;
    public Dialogue actionDialogue;

    [Header("Attached puzzle")]
    public PuzzleCombine1 puzzleCombine1;
    public PuzzleCombine2 puzzleCombine2;
    public CombinationLock combinationLock;
    public Spellbook puzzleSpellbook;


    void Start(){
        if(spriteRenderer!=null) spriteRenderer.sprite = item.artwork;
    }

    void OnMouseOver(){
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            Debug.Log("Distance between player and object: " + Vector3.Distance(
            GameObject.FindWithTag("Player").transform.position,this.transform.position));
            
            if(Vector3.Distance(GameObject.FindWithTag("Player").transform.position,
            this.transform.position) < GameMaster.Instance.objectActivationDistance){
  
                actionDialogue.Trigger();
                if(pickupOnAction){ 
                    GameMaster.Instance.PickupItem(item);
                    if(disappearOnAction) Destroy(gameObject);
                }
                else if(puzzleCombine1!=null){
                    PuzzleCombine1Controller.Instance.OpenPuzzle(puzzleCombine1);
                }
                else if(puzzleCombine2!=null){
                    PuzzleCombine2Controller.Instance.OpenPuzzle(puzzleCombine2);
                }
                else if(combinationLock!=null){
                    CombinationLockController.Instance.OpenPuzzle(combinationLock);
                }
                else if(puzzleSpellbook!=null){
                    SpellbookController.Instance.OpenPuzzle(puzzleSpellbook);
                }
                else if(disappearOnAction) Destroy(gameObject);
            }
        }
    }
}
