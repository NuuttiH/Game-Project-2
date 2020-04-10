using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManagerIntro : MonoBehaviour{
    public static DialogueManagerIntro Instance;
    [Header("GUI Reference")]
    public AudioSource audioSource;
    public GameObject[] texts;
    public GameObject endButton;
    private int currentText = 0;

    [Header("Delays for text display")]
    public float letterDelay;
    public float dialogueDelay;
    public float dialogueAfterDelay;

    private Queue<DialogueLineIntro> lines;
    private DialogueIntro dialogue;
    private TextMeshProUGUI textSpace;
    
    void Awake(){
        Instance = this;
    }

    void Start(){
        lines = new Queue<DialogueLineIntro>();
        for(int i=1; i<texts.Length; i++){
            texts[i].SetActive(false);
        }
        endButton.SetActive(false);
        texts[0].GetComponent<Button>().onClick.AddListener(StartDialogue);
    }

    public void StartDialogue(){
        texts[currentText].GetComponent<Button>().onClick.RemoveListener(StartDialogue);
        if(currentText!=0) texts[currentText-1].SetActive(false);

        dialogue = texts[currentText].GetComponent<DialogueIntro>();
        textSpace = texts[currentText].GetComponent<TextMeshProUGUI>();
        textSpace.text = "";

        lines.Clear();
        foreach(DialogueLineIntro line in dialogue.lines){
            lines.Enqueue(line);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence(){
        if(lines.Count==0){
            StartCoroutine(EndDialogue());
            return;
        }

        DialogueLineIntro line = lines.Dequeue();    
        StopAllCoroutines();
        StartCoroutine(TypeText(line.sentence));
        if(line.audioClip != null){
            audioSource.Stop();
            audioSource.PlayOneShot(line.audioClip);
        }

        //StartCoroutine(ContinueToNextSentence()); in TypeText()
    }
    IEnumerator TypeText(string text){
        textSpace.text = "";
        foreach(char c in text.ToCharArray()){
            textSpace.text += c;
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
        
        currentText++;
        if(currentText!=texts.Length){
            texts[currentText].SetActive(true);
            texts[currentText].GetComponent<Button>().onClick.AddListener(StartDialogue);
        }
        else{
            endButton.SetActive(true);
            endButton.GetComponent<Button>().onClick.AddListener(NextScene);
        }
    }

    public void NextScene(){
        GameMaster.Instance.StartScene(2);
    }
}
