using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public CardHandler cardHandler;
    private Vector3 worldPos;
    public GameObject soldiersParent;
    public float fireRate;

    // This function is called by CardUIController
    public void PutSoldier(Vector3 mousePos)
    {
        worldPos = mousePos;
        worldPos.z = 0;
        GameObject newSoldier = Instantiate(cardHandler.selectedCard.soldierPrefab, worldPos, Quaternion.identity, soldiersParent.transform);
        cardHandler.DecreaseCardCount();
    }

    [ContextMenu("Refresh Deck")]
    public void RefreshDeck()
    {
        cardHandler.RefreshPlayerDeck();
    }

}
