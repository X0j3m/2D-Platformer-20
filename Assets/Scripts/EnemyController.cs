using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyController : MonoBehaviour
{
    private bool isFacingRight = false;
    private Animator animator;
    [Range(0.01f, 20.0f)][SerializeField] private float moveSpeed = 0.1f; // moving speed of the enemy
    [Range(0.01f, 20.0f)][SerializeField] private float moveRange = 1.0f; // moving range of the enemy
    private float startPositionX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFacingRight)
        {
            if (this.transform.position.x < startPositionX + moveRange)
            {
                MoveRight();
            }
            else
            {
                Flip();
                MoveLeft();
            }
        }
        else
        {
            if(this.transform.position.x > startPositionX - moveRange)
            {
                MoveLeft();
            }
            else
            {
                Flip();
                MoveRight();
            }
        }
        
    }

    private void Awake()
    {
        startPositionX= this.transform.position.x;
        animator = GetComponent<Animator>();
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void MoveRight()
    {
        transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);

    }

    void MoveLeft()
    {
        transform.Translate(-1 * moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (transform.position.y < collision.gameObject.transform.position.y) {
                animator.SetBool("isDead",true);
                StartCoroutine( KillOnAnimationEnd() );
            }else{
                Debug.Log("GAME OVER!");
            }
        }
    }

    IEnumerator KillOnAnimationEnd() {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
