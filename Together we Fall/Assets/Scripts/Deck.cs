using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class Deck : ScriptableObject
{
    public List<Card> cards;


    public void ChangeCardAliveCount(int idx, int count){
        if(0 <= idx && idx < cards.Count){
            cards[idx].aliveCounter = count;
    	    EditorUtility.SetDirty(cards[idx]);
        }
    }

    public void SaveCards(){
        foreach(Card c in cards){
            EditorUtility.SetDirty(c);
        }
    }
}
