using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public int roomPickups;
    public int roomEnemies;
    public GameObject pauseMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ActivateRandomObjectsWithTag("Pickup", roomPickups);
        ActivateRandomObjectsWithTag("Enemy", roomEnemies);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            pauseMenu.gameObject.SetActive(!pauseMenu.activeSelf);
        }
    }

    public void RestartScene() 
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame() 
    {
        Application.Quit();
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
