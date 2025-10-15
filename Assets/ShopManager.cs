using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject levelManager;
    public GameObject shopMenu;
    public Button coinQuantityButton;
    public int coinQuantityCost;
    private string coinQuantityText;
    public Button coinValueButton;
    public int coinValueCost;
    private string coinValueText;
    public Button timeIncreaseButton;
    public int timeIncreaseCost;
    private string timeIncreaseText;
    public Button difficultyPurchaseButton;
    public int difficultyPurchaseCost;
    private string difficultyPurchaseText;
    public Button areaPurchaseButton;
    public int areaPurchaseCost;
    private string areaPurchaseText;

    public void Start()
    {
        coinQuantityText = coinQuantityButton.GetComponentInChildren<TextMeshProUGUI>().text;
        coinValueText = coinValueButton.GetComponentInChildren<TextMeshProUGUI>().text;
        timeIncreaseText = timeIncreaseButton.GetComponentInChildren<TextMeshProUGUI>().text;
        difficultyPurchaseText = difficultyPurchaseButton.GetComponentInChildren<TextMeshProUGUI>().text;
        areaPurchaseText = areaPurchaseButton.GetComponentInChildren<TextMeshProUGUI>().text;
    }

    public void SetShopInteractables() 
    {
        int coins = levelManager.GetComponent<LevelManager>().moneyTotal;

        SetButtonInteractable(coinQuantityButton, coinQuantityCost, coinQuantityText, coins);
        SetButtonInteractable(coinValueButton, coinValueCost, coinValueText, coins);
        SetButtonInteractable(timeIncreaseButton, timeIncreaseCost, timeIncreaseText, coins);
        SetButtonInteractable(difficultyPurchaseButton, difficultyPurchaseCost,difficultyPurchaseText ,coins);
        SetButtonInteractable(areaPurchaseButton, areaPurchaseCost, areaPurchaseText, coins);
    }

    public void SetButtonInteractable(Button btn, int cost, string text, int money) 
    {
        if (cost <= money)
        {
            btn.interactable = true;
            Debug.Log(text + ": Active");
        }
        else 
        {
            btn.interactable = false;
            Debug.Log(text + ": Inactive");
        }

        string btnText = text + $"\n[${cost}]";

        btn.GetComponentInChildren<TextMeshProUGUI>().text = btnText;
    }

    public void PurchaseCoinQuantity() 
    {
        int coins = levelManager.GetComponent<LevelManager>().moneyTotal;

        if (coins <= coinQuantityCost) 
        {
            levelManager.GetComponent<LevelManager>().IncreasePickups();
            levelManager.GetComponent<LevelManager>().SubtractMoney(coinQuantityCost);
            coinQuantityCost *= 5;

            SetShopInteractables();
        }
    }

    public void PurchaseCoinValue()
    {
        int coins = levelManager.GetComponent<LevelManager>().moneyTotal;

        if (coins <= coinValueCost)
        {
            levelManager.GetComponent<LevelManager>().IncreasePickupValue();
            levelManager.GetComponent<LevelManager>().SubtractMoney(coinValueCost);
            coinValueCost *= 5;

            SetShopInteractables();
        }
    }

    public void PurchaseTimeLimit()
    {
        int coins = levelManager.GetComponent<LevelManager>().moneyTotal;

        if (coins <= timeIncreaseCost)
        {
            levelManager.GetComponent<LevelManager>().IncreaseTimeLimit();
            levelManager.GetComponent<LevelManager>().SubtractMoney(timeIncreaseCost);
            timeIncreaseCost *= 5;

            SetShopInteractables();
        }
    }

    public void PurchaseDifficulty()
    {
        int coins = levelManager.GetComponent<LevelManager>().moneyTotal;

        if (coins <= difficultyPurchaseCost)
        {
            levelManager.GetComponent<LevelManager>().IncreaseDifficulty();
            levelManager.GetComponent<LevelManager>().SubtractMoney(difficultyPurchaseCost);
            difficultyPurchaseCost *= 5;

            SetShopInteractables();
        }
    }
}
