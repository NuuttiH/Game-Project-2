using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageZoom : MonoBehaviour{
    public static ImageZoom Instance;
    public CanvasGroup canvasGroup;
    public GameObject panel;
    public Image imageObject;


    void Awake(){
        Instance = this;
    }

    void Start(){
        
    }


    public void ZoomImage(GameObject objectToZoom){
        //ImageZoom.Instance.ZoomImage(mouseOverInteractiveObject
        //.gameObject.GetComponent<SpriteRenderer>().sprite);

        // Assess the proper shape of the image by box colliders
        Vector2 imageSize = objectToZoom.GetComponent<BoxCollider2D>().size;
        float xToYRatio = imageSize.x / imageSize.y;
        Debug.Log(xToYRatio);

        if(xToYRatio>1.2f){         // Wide image
            panel.transform.localScale = new Vector3(1.4f, 1, 1);
        }
        else if(xToYRatio<0.8f){    // Tall image
            panel.transform.localScale = new Vector3(1, 1.2f, 1);
        }
        else{
            panel.transform.localScale = new Vector3(1, 1, 1);
        }

        imageObject.sprite = objectToZoom.GetComponent<SpriteRenderer>().sprite;
        GameMaster.Instance.imageZoomOpen = true;
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void CloseImageZoom(){
        GameMaster.Instance.imageZoomOpen = false;
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
