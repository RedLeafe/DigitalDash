using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public Score Score;
    public TextMeshProUGUI score;

    public void Setup()
    {
        gameObject.SetActive(true);
        int s = (int) Score.scoreValue;
        score.text = "Score: " + s;
    }

    public void Restart()
    {
        Score.scoreValue = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void mainMenu()
    {
        Score.scoreValue = 0;
        SceneManager.LoadScene("MainMenu");
    }
}
