using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue{
    public DialogueLine[] lines;

    public void Trigger(){
        if(lines.Length>0) DialogueManager.Instance.StartDialogue(this);
    }
}
