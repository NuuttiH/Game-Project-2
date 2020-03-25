using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PuzzleCombineController : MonoBehaviour{
    public static PuzzleCombineController Instance;
    public Canvas canvas;
    public Image background;
    public Image combineIcon;
    public Text combineText;
    public Text title;
    public Text puzzleText;

    [Header("GUI Item Reference")]
    public GameObject item1;
    public GameObject item2;
    public GameObject item3;
    public GameObject item4;
    public GameObject item5;
    public GameObject item6;

    public List<Item> realSolution = new List<Item>();
    
    public List<Item> currentSolution = new List<Item>();
    private PuzzleCombine currentPuzzle;

    void Awake(){
        Instance = this;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Mouse1)) ClosePuzzle();
    }


    public void OpenPuzzle(PuzzleCombine puzzle){
        title.text = puzzle.title;
        puzzleText.text = puzzle.puzzleText;
        combineIcon.sprite = puzzle.combineIcon;

        combineText.text = "0/" + puzzle.correctSolution.solution.Count;
        item1.GetComponent<ItemDisplay>().NewDisplay(puzzle.item1);
        item2.GetComponent<ItemDisplay>().NewDisplay(puzzle.item2);
        item3.GetComponent<ItemDisplay>().NewDisplay(puzzle.item3);
        item4.GetComponent<ItemDisplay>().NewDisplay(puzzle.item4);
        item5.GetComponent<ItemDisplay>().NewDisplay(puzzle.item5);
        item6.GetComponent<ItemDisplay>().NewDisplay(puzzle.item6);
        
        realSolution = puzzle.correctSolution.solution;
        currentPuzzle = puzzle;

        currentSolution = new List<Item>();
        GameMaster.Instance.puzzleOpen = 1;
        canvas.enabled = true;
    }
    public void ClosePuzzle(){
        GameMaster.Instance.puzzleOpen = 0;
        canvas.enabled = false;
    }
    public void ResetPuzzle(){ OpenPuzzle(currentPuzzle); }

    public void Combine(Item item){
        currentSolution.Add(item);
        if(currentSolution.Count == realSolution.Count){
            bool match = false;

            foreach(Item realSolutionItem in realSolution){
                match = false;
                foreach(Item currentSolutionItem in currentSolution){
                    if(realSolutionItem.name == currentSolutionItem.name){
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
                ClosePuzzle();
            }
            else{
                Debug.Log("Puzzle failed");

                foreach(PuzzleCombineSolution s in currentPuzzle.solutions){
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
            combineText.text = currentSolution.Count + "/" + realSolution.Count;
        }
    }
    
}
