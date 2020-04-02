using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue{
    public int uniqueID;
    public DialogueLine[] lines;

    public void Trigger(){
        if(lines.Length>0 && (uniqueID==0 
        || !GameMaster.Instance.dialogueMemory[uniqueID])){
            DialogueManager.Instance.StartDialogue(this);
            GameMaster.Instance.dialogueMemory[uniqueID] = true;
        }
    }      
}
