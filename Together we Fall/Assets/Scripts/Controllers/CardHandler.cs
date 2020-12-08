using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHandler : MonoBehaviour
{
    
    public static Action OnCardDeploy;
    public Deck playerDeck;
    public Dictionary<Card, int> cardCounterInGame;
    [HideInInspector]
    public Card selectedCard;

    private void Start()
    {
        OnCardDeploy += DecreaseCardCount;

        cardCounterInGame = new Dictionary<Card, int>();
        
        foreach(Card c in playerDeck.cards)
        {
            cardCounterInGame.Add(c, c.aliveCounter);
        }

    }

    void OnDestroy()
    {
        OnCardDeploy -= DecreaseCardCount;
    }

    public void SelectCard(Card _c)
    {
        selectedCard = _c;
    }

    public void DecreaseCardCount()
    {
        if (cardCounterInGame[selectedCard] <= 0) return;

        cardCounterInGame[selectedCard]--;
    }

    public int GetTroopCount(Card _c)
    {
        return cardCounterInGame[_c];
    }

    public bool HasTroops()
    {
        //Debug.Log(cardCounter[selectedCard] != 0);
        return cardCounterInGame[selectedCard] != 0;
    }
    
    //returns the number of soldiers alive + 1 (it includes Irene)
    public int ArmySize()
    {
        int count = 0;
        foreach(KeyValuePair<Card, int> c in cardCounterInGame)
        {
            count += c.Value;
        }

        return count;
    }

    [ContextMenu("Refresh player deck")]
    public void RefreshPlayerDeck()
    {
        foreach(Card _c in playerDeck.cards){
            _c.aliveCounter = 100;
            if (_c.cardType == CombatentTypesEnum.Irene)
                _c.aliveCounter = 1;
        }
    }

}
