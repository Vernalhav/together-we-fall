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
    private RectTransform buttonPanelTransform;
    [SerializeField]
    private CanvasGroup UICanvasGroup;

    [SerializeField]
    private Sprite selectedSprite;
    [SerializeField] private Sprite unselectedSprite;

    private List<TroopSelectionUI> soldierButtonInfos = new List<TroopSelectionUI>();

    void Start()
    {
        CardHandler.OnCardDeploy += UpdateCardCounts;
        
        foreach(Transform soldierInfo in buttonPanelTransform) {
            TroopSelectionUI currentSoldierInfo;
            currentSoldierInfo.soldierButton = soldierInfo.GetComponentInChildren<Button>();
            currentSoldierInfo.soldierCount = soldierInfo.GetComponentInChildren<TextMeshProUGUI>();
            currentSoldierInfo.buttonBackgroundImage = currentSoldierInfo.soldierButton.GetComponent<Image>();
            currentSoldierInfo.soldierCard = currentSoldierInfo.soldierButton.GetComponent<TroopSelectButtonCard>().troopCard;
            
            soldierButtonInfos.Add(currentSoldierInfo);
        }

        UpdateCardCounts();
    }

    void OnDestroy()
    {
        CardHandler.OnCardDeploy -= UpdateCardCounts;
    }

    void UpdateCardCounts()
    {
        foreach (TroopSelectionUI t in soldierButtonInfos){
            t.soldierCount.text = cardHandler.GetTroopCount(t.soldierCard).ToString();
        }
    }

    public void HandleCardButtonClick()
    {
        Card c = EventSystem.current.currentSelectedGameObject.GetComponent<Button>().GetComponent<TroopSelectButtonCard>().troopCard;

        foreach (TroopSelectionUI t in soldierButtonInfos){
            if (GameObject.ReferenceEquals(c, t.soldierCard)) {
                t.buttonBackgroundImage.sprite = selectedSprite;
                t.soldierButton.interactable = false;
            } else {
                t.buttonBackgroundImage.sprite = unselectedSprite;
                t.soldierButton.interactable = true;
            }
        }

        cardHandler.SelectCard(c);
    }

    public void BlockUserInput()
    {
        cardHandler.SelectCard(null);
        foreach (TroopSelectionUI t in soldierButtonInfos) {
            t.soldierButton.interactable = false;
            t.buttonBackgroundImage.sprite = unselectedSprite;
        }
        UICanvasGroup.alpha = 0.5f;
    }

    public void AllowUserInput()
    {
        foreach (TroopSelectionUI t in soldierButtonInfos) {
            t.soldierButton.interactable = true;
            t.buttonBackgroundImage.sprite = unselectedSprite;
        }
        UICanvasGroup.alpha = 1f;
    }
}


public struct TroopSelectionUI {
    public Button soldierButton;
    public Card soldierCard;
    public Image buttonBackgroundImage;
    public TextMeshProUGUI soldierCount;
}