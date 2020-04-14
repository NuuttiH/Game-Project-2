using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour{
    public static Transition Instance;
    public Animator transition;


    void Awake(){
        Instance = this;
    }

    /*  Automatic
    void Start(){
        FadeIn();
    }


    public void FadeIn(){

    }*/

    public void FadeOut(){
        transition.SetTrigger("Flag");
    }
}
