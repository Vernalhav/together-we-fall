using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResetButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private CanvasGroup resetButtonGroup;

    void Start()
    {
        resetButtonGroup = GetComponent<CanvasGroup>();
        resetButtonGroup.alpha = 0.5f;
    }

    public void OnPointerEnter(PointerEventData e)
    {
        resetButtonGroup.alpha = 1f;
    }

    public void OnPointerExit(PointerEventData e)
    {
        resetButtonGroup.alpha = 0.5f;
    }
}
