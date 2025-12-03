using TMPro;
using UnityEngine;

public class PortalInteraction : MonoBehaviour
{
    public double price;

    public TextMeshPro priceText;
    public GameObject buyIcon;
    private bool canPurchase = false;

    public PlayerControllerNew playerController;
    public GameObject winCanvas;

    public void Start()
    {
        priceText.text = $"{CostText(price)}";
    }

    public void Update()
    {
        if (canPurchase && playerController.Cash() >= price && Input.GetKeyDown(KeyCode.E))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            winCanvas.SetActive(true);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ToggleBuyIcon(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ToggleBuyIcon(false);
        }
    }

    public void ToggleBuyIcon(bool toggle)
    {
        canPurchase = toggle;
        buyIcon.SetActive(toggle);
    }

    public string CostText(double p)
    {
        if (p >= 1000000000000.0)
        {
            double truncatedCoins = price / 1000000000000;
            return "$" + truncatedCoins.ToString("F0") + "t";
        }
        else if (p >= 1000000000.0)
        {
            double truncatedCoins = price / 1000000000;
            return "$" + truncatedCoins.ToString("F0") + "b";
        }
        else if (p >= 1000000.0)
        {
            double truncatedCoins = price / 1000000;
            return "$" + truncatedCoins.ToString("F0") + "m";
        }
        else if (p >= 1000)
        {
            double truncatedCoins = price / 1000;
            return "$" + truncatedCoins.ToString("F0") + "k";
        }
        else
        {
            return "$" + p;
        }
    }
}
