using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    [SerializeField] private int dialogueSceneIndex = 1;
    [SerializeField] private int combatSceneIndex = 2;

    public void LoadInitialScene()
    {
        if (SceneTracker.sceneArgs.Count == 0) {
            Debug.LogWarning("No objects in Main Game Data! (Check main UI's MainInitializer component)");
            return;
        } else if (SceneTracker.sceneArgs.Peek() is CombatArgs) {
            SceneManager.LoadScene(combatSceneIndex);
        } else if (SceneTracker.sceneArgs.Peek() is DialogueArgs) {
            SceneManager.LoadScene(dialogueSceneIndex);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
