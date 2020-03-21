using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractiveObject : MonoBehaviour{
    public Item item;
    public SpriteRenderer spriteRenderer;
    public bool hasPickup;
    public bool disappearOnAction;
    public Puzzle puzzle;


    void Start(){
        spriteRenderer.sprite = item.artwork;
    }

    void OnMouseOver(){
        if(Input.GetMouseButtonDown(1)){
            Debug.Log("Distance between player and object: " + Vector3.Distance(
            GameObject.FindWithTag("Player").transform.position,this.transform.position));
            
            if(Vector3.Distance(GameObject.FindWithTag("Player").transform.position,
            this.transform.position) < GameMaster.Instance.objectActivationDistance){
  
                if(hasPickup) GameMaster.Instance.ItemPickup(item);
                if(disappearOnAction) Destroy(gameObject);
                if(puzzle!=null){
                    puzzle.OpenPuzzle();
                }
            }
        }
    }
}
