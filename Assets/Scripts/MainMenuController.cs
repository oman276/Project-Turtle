using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject levelScreen;

    public void SampleLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void LevelSelect()
    {
        mainMenu.SetActive(false);
        levelScreen.SetActive(true);
    }

    public void BackToMain() {
        levelScreen.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void PlayLevel1() {
        SceneManager.LoadScene("Level1");
    }

    public void PlayLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void PlayLevel3()
    {
        SceneManager.LoadScene("Level3");
    }

    public void PlayLevel4()
    {
        SceneManager.LoadScene("Level4");
    }

    public void PlayLevel5()
    {
        SceneManager.LoadScene("Level5");
    }

    public void PlayLevel6()
    {
        SceneManager.LoadScene("Level6");
    }

    public void PlayLevel7()
    {
        SceneManager.LoadScene("Level7");
    }

    public void PlayLevel8()
    {
        SceneManager.LoadScene("Level8");
    }

    public void PlayLevel9()
    {
        SceneManager.LoadScene("Level9");
    }

    public void PlayLevel10()
    {
        SceneManager.LoadScene("Level10");
    }

    public void PlayLevel11()
    {
        SceneManager.LoadScene("Level11");
    }

    public void PlayLevel12()
    {
        SceneManager.LoadScene("Level12");
    }

    public void PlayLevel13()
    {
        SceneManager.LoadScene("Level13");
    }

    public void PlayLevel14()
    {
        SceneManager.LoadScene("Level14");
    }
}

