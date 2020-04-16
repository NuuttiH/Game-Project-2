using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour{
    public static PlayerController Instance;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public Vector3 targetPosition;

    public float objectActivationDistance = 3.8f;
    
    private Camera cameraMain;
    [HideInInspector]
    public InteractiveObject mouseOverInteractiveObject;

    [Header("Movement limitations on X axis")]
    public float leftWall;
    public float rightWall;

    private InteractiveObject queuedAction;

    void Awake(){
        Instance = this;
    }

    void Start(){
        transform.position = GameMaster.Instance.startLocation;
        targetPosition = GameMaster.Instance.startLocation;
        cameraMain = Camera.main;
        queuedAction = null;
    }

    void Update(){
        if(!EventSystem.current.IsPointerOverGameObject() && !GameMaster.Instance.menuOpen
        && GameMaster.Instance.puzzleOpen==0 && Input.GetKeyDown(KeyCode.Mouse0)){
            queuedAction = null;

            if(cameraMain.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x) spriteRenderer.flipX = false;
            else spriteRenderer.flipX = true;

            // Interactive action
            bool didAction = false;
            if(mouseOverInteractiveObject != null){
                Debug.Log("Distance between player and object: " + Vector3.Distance(
                transform.position, mouseOverInteractiveObject.t.position));
                
                if(Vector3.Distance(transform.position, mouseOverInteractiveObject.t.position)
                < objectActivationDistance){
                    didAction = true;
                    mouseOverInteractiveObject.DoDialogue();

                    if(mouseOverInteractiveObject.zoomOnAction){
                        ImageZoom.Instance.ZoomImage(mouseOverInteractiveObject.gameObject);
                    }

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
                else{
                    // Queue action if click out of range 
                    queuedAction = mouseOverInteractiveObject;
                }
            }
            // Move if no interactive action
            if(!didAction){
                animator.SetBool("IsWalking", true);
                targetPosition = cameraMain.ScreenToWorldPoint(Input.mousePosition);
                targetPosition.y = GameMaster.Instance.camHeight;
                if(targetPosition.x < leftWall) targetPosition.x = leftWall;
                else if(targetPosition.x > rightWall) targetPosition.x = rightWall;
                targetPosition.z = 0;
            }
            else{
                targetPosition = transform.position;
            }
        }
        else if(transform.position==targetPosition){
            animator.SetBool("IsWalking", false);

            if(queuedAction!=null){
                // Do queued action
                queuedAction.DoDialogue();
                if(queuedAction.pickupOnAction){ 
                    GameMaster.Instance.PickupItem(queuedAction.item);
                    Destroy(queuedAction.gameObject);
                }
                else if(queuedAction.puzzleCombine1!=null){
                    PuzzleCombine1Controller.Instance.OpenPuzzle(
                    queuedAction.puzzleCombine1);
                }
                else if(queuedAction.puzzleCombine2!=null){
                    PuzzleCombine2Controller.Instance.OpenPuzzle(
                    queuedAction.puzzleCombine2);
                }
                else if(queuedAction.combinationLock!=null){
                    CombinationLockController.Instance.OpenPuzzle(
                    queuedAction.combinationLock);
                }
                else if(queuedAction.puzzleSpellbook!=null){
                    SpellbookController.Instance.OpenPuzzle(
                    queuedAction.puzzleSpellbook);
                }
                queuedAction = null;
            } 
        }
            
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 5);
    }
}
