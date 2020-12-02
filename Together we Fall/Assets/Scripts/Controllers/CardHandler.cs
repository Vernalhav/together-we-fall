using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHandler : MonoBehaviour
{
    public Deck playerDeck;
    public Dictionary<Card, int> cardCounter;
    public Card selectedCard;


    private void Start()
    {
        cardCounter = new Dictionary<Card, int>();
        
        foreach(Card c in playerDeck.cards)
        {
            cardCounter.Add(c, c.initQnt);
        }
    }

    public void SelectCard(Card _c)
    {
        selectedCard = _c;
    }

    public void DecreaseCardCount()
    {
        if (cardCounter[selectedCard] <= 0) return;

        cardCounter[selectedCard]--;
        
        if(cardCounter[selectedCard] == 0)
        {
            Debug.Log("acabaram as tropas");
        }
  
    }

    public bool HasTroops()
    {
        //Debug.Log(cardCounter[selectedCard] != 0);
        return cardCounter[selectedCard] != 0;
    }
    
}
