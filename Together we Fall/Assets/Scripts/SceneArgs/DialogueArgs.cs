using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName="DialogueArgs", menuName="Scene Arguments/Dialogue Arguments")]
public class DialogueArgs : SceneArgs
{
    public ConversationData currentConversation;
}
