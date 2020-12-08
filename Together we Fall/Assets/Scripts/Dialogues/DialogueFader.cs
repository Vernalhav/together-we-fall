using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class DialogueFader : MonoBehaviour
{
    [SerializeField]
    private Image blackSquare;
    [SerializeField]
    private TextMeshProUGUI fadeText;

    public Color overlayColor;
    public Color textColor;

    void Awake()
    {
        blackSquare.color = new Color(overlayColor.r, overlayColor.g, overlayColor.b, 1);
        fadeText.color = new Color(textColor.r, textColor.g, textColor.b, 0);
    }

    private void Start() {
        blackSquare.gameObject.SetActive(true);
        
        Sequence fadeSequence = DOTween.Sequence();
        fadeSequence.AppendInterval(1f)
                    .Append(blackSquare.DOFade(0f, 1f));
    }

    private void OnValidate() {
        Awake();
    }

    public void TransitionToScene(SceneIndexes sceneIndex, int fadeDuration = 1) {
        Sequence fadeSequence = DOTween.Sequence();
        fadeSequence.AppendInterval(1f)
                    .Append(blackSquare.DOFade(1f, fadeDuration))
                    .AppendCallback(() => SceneManager.LoadScene((int)sceneIndex));
    }

    /* Duration is the time in seconds that the screen will be fully blacked out after text appeared */
    public void Fade(TweenCallback OnFadeInEnd, TweenCallback OnFadeOutEnd,
                    float duration = 3, float textFadeDuration = 1, float fadeDuration = 1, string text = "") {

        fadeText.text = text;

        // Sequence of tweens that create the fade effect
        Sequence fadeSequence = DOTween.Sequence();
        fadeSequence.Append(blackSquare.DOFade(1f, fadeDuration))
                    .AppendCallback(OnFadeInEnd)
                    .Append(DOTween.ToAlpha(() => fadeText.color, x => fadeText.color = x, 1f, textFadeDuration))
                    .AppendInterval(duration)
                    .Append(DOTween.ToAlpha(() => fadeText.color, x => fadeText.color = x, 0f, textFadeDuration))
                    .Append(blackSquare.DOFade(0f, fadeDuration))
                    .AppendCallback(OnFadeOutEnd);
    }
}
