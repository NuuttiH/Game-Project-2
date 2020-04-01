using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldHandler : MonoBehaviour{
    public PuzzleCombinationLockController puzzleCombinationLockController;
    public Text text;
    public int id;

    public void TextChanged(string newText){
        char newChar = newText[newText.Length-1];
        puzzleCombinationLockController.ChangeSolution(newChar, id);
    }
}
