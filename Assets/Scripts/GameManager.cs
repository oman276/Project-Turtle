using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI movesStr;
    public TextMeshProUGUI defeatedStr;

    public GameObject endScreen;

    public int numOfEnemies;
    int enemiesDefeated = 0;

    public bool paused = false;

    int moves = 0;

    // Start is called before the first frame update
    void Start()
    {
        movesStr.text = "Moves: 0";
        defeatedStr.text = "0/" + numOfEnemies;
    }

    public void EnemyDefeated() {
        ++enemiesDefeated;
        defeatedStr.text = enemiesDefeated + "/" + numOfEnemies;

        if (enemiesDefeated == numOfEnemies) {
            EndGame();
        }
    }

    public void EndGame() {
        endScreen.SetActive(true);
        Time.timeScale = 0;
        paused = true;
    }

    public void Moved() {
        ++moves;
        movesStr.text = "Moves: " + moves;
    }

    public void BackToMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
