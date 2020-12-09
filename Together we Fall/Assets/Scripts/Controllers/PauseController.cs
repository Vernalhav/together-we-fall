using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private CanvasGroup pauseGroup;
    [SerializeField] private CanvasGroup pauseButtonGroup;
    [SerializeField] private Sprite pauseImage;
    [SerializeField] private Sprite unpauseImage;
    [SerializeField] private Image buttonImage;

    [SerializeField] private AudioMixer masterMixer;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private Slider musicSlider;

    private float previousTimeScale;

    private bool _isPaused = false;
    public bool isPaused { get { return _isPaused; } }

    private void Start()
    {
        previousTimeScale = Time.timeScale;

        _isPaused = false;
        pauseButtonGroup.alpha = 0.5f;
        buttonImage.sprite = pauseImage;

        pauseGroup.gameObject.SetActive(false);

        float SFXvalue;
        masterMixer.GetFloat("SFXVolume", out SFXvalue);
        float musicValue;
        masterMixer.GetFloat("MusicVolume", out musicValue);

        SFXSlider.value = Mathf.Pow(10, SFXvalue/20);
        musicSlider.value = Mathf.Pow(10, musicValue/20);
    }

    public void PauseUnpause()
    {
        if (!_isPaused) {
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0;

            buttonImage.sprite = unpauseImage;
            pauseGroup.gameObject.SetActive(true);
        }
        else {
            Time.timeScale = previousTimeScale;
            buttonImage.sprite = pauseImage;
            pauseGroup.gameObject.SetActive(false);
        }

        _isPaused = !_isPaused;
    }

    public void SetSFXVolume(float volume)
    {
        masterMixer.SetFloat("SFXVolume", Mathf.Log10(volume)*20);
    }

    public void SetMusicVolume(float volume)
    {
        masterMixer.SetFloat("MusicVolume", Mathf.Log10(volume)*20);
    }

    public void OnPointerEnter(PointerEventData e)
    {
        pauseButtonGroup.alpha = 1f;
    }

    public void OnPointerExit(PointerEventData e)
    {
        pauseButtonGroup.alpha = 0.5f;
    }

    public void QuitGame()
    {
        SceneManager.LoadScene((int)SceneIndexes.MainMenu);
    }
}