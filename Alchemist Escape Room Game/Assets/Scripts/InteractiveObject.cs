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

    [HideInInspector]
    public Transform t;


    void Start(){
        if(spriteRenderer!=null) spriteRenderer.sprite = item.artwork;
        t = transform;
    }

    void OnMouseOver(){
        PlayerController.Instance.mouseOverInteractiveObject = this;
    }

    void OnMouseExit(){
        PlayerController.Instance.mouseOverInteractiveObject = null;
    }
}
