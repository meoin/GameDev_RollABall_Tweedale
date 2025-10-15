using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Init,
    Menu,
    Paused,
    Shop,
    Roll
}

public class LevelManager : MonoBehaviour
{
    public int moneyTotal;
    public int roomPickups;
    public int pickupValue;
    public int roomEnemies;
    public float timeLimit;
    public GameObject UI;
    public GameObject shopManager;
    public GameObject pauseMenu;
    public GameObject mainMenu;
    public GameObject shopMenu;
    public GameObject winText;
    public GameObject countText;
    private bool paused = false;
    
    public GameState currentState = GameState.Menu;

    public LevelManager Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(shopManager);
        DontDestroyOnLoad(UI);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (currentState == GameState.Init)
        {
            SceneManager.LoadScene("MainMenu");
            mainMenu.SetActive(true);
            currentState = GameState.Menu;
        }
        if (currentState == GameState.Roll)
        {
            ActivateRandomObjectsWithTag("Pickup", roomPickups);
            ActivateRandomObjectsWithTag("Enemy", roomEnemies);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (currentState == GameState.Roll)
        {
            ActivateRandomObjectsWithTag("Pickup", roomPickups);
            ActivateRandomObjectsWithTag("Enemy", roomEnemies);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                PauseScene();
            }
            else 
            {
                ResumeScene();
            }
        }
    }

    public void SetCoinText() 
    {
        countText.gameObject.SetActive(true);
        countText.GetComponent<TextMeshProUGUI>().text = $"${moneyTotal}";
    }

    public void SubtractMoney(int cost) 
    {
        moneyTotal -= cost;
        SetCoinText();
    }

    public void IncreasePickups() 
    {
        Debug.Log($"Increasing pickups: {roomPickups} -> {roomPickups + 1}...");
        roomPickups++;
    }

    public void IncreaseDifficulty() 
    {
        Debug.Log($"Increasing enemies: {roomEnemies} -> {roomEnemies + 1}...");
        roomEnemies++;
    }

    public void IncreasePickupValue() 
    {
        Debug.Log($"Increasing pickup value: {pickupValue} -> {pickupValue * 2}...");
        pickupValue = pickupValue * 2;
    }

    public void IncreaseTimeLimit() 
    {
        Debug.Log($"Increasing time limit: {timeLimit} -> {timeLimit + 5}...");
        timeLimit += 5;
    }

    public void PickupCoin() 
    {
        moneyTotal += pickupValue;
    }

    public void PauseScene() 
    {
        Time.timeScale = 0;
        pauseMenu.gameObject.SetActive(true);
        paused = true;
    }

    public void ResumeScene() 
    {
        Time.timeScale = 1;
        pauseMenu.gameObject.SetActive(false);
        paused = false;
    }

    public void RestartScene() 
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Minigame");
        pauseMenu.SetActive(false);
        paused = false;
    }

    public void QuitGame() 
    {
        Application.Quit();
    }

    public void LoadMinigame() 
    {
        mainMenu.SetActive(false);
        shopMenu.SetActive(false);
        Debug.Log("Loading Minigame...");

        currentState = GameState.Roll;
        SceneManager.LoadScene("Minigame");
    }

    public void LoadShop() 
    {
        Debug.Log("Loading Shop...");
        currentState = GameState.Shop;
        SceneManager.LoadScene("Shop");
        shopMenu.SetActive(true);
        shopManager.GetComponent<ShopManager>().SetShopInteractables();
    }

    public void ReturnToMenu() 
    {
        pauseMenu.gameObject.SetActive(false);
        paused = false;

        currentState = GameState.Menu;
        SceneManager.LoadScene("MainMenu");
        mainMenu.SetActive(true);

        Time.timeScale = 1;
    }

    private void ActivateRandomObjectsWithTag(string tag, int count)
    {
        GameObject[] objectArray = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject obj in objectArray)
        {
            obj.SetActive(false);
        }

        List<int> uniqueObjectIndexes = new List<int>();

        for (int i = 0; i < count; i++)
        {
            bool loopForUnique = true;

            while (loopForUnique)
            {
                int randomIndex = Random.Range(0, objectArray.Length - 1);

                if (!uniqueObjectIndexes.Contains(randomIndex))
                {
                    loopForUnique = false;

                    uniqueObjectIndexes.Add(randomIndex);
                }
            }
        }

        foreach (int i in uniqueObjectIndexes)
        {
            objectArray[i].SetActive(true);
        }
    }


}
