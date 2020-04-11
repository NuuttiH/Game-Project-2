using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour{
    public static PlayerController Instance;
    public Vector3 targetPosition;

    public float objectActivationDistance = 3.8f;
    
    [HideInInspector]
    public float camHeight; // 0
    private Camera cameraMain;
    [HideInInspector]
    public InteractiveObject mouseOverInteractiveObject;

    [Header("Movement limitations on X axis")]
    public float leftWall;
    public float rightWall;

    void Awake(){
        Instance = this;
    }

    void Start(){
        this.transform.position = GameMaster.Instance.startLocation;
        targetPosition = GameMaster.Instance.startLocation;
        camHeight = GameMaster.Instance.startLocation.y; // 0
        cameraMain = Camera.main;
    }

    void Update(){
        if(!EventSystem.current.IsPointerOverGameObject() &&
        GameMaster.Instance.puzzleOpen==0 && Input.GetKeyDown(KeyCode.Mouse0)){
            // Interactive action
            bool didAction = false;
            if(mouseOverInteractiveObject != null){
                Debug.Log("Distance between player and object: " + Vector3.Distance(
                this.transform.position, mouseOverInteractiveObject.t.position));
                
                if(Vector3.Distance(this.transform.position, mouseOverInteractiveObject.t.position)
                < objectActivationDistance){
                    didAction = true;
                    mouseOverInteractiveObject.actionDialogue.Trigger();
                    if(mouseOverInteractiveObject.pickupOnAction){ 
                        GameMaster.Instance.PickupItem(mouseOverInteractiveObject.item);
                        Destroy(mouseOverInteractiveObject.gameObject);
                    }
                    else if(mouseOverInteractiveObject.puzzleCombine1!=null){
                        PuzzleCombine1Controller.Instance.OpenPuzzle(
                        mouseOverInteractiveObject.puzzleCombine1);
                    }
                    else if(mouseOverInteractiveObject.puzzleCombine2!=null){
                        PuzzleCombine2Controller.Instance.OpenPuzzle(
                        mouseOverInteractiveObject.puzzleCombine2);
                    }
                    else if(mouseOverInteractiveObject.combinationLock!=null){
                        CombinationLockController.Instance.OpenPuzzle(
                        mouseOverInteractiveObject.combinationLock);
                    }
                    else if(mouseOverInteractiveObject.puzzleSpellbook!=null){
                        SpellbookController.Instance.OpenPuzzle(
                        mouseOverInteractiveObject.puzzleSpellbook);
                    }
                }
            }
            // Move if no interactive action
            if(!didAction){
                targetPosition = cameraMain.ScreenToWorldPoint(Input.mousePosition);
                targetPosition.y = camHeight;
                if(targetPosition.x < leftWall) targetPosition.x = leftWall;
                else if(targetPosition.x > rightWall) targetPosition.x = rightWall;
                targetPosition.z = 0;
            }
            else{
                targetPosition = transform.position;
            }
        }
            
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 5);
    }
}
