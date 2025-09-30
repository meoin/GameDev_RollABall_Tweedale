using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public int roomPickups;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetPickups();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetPickups()
    {
        GameObject[] pickupArray = GameObject.FindGameObjectsWithTag("Pickup");

        foreach (GameObject pickup in pickupArray)
        {
            pickup.SetActive(false);
        }

        List<int> uniqueObjectIndexes = new List<int>();

        for (int i = 0; i < roomPickups; i++)
        {
            bool loopForUnique = true;

            while (loopForUnique)
            {
                int randomIndex = Random.Range(0, pickupArray.Length - 1);

                if (!uniqueObjectIndexes.Contains(randomIndex))
                {
                    loopForUnique = false;

                    uniqueObjectIndexes.Add(randomIndex);
                }
            }
        }

        foreach (int i in uniqueObjectIndexes)
        {
            pickupArray[i].SetActive(true);
        }
    }
}
