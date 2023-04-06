using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    public static float scoreValue = 0;
    private TextMeshProUGUI score;
    private float scoreScale = 1;
    public Player player;
    
    void Start()
    {
        score = GetComponent<TextMeshProUGUI>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.getAlive())
        {
            score.text = "Score: " + scoreCalc();
        }
    }

    public int scoreCalc() {
        scoreValue += Time.deltaTime * 25;
        if (scoreScale <= 10) {
            scoreScale += Time.deltaTime / 1000;
        }
        return (int)(scoreValue * scoreScale);
    }

    public void Setup() //add int score after in parameters
    {
        gameObject.SetActive(false);
    }
}
