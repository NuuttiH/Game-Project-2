using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PuzzleCombine2Controller : MonoBehaviour{
    public static PuzzleCombine2Controller Instance;
    [Header("GUI Reference")]
    public Canvas canvas;
    public Image background;
    public Text title;

    public GameObject item1;
    public GameObject item2;
    public GameObject item3;
    
    private Item[] currentSolution;
    private PuzzleCombine2 currentPuzzle;


    void Awake(){
        Instance = this;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Mouse1)) ClosePuzzle();
    }


    public void OpenPuzzle(PuzzleCombine2 puzzle){
        title.text = puzzle.title;
        
        item1.GetComponent<ItemDisplay>().EmptyDisplay();
        item2.GetComponent<ItemDisplay>().EmptyDisplay();
        item3.GetComponent<ItemDisplay>().EmptyDisplay();
        
        currentPuzzle = puzzle;

        currentSolution = new Item[2];
        GameMaster.Instance.puzzleOpen = 2;
        canvas.enabled = true;
    }
    public void ClosePuzzle(){
        GameMaster.Instance.puzzleOpen = 0;
        canvas.enabled = false;
    }
    public void ResetPuzzle(){ OpenPuzzle(currentPuzzle); }

    public void Combine(Item item, int slot){
        if(slot == 1){
            item1.GetComponent<ItemDisplay>().NewDisplay(item);
            currentSolution[0] = item;
        }
        else{
            item2.GetComponent<ItemDisplay>().NewDisplay(item);
            currentSolution[1] = item;
        }
        if(currentSolution[0]!=null && currentSolution[1]!=null){
            bool match = false;

            foreach(Item correctItem in currentPuzzle.correctSolution.solution){
                match = false;
                foreach(Item currentSolutionItem in currentSolution){
                    if(correctItem.name == currentSolutionItem.name){
                        match = true;
                        break;
                    }
                }
                if(!match) break;
            }

            if(match){
                Debug.Log("Puzzle compleated");
                currentPuzzle.correctSolution.resultDialogue.Trigger();
                GameEventHandler.Instance
                .DoEvent(currentPuzzle.correctSolution.customEventId);
                item3.GetComponent<ItemDisplay>()
                .NewDisplay(currentPuzzle.correctSolution.rewardItem);
                GameMaster.Instance
                .PickupItem(currentPuzzle.correctSolution.rewardItem);
                //ClosePuzzle(); do not close this puzzle on completion
            }
            else{
                Debug.Log("Puzzle failed");

                foreach(PuzzleCombine2Solution s in currentPuzzle.solutions){
                    foreach(Item solutionItem in s.solution){
                        match = false;
                        foreach(Item currentSolutionItem in currentSolution){
                            if(solutionItem.name == currentSolutionItem.name){
                                match = true;
                                break;
                            }
                        }
                        if(!match) break;
                    }
                    if(match){
                        Debug.Log("Failing solution found");
                        s.resultDialogue.Trigger();
                        GameEventHandler.Instance
                        .DoEvent(s.customEventId);
                        ClosePuzzle();
                    }
                    else{
                        Debug.Log("No specific failing solution found");
                        currentPuzzle.defaultFailingSolution.resultDialogue.Trigger();
                        GameEventHandler.Instance
                        .DoEvent(currentPuzzle.defaultFailingSolution.customEventId);
                        ClosePuzzle();
                    }
                }

                ResetPuzzle();
            }
        }
        else{
            Debug.Log("Testing");
        }
    }
    
}
