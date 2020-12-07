using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DefeatUIController : MonoBehaviour
{

    [SerializeField] private float fadeDuration;
    [SerializeField] private TextMeshProUGUI defeatText;
    [SerializeField] private CanvasGroup defeatScreen;

    public void Start()
    {
        defeatScreen.gameObject.SetActive(false);
        defeatScreen.alpha = 0;
    }

    public void ShowDefeatScreen(string text, float delayUntilShow = 1f)
    {
        defeatText.text = text;
        defeatScreen.alpha = 0;

        Sequence fadeSequence = DOTween.Sequence();
        fadeSequence.AppendInterval(delayUntilShow)
                    .AppendCallback( () => { defeatScreen.gameObject.SetActive(true); } )
                    .Append(defeatScreen.DOFade(1f, fadeDuration));
    }
}
