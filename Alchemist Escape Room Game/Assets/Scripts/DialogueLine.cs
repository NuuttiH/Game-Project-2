using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLine : MonoBehaviour{
    public Sprite sprite;
    new public string name;

    [TextArea(3, 10)]
    public string sentence;
}
