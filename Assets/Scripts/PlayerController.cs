using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Drawing;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Movement parameters")]
    [Range(0.01f, 20.0f)][SerializeField] private float moveSpeed = 0.1f; // moving speed of the player
    [Range(0.01f, 20.0f)][SerializeField][Space(10)]private float jumpForce = 6.0f; // jumping force of the player
    private Rigidbody2D rigidBody;
    public LayerMask groundLayer;
    public const float rayLength = 1.3f;
    private Animator animator;
    private bool isWalking=false;
    private bool isFacingRight=true;
    private int score=0;
    private bool isLadder = false;
    private bool isClimbing=false;
    private float vertical=0.0f;
    void Start()
    {
        
    }

   

    // Update is called once per frame
    void Update()
    {
        isWalking = false;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
            isWalking = true;
            if (!isFacingRight)
            {
                Flip();
            }
        } else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(-1 * moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
            isWalking = true;
            if (isFacingRight)
            {
                Flip();
            }
        }
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        Debug.DrawRay(transform.position, rayLength * Vector3.down, Color.yellow, 1, false);
        animator.SetBool("isGrounded", IsGrounded());
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isClimbing", isClimbing);
        vertical = Input.GetAxis("Vertical");
        if (isLadder && vertical!=0)
        {
            isClimbing = true;
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rigidBody.gravityScale = 0;
            rigidBody.velocity=new Vector2(rigidBody.velocity.x, vertical*moveSpeed);
        }
        else
        {
            rigidBody.gravityScale=1;
        }
    }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    bool IsGrounded()
    {
        return Physics2D.Raycast(this.transform.position, Vector2.down, rayLength, groundLayer.value);
    }

    void Jump()
    {
        if (IsGrounded())
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void Flip()
    {
        isFacingRight=!isFacingRight;
        Vector3 theScale=transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
}


private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "FallLevel")
        {
            print("BGAME OVER");
        } else
        {
            print("WIN OVER");
        }
        if (collision.CompareTag("Bonus")){
            score += 10;
            collision.gameObject.SetActive(false);
            print(score);
        }
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;

        }
    }
}

