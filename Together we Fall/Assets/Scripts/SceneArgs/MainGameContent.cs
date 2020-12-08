using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="DialogueArgs", menuName="Main Game Content")]
public class MainGameContent : ScriptableObject
{
    [SerializeField] public List<SceneArgs> mainGameContent;
}
