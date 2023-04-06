using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedDebug : MonoBehaviour
{
    private TextMeshProUGUI score;
   
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
            score.text = "Speed: " + player.playerVL;
        }
    }
}
