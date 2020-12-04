using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CardUIController : MonoBehaviour
{

    [SerializeField]
    private CardHandler cardHandler;
    [SerializeField]
    private RectTransform ButtonPanelTransform;

    [SerializeField]
    private Sprite selectedSprite;
    [SerializeField]
    private Sprite unselectedSprite;

    private List<TroopSelectionUI> soldierButtonInfos = new List<TroopSelectionUI>();

    void Start()
    {
        foreach(Transform soldierInfo in ButtonPanelTransform) {
            TroopSelectionUI currentSoldierInfo;
            currentSoldierInfo.soldierButton = soldierInfo.GetComponentInChildren<Button>();
            currentSoldierInfo.soldierCount = soldierInfo.GetComponentInChildren<TextMeshProUGUI>();
            currentSoldierInfo.buttonBackgroundImage = currentSoldierInfo.soldierButton.GetComponent<Image>();
            
            soldierButtonInfos.Add(currentSoldierInfo);
        }
    }

    public void HandleCardButtonClick(Card c)
    {
        Button clicked = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        foreach (TroopSelectionUI t in soldierButtonInfos){
            if (GameObject.ReferenceEquals(clicked, t.soldierButton)) {
                t.buttonBackgroundImage.sprite = selectedSprite;
                t.soldierButton.interactable = false;
            } else {
                t.buttonBackgroundImage.sprite = unselectedSprite;
                t.soldierButton.interactable = true;
            }
        }

        cardHandler.SelectCard(c);
    }
}


public struct TroopSelectionUI {
    public Button soldierButton;
    public Image buttonBackgroundImage;
    public TextMeshProUGUI soldierCount;
}