using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour
{
    // config variables
    [SerializeField] float moveSpeed = 5f;

    // cache
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Moves character on X and Y planes according to input.
    /// </summary>
    /// <param name="horDirection"></param>
    /// <param name="verDirection"></param>
    public void Move(float horDirection, float verDirection)
    {
        float deltaX = 
                        horDirection == 0 ? (rb.velocity.x) : (horDirection * moveSpeed);
        float deltaY = 
                        verDirection == 0 ? (rb.velocity.y) : (verDirection * moveSpeed);

        rb.velocity = new Vector2(deltaX, deltaY);
        FaceDirection();
    }

    /// <summary>
    /// Makes sure character is facing dorection of movement (on X axis)
    /// </summary>
    private void FaceDirection()
    {
        bool isMovingOnX = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon; //to avoid physics-related errors

        if (isMovingOnX)
        {
            if (rb.velocity.x > 0) { spriteRenderer.flipX = false; }
            else { spriteRenderer.flipX = true; }
        }
    }

}
