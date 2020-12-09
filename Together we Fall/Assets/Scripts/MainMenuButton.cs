using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;


public class MainMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private RectTransform myTransform;
    [SerializeField] private TextMeshProUGUI myText;
    [SerializeField] private Color defaultColour;
    [SerializeField] private Color hoverColour;
    private float myHeight;
    private float myWidth;
    private DOTween hoverAnim;

    public void Start()
    {
        myHeight = myTransform.rect.height;
        myWidth = myTransform.rect.width;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        myText.color = hoverColour;

        myTransform.DOSizeDelta( new Vector2(myWidth + 20, myHeight), 0.5f );
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        myText.color = defaultColour;
        myTransform.DOSizeDelta( new Vector2(myWidth, myHeight), 0.5f );
    }
 
}
