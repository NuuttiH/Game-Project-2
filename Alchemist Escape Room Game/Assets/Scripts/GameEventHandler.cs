using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GameEventHandler : MonoBehaviour{
    public static GameEventHandler Instance;


    void Awake(){
        Instance = this;
    }

    void Start(){
        for(int i=0; i<GameMaster.Instance.eventCount; i++){
            if(GameMaster.Instance.eventMemory[i]) DoEvent(i, true);
        }
    }


    public void DoEvent(int customEventId, bool force=false){
        if(customEventId==0 || force || (GameMaster.Instance.eventCount>customEventId 
        && !GameMaster.Instance.eventMemory[customEventId])){
            Debug.Log("Custom Event Handler - Event " + customEventId);
            switch(customEventId){
                case 1:
                    Event1();
                    break;
                case 2:
                    Event2();
                    break;
                case 3:
                    Event3();
                    break;
                case 4:
                    Event4();
                    break;
                case 5:
                    Event5();
                    break;
                default:
                    break;
            }
            if(customEventId!=0 && !force && customEventId!=5){
                GameMaster.Instance.eventMemory[customEventId] = true;
                GameMaster.Instance.Save(0); // Autosave
            }
        }
    }

    // Light puzzle
    void Event1(){
        GameObject.Find("GlobalLight").GetComponent<Light2D>()
        .intensity = 0.4f;
        GameObject.Find("ChandelierLight").GetComponent<Light2D>()
        .intensity = 0.6f;
        GameObject.Find("TableLampLight").GetComponent<Light2D>()
        .intensity = 0.7f;

        InteractiveObject lampInteractiveObject = GameObject.Find("PuzzleLamp")
        .GetComponent<InteractiveObject>();
        lampInteractiveObject.puzzleCombine1 = null;
        lampInteractiveObject.SetItem(Resources.Load<Item>("Items/Puzzle lamp ON"));
    }

    // First locked chest
    void Event2(){
        GameObject.Find("Chest1").GetComponent<InteractiveObject>()
        .combinationLock = null;
    }

    // Combining scrolls
    void Event3(){
        GameObject.Find("ScrollBook").GetComponent<InteractiveObject>()
        .puzzleSpellbook = null;
    }

    // Open door to allow game end
    void Event4(){
        InteractiveObject doorInteractiveObject = GameObject.Find("Door")
        .GetComponent<InteractiveObject>();
        doorInteractiveObject.eventOnAction = 5;
        doorInteractiveObject.actionDialogue = new List<Dialogue>();
    }

    // Game end
    void Event5(){
        GameMaster.Instance.StartScene(0);
    }
}
