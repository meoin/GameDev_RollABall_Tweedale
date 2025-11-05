using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public PetDetails[] inventory = new PetDetails[12];
    public GameObject inventoryGrid;
    public GameObject inventoryItemPrefab;

    public void PopulateGrid()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            int gridIndex = i;

            if (i >= 3) 
            {
                gridIndex = i + 3;
            }

            if (inventory[i] != null) 
            {
                GameObject pet = Instantiate(inventoryItemPrefab, inventoryGrid.transform.GetChild(gridIndex));

                pet.GetComponent<Image>().sprite = LoadSprite(inventory[i].Material);

                pet.GetComponent<InventoryItem>().details = inventory[i];
                pet.GetComponent<InventoryItem>().listIndex = i;
            }
        }
    }

    public void ClearGrid() 
    {
        foreach (Transform slot in inventoryGrid.transform)
        {
            if (slot.childCount > 0) 
            {
                GameObject child = slot.GetChild(0).gameObject;
                Destroy(child);
            }
        }

        GameObject itemDetails = GameObject.Find("ItemDetails");

        // Check if the object was found before attempting to destroy it
        if (itemDetails != null)
        {
            // Destroy the found GameObject
            Destroy(itemDetails);
        }
    }

    private Sprite LoadSprite(string color) 
    {
        Sprite loadedSprite = Resources.Load<Sprite>(color + "Sprite");

        if (loadedSprite != null)
        {
            Debug.Log($"Loaded sprite from Resources: {loadedSprite.name}");

            return loadedSprite;
        }
        else
        {
            Debug.LogError("Sprite not found in Resources.");
            return null;
        }
    }

    public void AddPetToInventory(PetDetails pet) 
    {
        bool emptySlotFound = false;

        for (int i = 0; i < inventory.Length; i++) 
        {
            if (!emptySlotFound) 
            {
                if (inventory[i] == null) 
                {
                    Debug.Log("Added pet to slot " + i);
                    inventory[i] = pet;
                    emptySlotFound = true;
                }
            }
        }

        if (!emptySlotFound) 
        {
            Debug.Log("No empty slots!");
        }
    }

}
