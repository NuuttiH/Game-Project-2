using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Puzzle (Spellbook)", menuName = "Puzzle (Spellbook)")]
public class Spellbook : ScriptableObject{
    public string title;
    [TextArea(10,15)]
    public string puzzleText;

    [Header("Items required for event to trigger")]
    public Item item1;
    public Item item2;
    public Item item3;

    [Header("Special dialogue")]
    public Dialogue firstFailDialogue;
    public Dialogue correctSolutionDialogue;

    [Header("Outcome")]
    public Item rewardItem;
    public int customEventId;
}
