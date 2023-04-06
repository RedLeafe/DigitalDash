using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("movement")]
    public Rigidbody2D rb;
    public float jumpAmount = 10;
    public float moveSpeed = 10f;
    public float groundDrag = 0f;
    public float maxSpeed = 8f;
    public Vector2 playerVL;
    public float SlideTimer;
    public float JumpTimer;

    [Header("Animation")]
    public Animator animator;

    [Header("Swipe Detection")]
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    [Header("Game Over detection")]
    private bool Alive = true;
    public GameOver GameOver;
    public Score Score;

    [Header("Collision detection")]
    [SerializeField] private LayerMask platformLayerMask;
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;

    [Header("WallClimbing")]
    public float climbSpeed = 5;
    public float climbDuration = 1000;
    private GameObject recentWall;
    //private GameObject recentPlatform;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        rb.drag = groundDrag;
    }

    // Update is called once per frame
    void Update()
    {
        //Wallcheck();
        playerVL = rb.velocity;
        isGrounded();
        if (Alive )
        {
            //sets the drag to limit player speed.
            
            moveRight();
            speedControl();
            SlideTimer += Time.deltaTime;
            JumpTimer += Time.deltaTime;

            swipeDectection();
            
            if(recentWall != null) { 
                
                if (recentWall.transform.GetComponent<WallClimbAble>().playerCheck() && !isGrounded())
                {
                    animator.SetBool("Climbing", true);
                    moveUp();
                }
                else if(isGrounded())
                {
                    animator.SetBool("Climbing", false);
                }

                
            }

            if (isGrounded() == true && JumpTimer >= 0.1)
            {
                animator.SetBool("Jumped", false);
            }

            if (animator.GetBool("Slided") == true && SlideTimer >= 1)
            {
                animator.SetBool("Slided", false);
            }
            

        }
        

    }
    

    public void swipeDectection() {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;
            
            swipeCalc();
            
        }
    }

    public bool isGrounded() {
        float extendedHeight = 0.1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, extendedHeight, platformLayerMask);
        Color rayColor;
        if (raycastHit.collider != null)
        {
            //recentPlatform = raycastHit.collider.gameObject;
            rayColor = Color.red;
        }
        else {
           
            rayColor = Color.green;
        }
        Debug.DrawRay(boxCollider2d.bounds.center + new Vector3(boxCollider2d.bounds.extents.x,0), Vector2.down * (boxCollider2d.bounds.extents.y + extendedHeight), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + extendedHeight), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, boxCollider2d.bounds.extents.y + extendedHeight), Vector2.right * 2 *(boxCollider2d.bounds.extents.x), rayColor);

        return raycastHit.collider != null;

    }

    public void swipeCalc() {
        if (vertDectection()) {
            if (endTouchPosition.y > startTouchPosition.y && isGrounded()) {
                Jump();
            }
            else if (endTouchPosition.y < startTouchPosition.y && isGrounded())
            {
                SlideTimer = 0;
                Slide();
            }

        }

    }
    public bool vertDectection()
    {
        if (endTouchPosition.y - startTouchPosition.y == 0) {
            return false;
        }
        if (Math.Abs(endTouchPosition.y - startTouchPosition.y) > Math.Abs(endTouchPosition.x - startTouchPosition.x))
        {

            return true;

        }
        else
        {
            return false;

        }
    }

    //raycast to check if it hits wall pls make raycast box collider for now and draw debug for it.
    public void Wallcheck() {
        float extendedHeight = 0.1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, new Vector2(boxCollider2d.bounds.size.x, boxCollider2d.bounds.size.y - extendedHeight * 2), 0f, Vector2.right,extendedHeight, platformLayerMask);
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.red;
            moveUp();
            
        }
        else
        {
            //animator.SetBool("Climbing", false);
            rayColor = Color.green;
        }
        Debug.DrawRay(boxCollider2d.bounds.center + new Vector3(0 , boxCollider2d.bounds.extents.y - extendedHeight, 0), Vector2.right * (boxCollider2d.bounds.extents.x + extendedHeight), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(0,boxCollider2d.bounds.extents.y - extendedHeight, 0), Vector2.right * (boxCollider2d.bounds.extents.x + extendedHeight), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center + new Vector3(boxCollider2d.bounds.extents.x+ extendedHeight, boxCollider2d.bounds.extents.y - extendedHeight ), Vector2.down * 2 * (boxCollider2d.bounds.extents.x - extendedHeight/2), rayColor);

        //return raycastHit.collider != null;
    }
        private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            
            Alive = false;
            Score.Setup();
            GameOver.Setup();            
        }

       
        if (other.gameObject.tag == "WallClimb")
        {
            if (other.gameObject.transform.GetComponent<WallClimbAble>() != null)
            {
               
                    recentWall = other.gameObject;
            }
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);

        }
        
        
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {

            Alive = false;
            Score.Setup();
            GameOver.Setup();
        }

        
    }

    void Slide()
    {
        animator.SetBool("Slided", true);
    }

    public void moveRight() {
        //transform.Translate(Time.deltaTime * moveSpeed, 0, 0);
        //rb.velocity = new Vector3(moveSpeed, rb.velocity.y, 0f);
        rb.AddForce(Vector2.right * moveSpeed * Time.deltaTime * 30, ForceMode2D.Force);
    }

    public void Jump()
    {
        JumpTimer = 0;
        animator.SetBool("Jumped", true);
        //reset y velocity just in case still maintians x velocity.
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
    }

    public void moveUp()
    {
        animator.SetBool("Climbing", true);
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * climbSpeed, ForceMode2D.Impulse);        

    }
    public void moveRightSlow()
    {
        rb.AddForce(new Vector2(0.5f, 0f), ForceMode2D.Force);

    }

    public void speedControl() {
        if (rb.velocity.x > maxSpeed) {
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        }
    }

    public bool getAlive() {
        return Alive;
    }

    public void Inputs() { }

}

    
