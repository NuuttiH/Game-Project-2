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
            switch(customEventId){
                case 1:
                    Event1();
                    break;
                default:
                    Debug.Log("Custom Event Handler - No Event");
                    break;
            }
            GameMaster.Instance.eventMemory[customEventId] = true;
        }
    }

    void Event1(){
        Debug.Log("Custom Event Handler - Event 1");
        GameObject.Find("GlobalLight").GetComponent<Light2D>().intensity = 0.4f;
        GameObject.Find("ChandelierLight").GetComponent<Light2D>().intensity = 0.6f;
    }
}
