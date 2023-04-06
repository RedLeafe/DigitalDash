using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPathWarning : MonoBehaviour
{
    public GameObject player;
    public GameObject laser;
    public GameObject chaserLight;
    float lengthTimer;
    float heightTimer;
    LaserTracking laserTracking;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.localScale = new Vector3(0.5f, 0.05f, 1);
        player = GameObject.FindGameObjectWithTag("Player");
        chaserLight = GameObject.FindGameObjectWithTag("Chaser");
        laserTracking = laser.GetComponent<LaserTracking>();
        lengthTimer = 0.2f;
        heightTimer = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        followPlayer();
        if (this.transform.localScale.x >= 42 && this.transform.localScale.y >= 0.3) {
            shootLaser();
            this.transform.localScale = new Vector3(0.5f, 0.05f, 1);
            laserTracking.setFollow(false);
            Destroy(this.gameObject);
        }
        lengthTimer -= 1 * Time.deltaTime;
        if (lengthTimer <= 0 && this.transform.localScale.x < 42) {
            this.transform.localScale = new Vector3(this.transform.localScale.x + 1f, this.transform.localScale.y, 1);
            lengthTimer = 0.005f;
        } else if (this.transform.localScale.x >= 42) {
            heightTimer -= 1 * Time.deltaTime;
            if (heightTimer <= 0 && this.transform.localScale.y < 0.3) {
                this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y + 0.05f, 1);
                heightTimer = 0.050f;
            }  
        }
    }

    void followPlayer() {
        /*
        if (this.transform.localScale.x < 16) {
        Vector3 start = new Vector3(this.transform.position.x, this.transform.position.y, 1);
        Vector3 end = new Vector3(this.transform.position.x, player.transform.position.y, 1);
        this.transform.position = Vector3.MoveTowards(start, end , 5f);
        }
        */
        //Debug.Log(laserTracking.getFollow());
        if (laserTracking.getFollow() == false && this.transform.localScale.x < 42) {
            this.transform.position = new Vector3(player.transform.position.x + 7.5f, player.transform.position.y + 0.2f, 1);
        } else {
            aimTowardsPlayer();
            float xPos = chaserLight.transform.position.x - chaserLight.transform.localScale.x / 2;
            float yPos = chaserLight.transform.position.y + chaserLight.transform.localScale.y * 2;
            this.transform.position = new Vector3(xPos, yPos, 1);
        }
    }

    void shootLaser() {
        if (laserTracking.getFollow() == false) {
            Instantiate(laser, new Vector3(player.transform.position.x + 7.5f, this.transform.position.y, 1), Quaternion.identity);
        } else {
            Instantiate(laser, this.transform.position, Quaternion.identity);
        }
    }

    void aimTowardsPlayer() {
        Vector3 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
    }
}
