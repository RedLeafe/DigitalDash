using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Platform : MonoBehaviour
{
    BoxCollider2D Collider;
    public GameObject player1;
    float player;

    // Start is called before the first frame update
    void Start()
    {
        Collider = GetComponent<BoxCollider2D>();
        Collider.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        //check();
    }

    public void check()
    {
        player = GameObject.FindWithTag("Player").transform.position.y;
        if (player >= this.transform.position.y + 0.5)
        {
            Collider.enabled = true;
        }
        else
        {
            Collider.enabled = false;
        }
    }
}

