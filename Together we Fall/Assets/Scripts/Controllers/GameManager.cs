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
    [SerializeField] private CardUIController cardUIController;

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
        TroopsTracker.OnIreneFinished += SpeedUpGame;
        OnLevelFinished += NormalizeTimeScale;
    }

    void OnDestroy()
    {
        TroopsTracker.OnIreneFinished -= SpeedUpGame;
        OnLevelFinished -= NormalizeTimeScale;
    }

    public void SpeedUpGame(){
        Time.timeScale = acceleratedTimeScale;
        cardUIController.BlockUserInput();
    }

    public void NormalizeTimeScale(){
        Time.timeScale = 1f;
        cardUIController.AllowUserInput();
    }

    public void LevelCompleted(EndGameCondition condition){
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

