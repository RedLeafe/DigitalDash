using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x - this.transform.position.x > 45)
        {
            moveFloor();
        }
    }
    void moveFloor()
    {
       
        this.transform.Translate(90,0,0);
    }
}



