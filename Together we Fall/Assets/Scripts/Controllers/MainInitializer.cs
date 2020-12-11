using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainInitializer : MonoBehaviour
{
    [SerializeField] private MainGameContent mainGameContent;

    [SerializeField] private Deck deck;
    //runner soldier tank irene
    [SerializeField] private List<int> cardCounts;

    void Awake()
    {
        SceneTracker.sceneArgs = new Queue<SceneArgs>();

        foreach(SceneArgs sceneArgs in mainGameContent.mainGameContent){
            SceneTracker.sceneArgs.Enqueue(sceneArgs);
            Debug.Log(sceneArgs);
        }

    }

    void Start()
    {
        InitializeDeck();       
    }


    public void InitializeDeck(){
        for(int i = 0; i < deck.cards.Count; i ++){
            deck.ChangeCardAliveCount(i, cardCounts[i] ); 
        }
    }
}