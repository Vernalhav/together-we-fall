using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewConversation", menuName = "Dialogues/New Conversation")]
public class ConversationData : ScriptableObject
{
    public DialogueInfo[] dialogues;
}

[System.Serializable]
public struct DialogueInfo
{
    public string speakerName;
    public Sprite speakerSprite;
    public Sprite backgroundImage;
    public Sprite imageEffect;
    public bool setShadow;

    public DialogueData dialogue;    

    public string fadeOutText;
    public AudioClip fadeOutSound;
}