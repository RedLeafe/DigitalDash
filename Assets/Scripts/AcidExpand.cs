using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidExpand : MonoBehaviour
{
    private float expandTimer;
    // Start is called before the first frame update
    void Start()
    {
        expandTimer = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        expandTimer -= 1 * Time.deltaTime;
        if (expandTimer <= 0) {
        this.transform.localScale = new Vector3(this.transform.localScale.x + 0.25f, this.transform.localScale.y, 10);
        expandTimer = 4f;
        }
    }
}
