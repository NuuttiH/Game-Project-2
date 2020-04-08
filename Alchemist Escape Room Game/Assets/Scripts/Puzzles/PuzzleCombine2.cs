using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Puzzle (Combine2)", menuName = "Puzzle (Combine2)")]
public class PuzzleCombine2 : ScriptableObject{
    public string title;

    [Header("Item combinations with unique results")]
    public PuzzleCombine2Solution[] solutions;

    [Header("Default item combination result")]
    public PuzzleCombine2Solution defaultFailingSolution;
}
