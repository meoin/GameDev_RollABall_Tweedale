using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public InventoryManager invManager;
    public int slotIndex;

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            InventoryItem item = dropped.GetComponent<InventoryItem>();
            item.parentAfterDrag = transform;
            invManager.inventory[item.listIndex] = null;

            invManager.inventory[GetNewListIndex(slotIndex)] = item.details;
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

            itemInThisSlot.gameObject.GetComponent<InventoryItem>().listIndex = item.listIndex;
            invManager.inventory[item.listIndex] = itemInThisSlot.gameObject.GetComponent<InventoryItem>().details;

            invManager.inventory[GetNewListIndex(slotIndex)] = item.details;
        }
    }

    private int GetNewListIndex(int index) 
    {
        int newListIndex = index;

        if (slotIndex > 3)
        {
            newListIndex = index - 3;
        }

        return newListIndex;
    }

}
