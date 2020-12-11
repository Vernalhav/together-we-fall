using UnityEngine;
using System.Collections.Generic;

public static class SceneTracker
{
    public static Queue<SceneArgs> sceneArgs = new Queue<SceneArgs>();
    public static Deck playerDeck;
}

public enum SceneIndexes
{
    MainMenu = 0,
    DialogueScene = 1,
    CombatScene = 2
}