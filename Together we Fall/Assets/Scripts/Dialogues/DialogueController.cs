using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


/*
    A conversation is made up of several Dialogues.
    A Dialogue involves a speaker and has either a binary choice, or no choice at all.
    Each choice leads to a different Dialogue.
    If there are no choices, the next Dialogue in the Conversation queue will be loaded.
    A conversation may have Dialogues of different speakers.
*/
public class DialogueController : MonoBehaviour
{
    private TextMeshProUGUI dialogueContentUI;
    private TextMeshProUGUI speakerNameUI;
    private TextMeshProUGUI firstButton;
    private TextMeshProUGUI secondButton;
    private List<GameObject> answerPanels = new List<GameObject>();

    private RectTransform storyUI;
    private RectTransform choiceButtonsGroup;

    [SerializeField]
    private Image backgroundImage;
    [SerializeField]
    private Image speakerSprite;

    public ConversationData conversation;
    private DialogueData currentDialogue;

    private int currentDialogueIndex;   // 1-based
    [SerializeField]
    private DialogueFader fader;


    [Header("Text typing settings")]
    [SerializeField]
    private float charTypeDelay;
    [SerializeField]
    private int commaDelayMultiplier = 5;
    [SerializeField]
    private int punctuationDelayMultiplier = 10;
    [SerializeField]
    private AudioSource typeAudioSource;
    [SerializeField]
    private AudioClip typeAudio;

    private string[] lines;
    private int currentLineIndex;
    private int maxLines;

    private bool isFading = false;

    private bool isTyping = false;
    private IEnumerator writingCoroutine;  // Reference to the typing coroutine (function)


    void Awake()
    {
        RectTransform mainPanelTransform = GameObject.FindGameObjectWithTag("MainPanel").GetComponent<RectTransform>();
        storyUI = (RectTransform)mainPanelTransform.Find("StoryUI");
        choiceButtonsGroup = (RectTransform)mainPanelTransform.Find("ChoiceButtons");

        dialogueContentUI = storyUI.Find("DialogueContentUI").GetComponentInChildren<TextMeshProUGUI>();
        speakerNameUI = storyUI.Find("SpeakerNameUI").GetComponentInChildren<TextMeshProUGUI>();
        firstButton = choiceButtonsGroup.Find("Answer0").GetComponentInChildren<TextMeshProUGUI>();
        secondButton = choiceButtonsGroup.Find("Answer1").GetComponentInChildren<TextMeshProUGUI>();

        foreach(Transform answerPanel in choiceButtonsGroup) {
            answerPanels.Add(answerPanel.gameObject);
        }

        currentDialogueIndex = 0;
        SetupNextDialogue();
    }

    private void SetupDialogueSprites(DialogueInfo dialogueInfo)
    {
        speakerNameUI.text = dialogueInfo.speakerName;
        backgroundImage.sprite = dialogueInfo.backgroundImage;
        speakerSprite.sprite = dialogueInfo.speakerSprite;
        
        dialogueContentUI.text = "";
    }

    private void SetupNextDialogue()
    {
        if (currentDialogueIndex == 0){
            DialogueInfo initialDialogueInfo = conversation.dialogues[currentDialogueIndex++];
            SetupDialogueSprites(initialDialogueInfo);
            SetupDialogue(initialDialogueInfo.dialogue);
            return;
        }

        if (currentDialogueIndex >= conversation.dialogues.Length){
            // Debug.Log("Mudamos a cena :)");
            return;
        }

        string fadeOutText = conversation.dialogues[currentDialogueIndex - 1].fadeOutText;
        AudioClip fadeClip = conversation.dialogues[currentDialogueIndex - 1].fadeOutSound;
        DialogueInfo nextDialogue = conversation.dialogues[currentDialogueIndex++];

        TweenCallback onFadeInEnd = () => {
            SetupDialogueSprites(nextDialogue);
            if (fadeClip != null)
                typeAudioSource.PlayOneShot(fadeClip);
        };

        TweenCallback onFadeOutEnd = () => {
            SetupDialogue(nextDialogue.dialogue);
            setIsFading(false);
        };
        
        setIsFading(true);
        fader.Fade(onFadeInEnd, onFadeOutEnd, text: fadeOutText);
    }

