using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour, IPointerClickHandler{
    public static DialogueManager Instance;
    [Header("GUI Reference")]
    public CanvasGroup dialogueCanvas;
    public AudioSource audioSource;
    public Image image;
    public Text title;
    public Text textbox;

    [Header("Delays for text display")]
    public float letterDelay;
    public float dialogueDelay;
    public float dialogueAfterDelay;

    private Queue<DialogueLine> lines;
    private DialogueLine line;
    private int phase;
    
    void Awake(){
        Instance = this;
    }

    void Start(){
        lines = new Queue<DialogueLine>();
        DisableCanvasGroup(dialogueCanvas);
        phase = 0;  // Not writing, not displaying
    }

    public void StartDialogue(Dialogue dialogue){
        lines.Clear();
        EnableCanvasGroup(dialogueCanvas);

        foreach(DialogueLine line in dialogue.lines){
            lines.Enqueue(line);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence(){
        if(lines.Count==0){
            phase = 2;  // Not writing, display
            StartCoroutine(EndDialogue());
            return;
        }

        line = lines.Dequeue();
        image.sprite = line.sprite;
        title.text = line.name;
        StopAllCoroutines();
        StartCoroutine(TypeText(line.sentence));
        if(line.audioClip != null){
            audioSource.Stop();
            audioSource.PlayOneShot(line.audioClip);
        }

        //StartCoroutine(ContinueToNextSentence()); in TypeText()
    }
    IEnumerator TypeText(string text){
        phase = 1;  // Writing
        textbox.text = "";
        foreach(char c in text.ToCharArray()){
            textbox.text += c;
            yield return new WaitForSeconds(letterDelay);
        }
        StartCoroutine(ContinueToNextSentence());
    }
    IEnumerator ContinueToNextSentence(){
        yield return new WaitForSeconds(dialogueDelay);
        DisplayNextSentence();
    }
    IEnumerator EndDialogue(){
        yield return new WaitForSeconds(dialogueAfterDelay);
        DisableCanvasGroup(dialogueCanvas);
        phase = 0;  // Not writing, not displaying
    }

    public void OnPointerClick(PointerEventData pointerEventData){
        switch(phase){
            case 1:     // Writing
                Debug.Log("CASE 1");
                StopAllCoroutines();
                textbox.text = line.sentence;
                phase = 2;  // Not writing, display
                StartCoroutine(ContinueToNextSentence());
                break;
            case 2:     // Not writing, displaying
                Debug.Log("CASE 2");
                if(lines.Count==0){
                    DisableCanvasGroup(dialogueCanvas);
                    audioSource.Stop();
                    phase = 0;  // Not writing, not displaying
                }
                else{
                    StopAllCoroutines();
                    DisplayNextSentence();
                }
                break;
            default:    // Not writing, not displaying
                Debug.Log("CASE DEFAULT");
                break;
        }
    }

    private void EnableCanvasGroup(CanvasGroup canvasGroup){
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
    private void DisableCanvasGroup(CanvasGroup canvasGroup){
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
