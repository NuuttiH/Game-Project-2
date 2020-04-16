using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldHandler : MonoBehaviour{
    public CombinationLockController combinationLockController;
    public Text text;
    public int id;

    public void TextChanged(string newText){
        if(newText!=""){
            char newChar = newText[newText.Length-1];
            combinationLockController.ChangeSolution(newChar, id);
        }
    }
}
