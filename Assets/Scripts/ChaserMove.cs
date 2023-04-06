using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserMove : MonoBehaviour
{
    public GameObject laserWarning;
    public GameObject laserbeam;
    public GameObject chaser;
    private float speed = 4f;
    private float increment;
    private int oneShot;
    private LaserTracking laserTracking;
    //private float moveSpeed = 0f;
    // Start is called before the first frame update
    void Awake()
    {
        increment = 5f;
        oneShot = 1;
        laserTracking = laserbeam.GetComponent<LaserTracking>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        increment -= 1 * Time.deltaTime;
        if (increment <= 0) {
            speed += 1f;
            increment = 5f;
        }
        */
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") {
            if (oneShot == 1) {
            laserTracking.setFollow(true);
            Instantiate(laserWarning, new Vector2(chaser.transform.position.x, chaser.transform.position.y + chaser.transform.localScale.y * 2), Quaternion.identity);
            //laserTracking.setFollow(false);
            oneShot = 0;
            }
        }
}
}
