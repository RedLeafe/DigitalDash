using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTracking : MonoBehaviour
{
    public GameObject target;
    public float speed;
    public float speed2 = 1.5f;
    public float t;
    public bool follow = false;
  //  public float speed;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (follow) {
        aimTowards();
        Vector3 start = transform.position;
        Vector3 end = target.transform.position;
        transform.position = Vector3.MoveTowards(start, end, speed);
        } else {
            this.transform.Translate(Vector2.left * speed2);
        }
    }

    public void setFollow(bool status) {
        follow = status;
    }

    public bool getFollow() {
        return follow;
    }

    private void aimTowards() {
        Vector3 direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
    }
}
