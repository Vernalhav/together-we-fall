using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public void LoadInitialScene()
    {
        if (SceneTracker.sceneArgs.Count == 0) {
            Debug.LogWarning("No objects in Main Game Data! (Check main UI's MainInitializer component)");
            return;
        }
        else if (SceneTracker.sceneArgs.Peek() is CombatArgs) {
            Debug.Log(SceneTracker.sceneArgs.Peek());
            SceneManager.LoadScene((int)SceneIndexes.CombatScene);
        }
        else if (SceneTracker.sceneArgs.Peek() is DialogueArgs) {
            SceneManager.LoadScene((int)SceneIndexes.DialogueScene);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
