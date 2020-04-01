using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler{
    [Header("GUI Reference")]
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
                string resultName = results[0].gameObject.name;
                switch(resultName){
                    case "CombineIcon":
                        results[0].gameObject.transform.root.GetComponent<Canvas>()
                        .GetComponent<PuzzleCombine1Controller>()
                        .Combine(itemDisplay.item);
                        break;
                    case "ItemInput1Image":
                        results[0].gameObject.transform.root.GetComponent<Canvas>()
                        .GetComponent<PuzzleCombine2Controller>()
                        .Combine(itemDisplay.item, 1);
                        break;
                    case "ItemInput2Image":
                        results[0].gameObject.transform.root.GetComponent<Canvas>()
                        .GetComponent<PuzzleCombine2Controller>()
                        .Combine(itemDisplay.item, 2);
                        break;
                    default:
                        break;
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
