using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLineIntro{
    [TextArea(3, 10)]
    public string sentence;

    public AudioClip audioClip;
}
