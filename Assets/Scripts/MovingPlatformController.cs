using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    private bool isMovingRight = false;
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
        if (isMovingRight)
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
            if (this.transform.position.x > startPositionX - moveRange)
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
        startPositionX = this.transform.position.x;
    }

    void Flip()
    {
        isMovingRight = !isMovingRight;
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
        
    }
}
