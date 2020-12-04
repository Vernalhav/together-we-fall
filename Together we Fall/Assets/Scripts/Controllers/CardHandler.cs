using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHandler : MonoBehaviour
{
    public Deck playerDeck;
    public Dictionary<Card, int> cardCounterInGame;
    public Card selectedCard;

    private void Start()
    {
        cardCounterInGame = new Dictionary<Card, int>();
        
        foreach(Card c in playerDeck.cards)
        {
            cardCounterInGame.Add(c, c.aliveCounter);
        }
    }

    public void SelectCard(Card _c)
    {
        selectedCard = _c;
    }

    public void DecreaseCardCount()
    {
        if (cardCounterInGame[selectedCard] <= 0) return;

        cardCounterInGame[selectedCard]--;
        
        if(cardCounterInGame[selectedCard] == 0)
        {
            Debug.Log("acabaram as tropas");
        }
  
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
}
