﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopsTracker : MonoBehaviour
{
    public static Action<CombatentTypesEnum> OnTroopDied;
    public static Action OnTroopFinished;
    public static Action OnAliveOnBattlefieldZeroed;
    public static Action OnIreneFinished;
    
    [SerializeField] private int troopsFinished;
    public bool ireneFinished {get; set;}
    [SerializeField] private int aliveOnBattlefield;
    [SerializeField] private Card soldierCard;
    [SerializeField] private Card runnerCard;
    [SerializeField] private Card tankCard;
    [SerializeField] private Card ireneCard;

    public bool irenePassed {get; set;}

    public bool AllSoldiersDead {
        get 
        {
            return tankCard.aliveCounter == 0 && soldierCard.aliveCounter == 0 && runnerCard.aliveCounter == 0;
        }
    }


    private GameManager gameManager;
    public CardHandler cardHandler;

    private void Start()
    {
        troopsFinished = 0;
        SubscribeEvents();
        gameManager = GameManager.Instance;
    }

    private void SubscribeEvents()
    {
        OnTroopDied += TroopDied;
        OnTroopDied += DecrementAliveOnField;
        OnTroopFinished += TroopFinished;
        OnTroopFinished += DecrementAliveOnField;
        CardHandler.OnCardDeploy += IncrementAliveOnField;
        OnIreneFinished += IreneFinished;
    }

    private void OnDestroy()
    {
        OnTroopDied -= TroopDied;
        OnTroopDied -= DecrementAliveOnField;
        OnTroopFinished -= TroopFinished;
        OnTroopFinished -= DecrementAliveOnField;
        CardHandler.OnCardDeploy -= IncrementAliveOnField;
        OnIreneFinished -= IreneFinished;
    }

    private void TroopFinished()
    {
        troopsFinished++;
    }

    public void IreneFinished(){
        Debug.Log("Irene passed!");
        ireneFinished = true;
    }

    public void IncrementAliveOnField(){
        Debug.Log("decrementei");
        aliveOnBattlefield++;
    }

    private void DecrementAliveOnField(CombatentTypesEnum c)
    {
        if (c != CombatentTypesEnum.Enemy)
        {
            if (aliveOnBattlefield > 0)
            {
                aliveOnBattlefield--;
            }
            else
            {
                Debug.LogWarning("Tentou decrementar quando não deveria.");
            }
        }

        CheckTroopsCondition();
    }

    private void CheckTroopsCondition()
    {
        if (aliveOnBattlefield == 0)
        {
            if(ireneCard.aliveCounter == 0){
                gameManager.LevelCompleted(EndGameCondition.IreneDied);
            }
            else if (ireneFinished)
            {
                gameManager.LevelCompleted(EndGameCondition.IreneFinished);
            }
            else if (AllSoldiersDead)
            {
                gameManager.LevelCompleted(EndGameCondition.AllDead);
            }
        }
    }

    private void DecrementAliveOnField(){
        if(aliveOnBattlefield > 0){
            aliveOnBattlefield--;
        }else{
            Debug.LogWarning("Tentou decrementar quando não deveria.");
        }

        CheckTroopsCondition();
    }


    public void TroopDied(CombatentTypesEnum type){

        switch (type)
        {
            case CombatentTypesEnum.Tank:
                DecreaseCardCounter(tankCard);
                break;

            case CombatentTypesEnum.Soldier:
                DecreaseCardCounter(soldierCard);
                break;
                
            case CombatentTypesEnum.Runner:
                DecreaseCardCounter(runnerCard);
                break;
            case CombatentTypesEnum.Irene:
                DecreaseCardCounter(ireneCard);
                break;
        }
    }

    public void DecreaseCardCounter(Card c){
        if(c.aliveCounter > 0){
            c.aliveCounter--;
        }else{
            Debug.LogWarning("Tentou decrementar a carta " + c + " quando não deveria.");
        }
    }
}
