using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EndGameCondition{
    IreneFinished,
    IreneDied,
    AllDead
}

public class GameManager: MonoBehaviour
{
    private static GameManager _instance;
    [SerializeField] private CardUIController cardUIController;
    [SerializeField] private DefeatUIController defeatUIController;

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

    private bool _hasLost = false;
    public bool hasLost {get {return _hasLost;} }

    void Start()
    {
        _hasLost = false;
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
                _hasLost = true;
                defeatUIController.ShowDefeatScreen("Mission failed: all troops died");
                break;

            case EndGameCondition.IreneDied:
                _hasLost = true;
                defeatUIController.ShowDefeatScreen("Mission failed: Irene died");
                break;

            case EndGameCondition.IreneFinished:
                Debug.Log("Passou de fase! Irene chegou viva ao outro lado.");
                break;
        }
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

}

