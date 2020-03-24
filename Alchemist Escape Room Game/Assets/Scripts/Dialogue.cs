using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour{
    public DialogueLine[] lines;

    public void Trigger(){
        DialogueManager.Instance.StartDialogue(this);
    }
}
