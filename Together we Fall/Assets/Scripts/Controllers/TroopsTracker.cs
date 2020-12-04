using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopsTracker : MonoBehaviour
{
    [SerializeField] private int totalAlive;
    [SerializeField] private Card soldierCard;
    [SerializeField] private Card runnerCard;
    [SerializeField] private Card tankCard;
    public static Action<CombatentTypesEnum> OnTroopDied;
    public CardHandler cardHandler;
    // Start is called before the first frame update
    private void Awake()
    {
        OnTroopDied += TroopDied;   
    }

    private void Start()
    {
        totalAlive = soldierCard.aliveCounter + tankCard.aliveCounter + runnerCard.aliveCounter;
    }
    private void OnDestroy()
    {
        OnTroopDied -= TroopDied;
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
}
