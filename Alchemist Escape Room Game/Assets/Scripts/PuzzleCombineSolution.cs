using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PuzzleCombineSolution{
    public Dialogue resultDialogue;
    public List<Item> solution = new List<Item>();
    public int customEventId;
}
