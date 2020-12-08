using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainInitializer : MonoBehaviour
{
    [SerializeField] private MainGameContent mainGameContent;
    void Awake()
    {
        SceneTracker.sceneArgs = new Queue<SceneArgs>();

        foreach(SceneArgs sceneArgs in mainGameContent.mainGameContent){
            SceneTracker.sceneArgs.Enqueue(sceneArgs);
        }
    }
}