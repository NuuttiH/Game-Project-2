using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Puzzle (Combine1)", menuName = "Puzzle (Combine1)")]
public class PuzzleCombine1 : ScriptableObject{
    public string title;
    [TextArea(10,15)]
    public string puzzleText;
    [Header("Icon that items are dragged to")]
    public Sprite combineIcon;

    [Header("Items made available by the puzzle")]
    public int puzzleItemCount;
    public Item item1;
    public Item item2;
    public Item item3;
    public Item item4;
    public Item item5;
    public Item item6;

    [Header("Correct combination")]
    public PuzzleCombine1Solution correctSolution;

    [Header("Combinations with unique results")]
    public PuzzleCombine1Solution[] solutions;

    [Header("Default combination result")]
    public PuzzleCombine1Solution defaultFailingSolution;
}
