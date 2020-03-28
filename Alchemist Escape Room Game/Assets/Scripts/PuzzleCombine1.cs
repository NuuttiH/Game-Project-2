using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Puzzle (Combine1)", menuName = "Puzzle (Combine1)")]
public class PuzzleCombine1 : ScriptableObject{
    public string title;
    [TextArea(10,15)]
    public string puzzleText;
    public Sprite combineIcon;

    public int puzzleItemCount;
    public Item item1;
    public Item item2;
    public Item item3;
    public Item item4;
    public Item item5;
    public Item item6;

    public PuzzleCombine1Solution correctSolution;
    public PuzzleCombine1Solution[] solutions;
    public PuzzleCombine1Solution defaultFailingSolution;
}
