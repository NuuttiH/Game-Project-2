using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GameEventHandler : MonoBehaviour{
    public static GameEventHandler Instance;
    [HideInInspector]
    public int eventCount = 2;

    void Awake(){
        Instance = this;
    }

    public void DoEvent(int customEventId){
        if(customEventId==0 || (eventCount>customEventId 
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
        GameObject.Find("GlobalLight").GetComponent<Light2D>().intensity = 0.4f;
        GameObject.Find("ChandelierLight").GetComponent<Light2D>().intensity = 0.6f;
    }
}
