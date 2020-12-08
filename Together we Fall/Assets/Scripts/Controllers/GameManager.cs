using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public enum EndGameCondition{
    IreneFinished,
    IreneDied,
    AllDead
}

public class GameManager: MonoBehaviour
{
    private static GameManager _instance;
    [SerializeField] private CardUIController cardUIController;
    [SerializeField] private CombatFader combatFader;

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
    [SerializeField] private GameObject defaultLevelPrefab;

    [SerializeField] private AudioSource combatThemeAudioSource;
    [SerializeField] private AudioSource motifAudioSource;
    [SerializeField] private AudioClip victoryMotif;
    [SerializeField] private AudioClip defeatMotif;

    private void Awake()
    {
        combatThemeAudioSource.volume = 0;

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

        DOTween.To(() => combatThemeAudioSource.volume, (float x) => combatThemeAudioSource.volume = x, 1f, 10f);
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

    public void LevelCompleted(EndGameCondition condition)
    {
        NormalizeTimeScale();

        DOTween.To(() => combatThemeAudioSource.volume, (float x) => combatThemeAudioSource.volume = x, 0f, 1f);

        if (condition == EndGameCondition.AllDead || condition == EndGameCondition.IreneDied) {
            _hasLost = true;
            combatFader.ShowDefeatScreen("Mission failed",
                    onFadeInStart: () => motifAudioSource.PlayOneShot(defeatMotif), 
                    onFadeInEnd: () => Time.timeScale = 0 );
        }
        else if (condition == EndGameCondition.IreneFinished){
            motifAudioSource.PlayOneShot(victoryMotif);
            if (SceneTracker.sceneArgs.Count > 0)
                SceneTracker.sceneArgs.Dequeue();
            
            if (SceneTracker.sceneArgs.Count == 0) {
                Debug.Log("Acabou o jogo!");
                combatFader.TransitionToScene(SceneIndexes.MainMenu, fadeDuration: 2.5f);
            }
            else {
                combatFader.TransitionToScene(SceneIndexes.DialogueScene, fadeDuration: 2.5f);
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

