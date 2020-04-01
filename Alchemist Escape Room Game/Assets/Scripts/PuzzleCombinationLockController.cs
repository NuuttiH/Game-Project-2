using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PuzzleCombinationLockController : MonoBehaviour{
    public static PuzzleCombinationLockController Instance;
    [Header("GUI Reference")]
    public Canvas canvas;
    public CanvasGroup canvasGroup;
    public Image background;
    public Text puzzleText;
    

    public InputField inputField1;
    public InputField inputField2;
    public InputField inputField3;
    
    private char[] currentSolution;
    private PuzzleCombinationLock currentPuzzle;

    void Awake(){
        Instance = this;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Mouse1)) ClosePuzzle();
    }


    public void OpenPuzzle(PuzzleCombinationLock puzzle){
        puzzleText.text = puzzle.puzzleText;
        
        currentPuzzle = puzzle;

        currentSolution = new char[3];
        currentSolution[0] = ' ';
        currentSolution[1] = ' ';
        currentSolution[2] = ' ';
        GameMaster.Instance.puzzleOpen = 3;
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        inputField1.ActivateInputField();
    }
    public void ClosePuzzle(){
        GameMaster.Instance.puzzleOpen = 0;
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
    public void ResetPuzzle(){ OpenPuzzle(currentPuzzle); }

    public void ChangeSolution(char newChar, int inputId){
        currentSolution[inputId] = newChar;
        switch(inputId){
            case 2:
                inputField3.text = newChar.ToString();
                break;
            case 1:
                inputField2.text = newChar.ToString();
                break;
            default:
                inputField1.text = newChar.ToString();
                break;
        }
        CheckOneSolution(inputId);
    }
    public void CheckOneSolution(int inputId){
        if(currentSolution[inputId] == currentPuzzle.correctSolution[inputId]){
            currentPuzzle.firstSuccessDialogue.Trigger();
            CheckSolution();
        }
        else currentPuzzle.firstFailDialogue.Trigger();
    }
    public void CheckSolution(){
        //yield return new WaitForSeconds(1);
        if(currentSolution[0] == currentPuzzle.correctSolution[0]
        && currentSolution[1] == currentPuzzle.correctSolution[1]
        && currentSolution[2] == currentPuzzle.correctSolution[2]){
            Debug.Log("Correct solution inputted");
            currentPuzzle.correctSolutionDialogue.Trigger();
            GameEventHandler.Instance.DoEvent(currentPuzzle.customEventId);
            if(currentPuzzle.rewardItem != null)
                GameMaster.Instance.PickupItem(currentPuzzle.rewardItem);
            ClosePuzzle();
        }
    }
}
