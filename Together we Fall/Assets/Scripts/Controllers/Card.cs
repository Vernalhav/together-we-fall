using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Card : ScriptableObject
{
    public int aliveCounter;
    public GameObject soldierPrefab;
    public CombatentTypesEnum cardType;
    
    private void Awake() {
        cardType = soldierPrefab.GetComponent<Soldier>().data.myType;
    }
    
    private void OnValidate() {
        Awake();
    }
}
