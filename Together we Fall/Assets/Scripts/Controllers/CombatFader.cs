using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class CombatFader : MonoBehaviour
{

    [SerializeField] private float fadeDuration;
    [SerializeField] private TextMeshProUGUI defeatText;
    [SerializeField] private CanvasGroup blackScreen;
    [SerializeField] private CanvasGroup defeatScreenContent;

    public void Start()
    {
        defeatScreenContent.alpha = 0;
        blackScreen.alpha = 1;

        Sequence fadeSequence = DOTween.Sequence();
        fadeSequence.AppendInterval(1f)
                    .Append(blackScreen.DOFade(0f, 1f))
                    .AppendCallback(() => blackScreen.gameObject.SetActive(false) );
    }
    
    public void TransitionToScene(SceneIndexes sceneIndex, int fadeDuration = 1) {
        blackScreen.gameObject.SetActive(true);

        Sequence fadeSequence = DOTween.Sequence();
        fadeSequence.AppendInterval(1f)
                    .Append(blackScreen.DOFade(1f, fadeDuration))
                    .AppendCallback(() => SceneManager.LoadScene((int)sceneIndex));
    }

    public void ShowDefeatScreen(string text, TweenCallback onFadeInEnd, float delayUntilShow = 1f)
    {
        defeatText.text = text;
        blackScreen.alpha = 0;
        defeatScreenContent.alpha = 1;
        blackScreen.interactable = false;

        Sequence fadeSequence = DOTween.Sequence();
        fadeSequence.AppendInterval(delayUntilShow)
                    .AppendCallback( () => { blackScreen.gameObject.SetActive(true); } )
                    .Append(blackScreen.DOFade(1f, fadeDuration))
                    .AppendCallback(onFadeInEnd)
                    .AppendCallback(() => {blackScreen.interactable = true; });
    }
}
