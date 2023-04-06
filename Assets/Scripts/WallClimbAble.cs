using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallClimbAble : MonoBehaviour
{
    [Header("Player Check")]
    private BoxCollider2D boxCollider2d;
    [SerializeField] private LayerMask playerLayermask;
   
    
    // Start is called before the first frame update
    void Start()
    {
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerCheck();
    }

    public bool playerCheck() {
        float extendedHeight = 0.1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, 0, 0), new Vector3(extendedHeight, boxCollider2d.bounds.size.y,0), 0f, Vector2.left,  extendedHeight, playerLayermask);
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.red;
            
            
        }
        else
        {
            
            rayColor = Color.green;
        }
        //debug menu to draw hitbox
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x,boxCollider2d.bounds.extents.y,0) , Vector2.left * extendedHeight, rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, -boxCollider2d.bounds.extents.y, 0), Vector2.left * extendedHeight, rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center + new Vector3(-(boxCollider2d.bounds.extents.x + extendedHeight), boxCollider2d.bounds.extents.y, 0), Vector2.down * boxCollider2d.bounds.size.y, rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center + new Vector3(-boxCollider2d.bounds.extents.x, boxCollider2d.bounds.extents.y, 0), Vector2.down * boxCollider2d.bounds.size.y, rayColor);
        return raycastHit.collider != null;
        
    }
}
