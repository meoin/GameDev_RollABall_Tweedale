using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public bool tutorialOneCleared = false;
    public bool tutorialTwoCleared = false;
    public bool tutorialThreeCleared = false;
    public bool tutorialFourCleared = false;

    public GameObject tutorialOne;
    public GameObject tutorialTwo;
    public GameObject tutorialThree;
    public GameObject tutorialFour;

    public void ClearTutorialOne() 
    {
        tutorialOne.SetActive(false);
        tutorialOneCleared = true;
    }

    public void ClearTutorialTwo()
    {
        tutorialTwo.SetActive(false);
        if (tutorialOneCleared)
        {
            tutorialTwoCleared = true;
        }
    }

    public void ClearTutorialThree()
    {
        tutorialThree.SetActive(false);
        if (tutorialTwoCleared)
        {
            tutorialThreeCleared = true;
        }
    }

    public void ClearTutorialFour()
    {
        tutorialFour.SetActive(false);
        if (tutorialThreeCleared)
        {
            tutorialFourCleared = true;
        }
    }

    public void ShowTutorialTwo() 
    {
        if (!tutorialTwoCleared && tutorialOneCleared) 
        {
            tutorialTwo.SetActive(true);
        }
    }

    public void ShowTutorialThree() 
    {
        if (!tutorialThreeCleared && tutorialTwoCleared)
        {
            tutorialThree.SetActive(true);
        }
    }

    public void ShowTutorialFour() 
    {
        if (!tutorialFourCleared && tutorialThreeCleared)
        {
            tutorialFour.SetActive(true);
        }
    }
}
