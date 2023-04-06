using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    float player;

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindWithTag("Player").transform.position.x;
        //Debug.Log("Player: " + player.transform.position.x);
        //Debug.Log("Building: " + this.transform.position.x);
        if (player - this.transform.position.x > 50)  {
            Destroy(this.gameObject);
        }
    }
    
}

