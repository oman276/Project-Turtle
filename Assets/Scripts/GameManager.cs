using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI movesStr;
    public TextMeshProUGUI defeatedStr;
    public TextMeshProUGUI coinsStr;

    public GameObject endScreen;
    public GameObject pauseScreen;

    public int numOfEnemies;
    int enemiesDefeated = 0;

    public bool paused = false;

    int moves = 0;
    int coins = 0;

    GameObject[] standardMenu;

    // Start is called before the first frame update
    void Start()
    {
        standardMenu = new GameObject[5];
        standardMenu[0] = GameObject.Find("Moves Host");
        standardMenu[1] = GameObject.Find("Enemy Host");
        standardMenu[2] = GameObject.Find("Pause Button");
        standardMenu[3] = GameObject.Find("Map Button");
        standardMenu[4] = GameObject.Find("Coins Host");

        if (!coinsStr) {
            coinsStr = GameObject.Find("Coins Text").GetComponent<TextMeshProUGUI>();
        }

        movesStr.text = "0";
        defeatedStr.text = "0/" + numOfEnemies;
        coinsStr.text = string.Format("{0:D2}", coins);
    }

    public void EnemyDefeated() {
        ++enemiesDefeated;
        defeatedStr.text = enemiesDefeated + "/" + numOfEnemies;

        if (enemiesDefeated == numOfEnemies) {
            EndGame();
        }
    }

    public void EndGame() {
        for (int i = 0; i < standardMenu.Length; ++i) {
            standardMenu[i].SetActive(false);
        }

        FindObjectOfType<AudioManager>().Play("victory_chime");
        endScreen.SetActive(true);
        Time.timeScale = 0;
        paused = true;
    }

    public void Moved() {
        ++moves;
        if (moves > 999)
        {
            movesStr.text = "Lots!";
        }
        else {
            movesStr.text = moves.ToString();
        }
        
    }

    public void BackToMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void Pause() {
        for (int i = 0; i < standardMenu.Length; ++i)
        {
            standardMenu[i].SetActive(false);
        }
        paused = true;
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume() {
        for (int i = 0; i < standardMenu.Length; ++i)
        {
            standardMenu[i].SetActive(true);
        }
        Time.timeScale = 1;
        paused = false;
        pauseScreen.SetActive(false);
    }

    public void CoinGet() {
        ++coins;
        coinsStr.text = string.Format("{0:D2}", coins);
    }
}
