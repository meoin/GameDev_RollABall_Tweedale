using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public int price;
    public int minRarity;
    public int maxRarity;
    public int minPower;
    public int maxPower;

    public TextMeshPro priceText;
    public GameObject buyIcon;
    public Sprite sprite;

    private bool canPurchase = false;
    public PlayerControllerNew playerController;

    public GachaPanel gachaPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        priceText.text = $"${price}";
        buyIcon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);

        if (canPurchase && playerController.Cash() >= price && Input.GetKeyDown(KeyCode.E)) 
        {
            playerController.SpendMoney(price);

            ToggleBuyIcon(false);
            gachaPanel.gameObject.SetActive(true);
            gachaPanel.RunGacha(minRarity, maxRarity, minPower, maxPower, gameObject);

            playerController.paused = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void ToggleBuyIcon(bool toggle) 
    {
        canPurchase = toggle;
        buyIcon.SetActive(toggle);
    }
}
