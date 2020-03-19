using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour{
    public Vector3 targetPosition;
    private float camHeight; // 0

    [Header("Movement limitations on X axis")]
    public float leftWall;
    public float rightWall;

    void Start(){
        targetPosition = GameMaster.Instance.startLocation;
        camHeight = GameMaster.Instance.startLocation.y; // 0
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Mouse0) &&
        !EventSystem.current.IsPointerOverGameObject()){
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.y = camHeight;
            if(targetPosition.x < leftWall) targetPosition.x = leftWall;
            else if(targetPosition.x > rightWall) targetPosition.x = rightWall;
            targetPosition.z = 0;
        }
            
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 5);
    }
}
