using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewConversation", menuName = "Dialogues/New Conversation")]
public class ConversationData : ScriptableObject
{
    // public DialogueData[] dialogues;
    // [Tooltip("Which strings should appear between dialogue transitions. Size should always be dialogues.Length - 1. Leave empty string for no text")]
    // public string[] fadeOutTexts;

    public DialogueInfo[] dialogues;
}

[System.Serializable]
public struct DialogueInfo
{
    public string speakerName;
    public Sprite speakerSprite;
    public Sprite backgroundImage;
    public bool setShadow;

    public DialogueData dialogue;    
    public string fadeOutText;
}