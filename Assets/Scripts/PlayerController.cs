using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    float xInput;
    public float speed;
    bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float jumpForce;
    Animator anim;
    bool doubleJump = true;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(xInput * speed, rb.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundDistance, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                Jump();
            }
            else
            {
                if (doubleJump)
                {
                    // Добавлена проверка на землю перед выполнением второго прыжка
                    if (Physics2D.OverlapCircle(groundCheck.position, groundDistance, groundLayer))
                    {
                        DoubleJump();
                        doubleJump = false;
                    }
                }
            }
        }

        if (isGrounded)
        {
            doubleJump = true;
        }

        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));

        CheckDirection();
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void DoubleJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce / 2);
    }

    void CheckDirection()
    {
        if (rb.velocity.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (rb.velocity.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
