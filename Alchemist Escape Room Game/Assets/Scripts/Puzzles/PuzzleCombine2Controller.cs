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
    
    private List<Item> currentSolution = new List<Item>();
    private PuzzleCombine2 currentPuzzle;


    void Awake(){
        Instance = this;
    }

    void Start(){
        ClosePuzzle();
    }


    public void OpenPuzzle(PuzzleCombine2 puzzle){
        title.text = puzzle.title;
        
        item1.GetComponent<ItemDisplay>().EmptyUIDisplay();
        item2.GetComponent<ItemDisplay>().EmptyUIDisplay();
        item3.GetComponent<ItemDisplay>().EmptyUIDisplay();
        
        currentPuzzle = puzzle;

        currentSolution = new List<Item>();
        GameMaster.Instance.puzzleOpen = 2;
        canvas.enabled = true;
    }
    public void ClosePuzzle(){
        GameMaster.Instance.puzzleOpen = 0;
        canvas.enabled = false;
    }
    public void ResetPuzzle(){ OpenPuzzle(currentPuzzle); }

    public void Combine(Item item, int slot){
        if(slot == 1)
            item1.GetComponent<ItemDisplay>().NewDisplay(item);
        else 
            item2.GetComponent<ItemDisplay>().NewDisplay(item);

        currentSolution.Add(item);

        if(currentSolution.Count==2){
            bool match = false;
            bool no_solution_match = true;

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
                // If untriggered match found, trigger it, otherwise continue checking
                if(match){
                    no_solution_match = false;
                    if(s.resultDialogue.Trigger()){
                        GameEventHandler.Instance.DoEvent(s.customEventId);
                        if(s.rewardItem != null){
                            Debug.Log("Some correct solution found");
                            item3.GetComponent<ItemDisplay>().NewDisplay(s.rewardItem);
                            GameMaster.Instance.RemoveItem(currentSolution[0]);
                            GameMaster.Instance.RemoveItem(currentSolution[1]);
                            GameMaster.Instance.PickupItem(s.rewardItem);
                        }
                        else{
                            Debug.Log("Some wrong solution found");
                            ResetPuzzle();
                        }
                        return;
                    }
                    else{
                        Debug.Log("Skip used dialogue");
                    }
                }
            }
            if(no_solution_match){
                Debug.Log("No specific failing solution found");
                currentPuzzle.defaultFailingSolution.resultDialogue.Trigger();
                GameEventHandler.Instance
                .DoEvent(currentPuzzle.defaultFailingSolution.customEventId);
                ResetPuzzle();
            }
            else{
                Debug.Log("Failing solution(s) found but already used");
                ResetPuzzle();
            }
        }
    }
}
