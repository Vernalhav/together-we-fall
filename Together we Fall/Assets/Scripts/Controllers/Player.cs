using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public KeyCode escapeKey;

    public CardHandler cardHandler;
    private Vector3 worldPos;
    public GameObject soldiersParent;
    public float fireRate;
    [SerializeField] private LayerMask entryRegionLayer;
    private float time;

    private void Awake() {
        if (Application.isEditor){
            Debug.Log("Refreshing player deck because we are in editor mode!");
            // RefreshDeck();
        }
    }

    void Start()
    {
        time = 1/fireRate;  // Initialize time so that player puts soldier instantly on first click
    }

    void Update()
    {
        if (Input.GetKeyDown(escapeKey)){
            SceneManager.LoadScene((int)SceneIndexes.MainMenu);
        }

        if (Input.GetMouseButtonUp(0))
            time = 1/fireRate;
    }
    
    void FixedUpdate()
    {
        if ((Input.GetMouseButton(0) && cardHandler.selectedCard != null) && !GameManager.Instance.hasLost)
        {
            if (time >= 1/fireRate){
                time = 0;

                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                
                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, 2100f, entryRegionLayer);
                
                if (hit.collider != null && !(cardHandler.selectedCard == null || !cardHandler.HasTroops())){
                    PutSoldier(mousePos);
                }
            }

            time += Time.deltaTime;            
        }
    }

    public void PutSoldier(Vector3 mousePos)
    {
        worldPos = mousePos;
        worldPos.z = 0;
        GameObject newSoldier = Instantiate(cardHandler.selectedCard.soldierPrefab, worldPos, Quaternion.identity, soldiersParent.transform);
        
        CardHandler.OnCardDeploy();
    }

    [ContextMenu("Refresh Deck")]
    public void RefreshDeck()
    {
        cardHandler.RefreshPlayerDeck();
    }

}
