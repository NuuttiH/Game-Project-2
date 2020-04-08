using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Puzzle (Spellbook)", menuName = "Puzzle (Spellbook)")]
public class Spellbook : ScriptableObject{
    public string title;

    [Header("Items required for event to trigger")]
    public Item item1;
    public Item item2;
    public Item item3;

    [Header("Special dialogue")]
    public Dialogue firstFailDialogue;
    public Dialogue correctSolutionDialogue;

    [Header("Event(ID) triggered by success")]
    public int customEventId;
}
