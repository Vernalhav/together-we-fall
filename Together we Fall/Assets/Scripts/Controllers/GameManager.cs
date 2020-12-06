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
        OnLevelFinished += NormalizeTimeScale;
    }

    void OnDestroy()
    {
        TroopsTracker.OnIreneFinished -= AccelerateTime;
        OnLevelFinished -= NormalizeTimeScale;
    }

    public void AccelerateTime(){
        Time.timeScale = acceleratedTimeScale;
    }

    public void NormalizeTimeScale(){
        Time.timeScale = 1f;
    }

    public void LevelCompleted(EndGameCondition condition ){
        NormalizeTimeScale();

        switch(condition){
            case EndGameCondition.AllDead:
                Debug.Log("Perdeu! Todos Morreram.");
                break;
            case EndGameCondition.IreneDied:
                Debug.Log("Perdeu! Irene Morreu");
                break;
            case EndGameCondition.IreneFinished:
                Debug.Log("Passou de fase! Irene chegou viva ao outro lado.");
                break;

        }
    }

}