    private void setIsFading(bool value)
    {
        isFading = value;
    }

    private void SetupDialogue(DialogueData dialogue)
    {
        currentDialogue = dialogue;

        lines = dialogue.story.Split('\n');
        maxLines = lines.Length;
        currentLineIndex = 0;

        WriteNextLine();

        if (dialogue.answers.Length > 0){
            firstButton.text = dialogue.opt1;
            secondButton.text = dialogue.opt2;
        }
    }

    public void SetButtonsEnabled(bool state)
    {
        foreach(GameObject panel in answerPanels) {
            panel.GetComponentInChildren<Button>().interactable = state;
        }
    }

    public void SelectAnswer(int answer)
    {
        SetupDialogue(currentDialogue.answers[answer]);
        HideAnswers();
    }

    private void HideAnswers()
    {
        SetButtonsEnabled(false);
        storyUI.DOAnchorPosY(-105f, 0.6f);
        choiceButtonsGroup.DOAnchorPosY(-115f, 0.6f);
    }

    private void ShowAnswers()
    {
        SetButtonsEnabled(true);
        storyUI.DOAnchorPosY(-18f, 0.6f);
        choiceButtonsGroup.DOAnchorPosY(-20f, 0.6f);
    }

    private float GetTypeDelay(char[] text, int i)
    {
        if (text[i] == ',' || text[i] == ';') return commaDelayMultiplier*charTypeDelay;
        if (text[i] == '.') return punctuationDelayMultiplier*charTypeDelay;
        if (text[i] == '!' || text[i] == '?') {
            if (i == text.Length - 1 || text[i + 1] == '!' || text[i + 1] == '?') return charTypeDelay;
            return punctuationDelayMultiplier*charTypeDelay;
        }
        return charTypeDelay;
    }

    private void WriteNextLine()
    {
        if (isTyping && writingCoroutine != null){
            Debug.LogWarning("Somehow the typing animation is playing and WriteNextLine was called!");
            StopCoroutine(writingCoroutine);  // Should never execute
        }

        writingCoroutine = WriteNextLineCoroutine();
        StartCoroutine(writingCoroutine);
    }

    private void SkipTypingAnimation()
    {
        if (isTyping && writingCoroutine != null && dialogueContentUI.text != lines[currentLineIndex - 1]){
            StopCoroutine(writingCoroutine);
            isTyping = false;
            dialogueContentUI.text = lines[currentLineIndex - 1];
        }
    }

    private IEnumerator WriteNextLineCoroutine()
    {
        string nextLine = lines[currentLineIndex++];

        char[] textString = nextLine.ToCharArray();
        dialogueContentUI.text = "";

        isTyping = true;
        for (int i = 0; i < textString.Length; i++){
            dialogueContentUI.text += textString[i];
            
            if (textString[i] != ' ' && textString[i] != '\n' && textString[i] != '\r')
                typeAudioSource.PlayOneShot(typeAudio);
            yield return new WaitForSeconds( GetTypeDelay(textString, i) );
        }
        isTyping = false;
    }

    public void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)) && !isFading){

            if (isTyping){
                SkipTypingAnimation();
                return;
            }

            if (currentLineIndex == lines.Length && currentDialogue.answers.Length == 0) {
                SetupNextDialogue();
                return;
            }

            if (currentLineIndex < lines.Length) {
                WriteNextLine();
                return;
            }

            if (currentLineIndex == lines.Length && currentDialogue.answers.Length == 2) {
                ShowAnswers();
                return;
            }

            if (currentLineIndex == lines.Length && currentDialogue.answers.Length == 1) {
                SetupDialogue(currentDialogue.answers[0]);
                return;
            }
        }
    }
}
