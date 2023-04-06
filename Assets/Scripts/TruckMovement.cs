using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckMovement : MonoBehaviour
{
    float speed = 7f;
    public bool left;
    public bool right;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(right);
        Debug.Log(false);

        if (left) {
            transform.Translate(Vector2.left * speed  * Time.deltaTime);
        } else if (right) {
            transform.Translate(Vector2.right * speed  * Time.deltaTime);
        }
    }

    public void setLeft(bool x) {
        left = x;
    }

    public void setRight(bool x) {
        right = x;
    }
}
