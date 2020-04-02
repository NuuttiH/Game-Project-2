using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Puzzle (Combine2)", menuName = "Puzzle (Combine2)")]
public class PuzzleCombine2 : ScriptableObject{
    public string title;
    [TextArea(10,15)]
    public string puzzleText;

    public PuzzleCombine2Solution correctSolution;
    public PuzzleCombine2Solution[] solutions;
    public PuzzleCombine2Solution defaultFailingSolution;
}
