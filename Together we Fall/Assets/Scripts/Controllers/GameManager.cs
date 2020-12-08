﻿using System;
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
    public bool hasLost { get {return _hasLost;} }
    [SerializeField] GameObject defaultLevelPrefab;

    private void Awake()
    {
        if (SceneTracker.sceneArgs.Count > 0 && SceneTracker.sceneArgs.Peek() is CombatArgs){
            CombatArgs currentLevel = SceneTracker.sceneArgs.Peek() as CombatArgs;
            Instantiate(currentLevel.mapPrefab);
        }
        else {
            Instantiate(defaultLevelPrefab);
        }
    }

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

        if (condition == EndGameCondition.AllDead || condition == EndGameCondition.IreneDied) {
            _hasLost = true;
            defeatUIController.ShowDefeatScreen("Mission failed", () => { Time.timeScale = 0; } );
        }
        else if (condition == EndGameCondition.IreneFinished){
            if (SceneTracker.sceneArgs.Count > 0)
                SceneTracker.sceneArgs.Dequeue();
            
            if (SceneTracker.sceneArgs.Count == 0) {
                Debug.Log("Acabou o jogo!");
                SceneManager.LoadScene((int)SceneIndexes.MainMenu);
            }
            else {
                SceneManager.LoadScene((int)SceneIndexes.DialogueScene);
            }
        }
    }

    public void ResetLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene((int)SceneIndexes.CombatScene);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene((int)SceneIndexes.MainMenu);
    }

}

