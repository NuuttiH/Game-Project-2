using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour{
    public static GameMaster Instance;
    public Vector3 startLocation; // = (0, 0, 0); expect player to spawn here

    void Awake(){
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
