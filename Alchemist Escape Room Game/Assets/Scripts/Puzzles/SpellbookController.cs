using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpellbookController : MonoBehaviour{
    public static SpellbookController Instance;
    [Header("GUI Reference")]
    public Canvas canvas;
    public Image background;
    public Text title;

    public GameObject item1;
    public GameObject item2;
    public GameObject item3;
    
    private Item[] currentSolution;
    private Spellbook currentPuzzle;


    void Awake(){
        Instance = this;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Mouse1)) ClosePuzzle();
    }

    public void OpenPuzzle(Spellbook puzzle){
        title.text = puzzle.title;
        
        item1.GetComponent<ItemDisplay>().EmptyDisplay();
        item2.GetComponent<ItemDisplay>().EmptyDisplay();
        item3.GetComponent<ItemDisplay>().EmptyDisplay();
        
        currentPuzzle = puzzle;

        currentSolution = new Item[3];
        GameMaster.Instance.puzzleOpen = 4;
        canvas.enabled = true;
    }
    public void ClosePuzzle(){
        GameMaster.Instance.puzzleOpen = 0;
        canvas.enabled = false;
    }
    public void ResetPuzzle(){ OpenPuzzle(currentPuzzle); }

    public void InsertItem(Item item, int slot){
        switch(slot){
            case 3:
                item3.GetComponent<ItemDisplay>().NewDisplay(item);
                currentSolution[2] = item;
                break;
            case 2:
                item2.GetComponent<ItemDisplay>().NewDisplay(item);
                currentSolution[1] = item;
                break;
            case 1:
                item1.GetComponent<ItemDisplay>().NewDisplay(item);
                currentSolution[0] = item;
                break;
        }

        if(currentSolution[0]!=null && currentSolution[1]!=null && currentSolution[2]!=null){
            if(currentSolution[0].name == currentPuzzle.item1.name 
            && currentSolution[1].name == currentPuzzle.item2.name 
            && currentSolution[2].name == currentPuzzle.item3.name ){
                Debug.Log("Puzzle compleated");
                currentPuzzle.correctSolutionDialogue.Trigger();
                GameEventHandler.Instance
                .DoEvent(currentPuzzle.customEventId);
                ClosePuzzle(); 
            }
            else{
                Debug.Log("Puzzle failed");
                currentPuzzle.firstFailDialogue.Trigger();
            }
        }
        else{
            Debug.Log("Testing");
        }
    }
}
