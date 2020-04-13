using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue{
    public int uniqueID;
    public DialogueLine[] lines;

    public bool Trigger(){
        if(uniqueID==0 || !GameMaster.Instance.dialogueMemory[uniqueID]){
            if(lines.Length>0){
                DialogueManager.Instance.StartDialogue(this);
            }
            GameMaster.Instance.dialogueMemory[uniqueID] = true;
            return true;
        }
        else return false;
    }      
}
