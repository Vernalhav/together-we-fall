using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopsTracker : MonoBehaviour
{
    public static Action<CombatentTypesEnum> OnTroopDied;
    public static Action OnTroopFinished;
    public static Action OnIreneFinished;
    
    [SerializeField] private int howManyFinished;
    [SerializeField] private Card soldierCard;
    [SerializeField] private Card runnerCard;
    [SerializeField] private Card tankCard;

    private GameManager gameManager;
    public CardHandler cardHandler;

    private void Start()
    {
        SubscribeEvents();
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void SubscribeEvents()
    {
        OnTroopDied += TroopDied;
        OnTroopFinished += TroopFinished;
        OnIreneFinished += IreneFinished;
    }

    private void OnDestroy()
    {
        OnTroopDied -= TroopDied;
        OnTroopFinished -= TroopFinished;
        OnIreneFinished -= IreneFinished;
    }

    private void TroopFinished()
    {
        howManyFinished++;
        if (howManyFinished == TotalAlive()) Debug.Log("Passou de fase!!");
    }

    private void IreneFinished()
    {
        gameManager.LevelCompleted();
    }

    public void TroopDied(CombatentTypesEnum type){

        Debug.Log(type + "died");
        switch (type)
        {
            case CombatentTypesEnum.Tank:
                if(tankCard.aliveCounter > 0)
                    tankCard.aliveCounter--;
                break;

            case CombatentTypesEnum.Soldier:
                if (soldierCard.aliveCounter > 0)
                    soldierCard.aliveCounter--;
                break;
                
            case CombatentTypesEnum.Runner:
                if (runnerCard.aliveCounter > 0)
                    runnerCard.aliveCounter--;
                break;
        }

        if(AllDead())
        {
            Debug.Log("Perdeu o jogo, todos morreram!");
        }
    }

    private bool AllDead()
    {
        return tankCard.aliveCounter == 0 && soldierCard.aliveCounter == 0 && runnerCard.aliveCounter == 0;
    }

    private int TotalAlive()
    {
        return soldierCard.aliveCounter + tankCard.aliveCounter + runnerCard.aliveCounter;
    }
}
