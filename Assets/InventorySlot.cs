using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            InventoryItem item = dropped.GetComponent<InventoryItem>();
            item.parentAfterDrag = transform;
        }
        else 
        {
            GameObject dropped = eventData.pointerDrag;
            InventoryItem item = dropped.GetComponent<InventoryItem>();
            Transform otherSlot = item.parentAfterDrag;
            Transform itemInThisSlot = transform.GetChild(0);

            itemInThisSlot.parent = otherSlot;
            itemInThisSlot.gameObject.GetComponent<InventoryItem>().parentAfterDrag = otherSlot;
            item.parentAfterDrag = transform;
            
        }

        
    }

}
