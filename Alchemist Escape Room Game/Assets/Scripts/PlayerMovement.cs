using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{
    public Vector3 targetPosition = GameMaster.Instance.startLocation;
    private float camHeight = GameMaster.Instance.startLocation.y; // 0

    // Locations for the rooms left and rightmost positions on X
    public float leftWallX;
    public float rightWallX;

    void Update(){
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.y = camHeight;
            if(targetPosition.x < leftWallX) targetPosition.x = leftWallX;
            if(targetPosition.x > rightWallX) targetPosition.x = rightWallX;
        }
 
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 5);
    }
}
