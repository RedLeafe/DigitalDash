using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player1.transform.position.y > 0)
        { // bruh can't we just have the camera follow the player always, like no if statement and just the followPlayer() method
            followPlayer();
        }
        else {
            followPlayerX();
        }
    }
    void followPlayer() {
        transform.position = new Vector3(player1.transform.position.x, player1.transform.position.y, -10);
    }

    void followPlayerX() {
        transform.position = new Vector3(player1.transform.position.x, 0, -10);
    }
}
