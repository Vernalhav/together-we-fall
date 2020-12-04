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
    public LayerMask entryRegionLayer;
    public float fireRate;
    private float time;

    void Start()
    {   
        time = 1/fireRate; // Initialize time so that player puts soldier instantly on first click
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0)) time = 1/fireRate;
    }

    private void PutSoldier()
    {
        if (!cardHandler.HasTroops()) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, 2100f, entryRegionLayer);
        //Debug.Log(mousePos + " " + (hit.collider == null? "É nulo" : "é o" + hit.collider.gameObject.name));
        
        if (hit.collider != null )
        {
            worldPos = mousePos;
            worldPos.z = 0;
            GameObject newSoldier = Instantiate(cardHandler.selectedCard.soldierPrefab, worldPos, Quaternion.identity, soldiersParent.transform);
            //Debug.Log(newSoldier.GetComponentInChildren<Soldier>());
            cardHandler.DecreaseCardCount();
        }
    }

    void FixedUpdate()
    {

        if (Input.GetMouseButton(0) && cardHandler.selectedCard != null)
        {
            if (time >= 1/fireRate){
                time = 0;
                PutSoldier();
            }

            time += Time.deltaTime;            
        }
    }
}
