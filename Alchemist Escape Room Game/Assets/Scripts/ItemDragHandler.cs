using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler{
    public ItemDisplay itemDisplay;
    public Image image;

    public void OnDrag(PointerEventData eventData){
        image.raycastTarget = false;
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData){
        transform.localPosition = Vector3.zero;

        int puzzleId = GameMaster.Instance.puzzleOpen;
        if(puzzleId!=0){
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            
            if(results.Count>0){
                Debug.Log("Item dropped on: " + results[0]);
                if(results[0].gameObject.name == "CombineIcon"){
                    if(puzzleId==1){
                        results[0].gameObject.transform.root.GetComponent<Canvas>()
                        .GetComponent<PuzzleCombineController>()
                        .Combine(itemDisplay.item);
                    }
                }
            }
            else{ Debug.Log("Item dropped over nothing "); }
        }
        

        StartCoroutine(TurnRaycasterBackOn());
    }
    IEnumerator TurnRaycasterBackOn(){
        yield return new WaitForSeconds(0.5f);
        image.raycastTarget = true;
    }
}
