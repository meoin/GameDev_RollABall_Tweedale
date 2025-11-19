
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryTrash : MonoBehaviour, IDropHandler
{
    public InventoryManager invManager;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        InventoryItem item = dropped.GetComponent<InventoryItem>();
        item.parentAfterDrag = transform;
        invManager.inventory[item.listIndex] = null;

        Destroy(dropped);
    }
}