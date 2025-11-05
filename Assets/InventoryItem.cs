using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    public Image image;
    public PetDetails details;
    [HideInInspector] public InventoryManager inventoryManager;

    [HideInInspector] public Transform parentAfterDrag;

    private GameObject itemDetails;
    private TextMeshProUGUI detailsText;
    private bool isHovering = false;

    void Start() 
    {
        itemDetails = transform.GetChild(0).gameObject;
        detailsText = itemDetails.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        itemDetails.SetActive(false);
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        parentAfterDrag = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        itemDetails.SetActive(false);
        isHovering = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ReturnToParent();
    }

    public void ReturnToParent() 
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
        itemDetails.transform.SetParent(transform);
        itemDetails.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        itemDetails.transform.SetParent(transform.root);
        itemDetails.transform.SetAsLastSibling();
        itemDetails.SetActive(true);

        detailsText.text = $"Name\n\nSTR: 5";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        itemDetails.transform.SetParent(transform);
        itemDetails.SetActive(false);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (isHovering)
        {
            itemDetails.transform.position = Input.mousePosition + new Vector3(10, 10);
        }
    }
}
