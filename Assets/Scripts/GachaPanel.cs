using NUnit.Framework.Internal;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GachaPanel : MonoBehaviour
{
    private int minPower;
    private int maxPower;
    private int minRarity;
    private int maxRarity;

    private PetDetails pet;

    public InventoryManager inventoryManager;
    public PetNameGenerator petNameGenerator;
    public PlayerControllerNew player;
    private Shop currentShop;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI strengthText;
    public Image spriteImage;

    public void RunGacha(int minRar, int maxRar, int minP, int maxP, GameObject shop) 
    {
        currentShop = shop.GetComponent<Shop>();
        minRarity = minRar;
        maxRarity = maxRar;
        minPower = minP;
        maxPower = maxP;

        PlayGachaPull();

        nameText.text = pet.Name.ToUpper();
        strengthText.text = $"STR: {pet.Strength}";
        spriteImage.sprite = Resources.Load<Sprite>(pet.Material + "Sprite");
    }

    public void TakePet() 
    {
        currentShop.ToggleBuyIcon(true);
        inventoryManager.AddPetToInventory(pet);
        player.SpawnPet();
        player.paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void DeclinePet() 
    {
        currentShop.ToggleBuyIcon(true);
        pet = null;
        player.paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void PlayGachaPull()
    {
        int rarity = GetRarity();
        string color = GetRandomColor(rarity);
        int power = GetRandomStrength(rarity);
        string name = petNameGenerator.GetName();

        pet = new PetDetails(name, power, rarity, color);
    }

    private int GetRarity()
    {
        int valueOne = Random.Range(minRarity, maxRarity+1);
        int valueTwo = Random.Range(minRarity, maxRarity+1);

        int randomValue = Mathf.Min(valueOne, valueTwo);

        return randomValue;
    }

    public string GetRandomColor(int randomValue)
    {
        switch (randomValue)
        {
            case 0:
                return "Grey";
            case 1:
                return "Tan";
            case 2:
                return "Yellow";
            case 3:
                return "Blue";
            case 4:
                return "Green";
            case 5:
                return "Orange";
            case 6:
                return "Cyan";
            case 7:
                return "Purple";
            case 8:
                return "Red";
            case 9:
                return "Pink";
            case 10:
                return "White";
            case 11:
                return "Black";
            default:
                return "Grey";

        }
    }

    private int GetRandomStrength(int boost)
    {
        int initialStrength = Random.Range(minPower, maxPower);

        int strength = Mathf.RoundToInt(initialStrength * (1 + boost * 0.5f));

        return strength;
    }
}
