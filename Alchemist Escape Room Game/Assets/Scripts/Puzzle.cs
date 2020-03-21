using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle : MonoBehaviour{
    public Canvas canvas;
    public Animator animator;
    public Image background;

    public int puzzleItemCount;
    public Item item1;
    public Item item2;
    public Item item3;
    public Item item4;
    public Item item5;
    public Item item6;
    

    void Update(){
        if(Input.GetKeyUp(KeyCode.Escape)) canvas.enabled = false;
    }

    public void OpenPuzzle(){
        animator.SetBool("IsOpen", true);
        GameMaster.Instance.puzzleOpen = true;
    }
    public void ClosePuzzle(){
        animator.SetBool("IsOpen", false);
        GameMaster.Instance.puzzleOpen = false;
    }
}
