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

    [SerializeField]
    private LayerMask entryRegionLayer;
    [SerializeField]
    private Player player;

    private float fireRate;
    private float time;


    private List<TroopSelectionUI> soldierButtonInfos = new List<TroopSelectionUI>();

    void Start()
    {
        fireRate = player.fireRate;
        time = 1/fireRate;  // Initialize time so that player puts soldier instantly on first click

        foreach(Transform soldierInfo in ButtonPanelTransform) {
            TroopSelectionUI currentSoldierInfo;
            currentSoldierInfo.soldierButton = soldierInfo.GetComponentInChildren<Button>();
            currentSoldierInfo.soldierCount = soldierInfo.GetComponentInChildren<TextMeshProUGUI>();
            currentSoldierInfo.buttonBackgroundImage = currentSoldierInfo.soldierButton.GetComponent<Image>();
            currentSoldierInfo.soldierCard = currentSoldierInfo.soldierButton.GetComponent<TroopSelectButtonCard>().troopCard;
            
            soldierButtonInfos.Add(currentSoldierInfo);
        }

        UpdateCardCounts();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
            time = 1/fireRate;
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && cardHandler.selectedCard != null)
        {
            if (time >= 1/fireRate){
                time = 0;

                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                
                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, 2100f, entryRegionLayer);
                Debug.Log(hit);
                
                if (hit.collider != null && !(cardHandler.selectedCard == null || !cardHandler.HasTroops())){
                    player.PutSoldier(mousePos);
                    UpdateCardCounts();
                }
            }

            time += Time.deltaTime;            
        }
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
}


public struct TroopSelectionUI {
    public Button soldierButton;
    public Card soldierCard;
    public Image buttonBackgroundImage;
    public TextMeshProUGUI soldierCount;
}