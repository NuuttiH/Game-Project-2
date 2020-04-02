using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Puzzle (Spellbook)", menuName = "Puzzle (Spellbook)")]
public class Spellbook : ScriptableObject{
    public string title;

    public Item item1;
    public Item item2;
    public Item item3;

    public Dialogue firstFailDialogue;
    public Dialogue correctSolutionDialogue;
    public int customEventId;
}
