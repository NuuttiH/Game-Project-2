using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GameEventHandler : MonoBehaviour{
    public static GameEventHandler Instance;

    void Awake(){
        Instance = this;
    }

    public void DoEvent(int customEventId){
        switch(customEventId){
            case 1:
                Event1();
                break;
            case 0:
                Debug.Log("Custom Event Handler - No Event");
                break;
        }
    }

    void Event1(){
        GameObject.Find("GlobalLight").GetComponent<Light2D>().intensity = 0.4f;
        GameObject.Find("ChandelierLight").GetComponent<Light2D>().intensity = 0.6f;
    }
}
