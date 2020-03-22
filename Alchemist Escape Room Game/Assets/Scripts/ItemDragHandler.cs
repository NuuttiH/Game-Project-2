using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IDropHandler{
    public ItemDisplay itemDisplay;

    public void OnDrag(PointerEventData eventData){
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData){
        transform.localPosition = Vector3.zero;
    }

    public void OnDrop(PointerEventData eventData){
        PuzzleController.Instance.Combine(itemDisplay.item);
    }
}
