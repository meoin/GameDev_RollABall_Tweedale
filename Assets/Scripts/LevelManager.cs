using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public enum GameState
{
    Menu,
    Paused,
    Shop,
    Roll
}

public class LevelManager : MonoBehaviour
{
    public int roomPickups;
    public int roomEnemies;
    public GameObject UI;
    public GameObject pauseMenu;
    public GameObject mainMenu;
    public GameObject winText;
    public GameObject countText;
    private bool paused = false;
    
    public GameState currentState = GameState.Menu;

    public LevelManager Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(UI);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

        currentState = GameState.Roll;
        SceneManager.LoadScene("Minigame");
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
