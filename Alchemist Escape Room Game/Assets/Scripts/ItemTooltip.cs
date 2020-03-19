using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    public CanvasGroup tooltipCanvas;

    void Start(){
        tooltipCanvas.alpha = 0;
    }

    public void OnPointerEnter(PointerEventData eventData){
        if(GameMaster.Instance.inventoryOpen) tooltipCanvas.alpha = 1;
    }

    public void OnPointerExit(PointerEventData eventData){
        tooltipCanvas.alpha = 0;
    }
}
