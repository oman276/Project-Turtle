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

    public void PlayLevel15()
    {
        SceneManager.LoadScene("Level15");
    }

    public void PlayLevel16()
    {
        SceneManager.LoadScene("Level16");
    }

    public void PlayLevel17()
    {
        SceneManager.LoadScene("Level17");
    }

    public void PlayLevel18()
    {
        SceneManager.LoadScene("Level18");
    }

    public void PlayLevel19()
    {
        SceneManager.LoadScene("Level19");
    }

    public void PlayLevel20()
    {
        SceneManager.LoadScene("Level20");
    }

    public void PlayLevel21()
    {
        SceneManager.LoadScene("Level21");
    }

    public void PlayLevel22()
    {
        SceneManager.LoadScene("Level22");
    }

    public void PlayLevel23()
    {
        SceneManager.LoadScene("Level23");
    }

    public void PlayLevel24()
    {
        SceneManager.LoadScene("Level24");
    }

    public void PlayLevel25()
    {
        SceneManager.LoadScene("Level25");
    }

    public void PlayLevel26()
    {
        SceneManager.LoadScene("Level26");
    }

    public void PlayLevel27()
    {
        SceneManager.LoadScene("Level27");
    }


}

