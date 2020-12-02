using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogues/New Dialogue")]
public class DialogueData : ScriptableObject
{
    [TextArea(2, 60)]
    public string story;

    [TextArea(2, 10)]
    public string opt1;
    [TextArea(2, 10)]
    public string opt2;

    public DialogueData[] answers;
}
