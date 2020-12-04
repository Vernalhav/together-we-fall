using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EndGameCondition{
    IreneFinished,
    IreneDied,
    AllDead
}

public class GameManager: MonoBehaviour
{
    private static GameManager _instance;

    public static Action OnLevelFinished;

    public TroopsTracker troopTracker;

    [SerializeField] private float acceleratedTimeScale = 2f;

    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }

    void Start()
    {
        TroopsTracker.OnIreneFinished += AccelerateTime;
        OnLevelFinished += DesaccelerateTime;
        TroopsTracker.OnAliveOnBattlefieldZeroed += CheckLevelCompleted;
        troopTracker = GetComponent<TroopsTracker>();
    }

    void OnDestroy()
    {
        TroopsTracker.OnIreneFinished -= AccelerateTime;
        OnLevelFinished -= DesaccelerateTime;
        TroopsTracker.OnAliveOnBattlefieldZeroed -= CheckLevelCompleted;
    }

    public void AccelerateTime(){
        Time.timeScale = acceleratedTimeScale;
    }

    public void DesaccelerateTime(){
        Time.timeScale = 1;
    }



    private void CheckLevelCompleted(){
        if(troopTracker.irenePassed){
            LevelCompleted(EndGameCondition.IreneFinished);
        }else{
            if(troopTracker.AllDead){
                LevelCompleted(EndGameCondition.AllDead);
            }else if()
        }
    }

    public void LevelCompleted(EndGameCondition condition ){

    }

}

