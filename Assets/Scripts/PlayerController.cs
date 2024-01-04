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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(xInput * speed, rb.velocity.y);


        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                //make it jump
                Jump();
            }
            else
            {
                if (doubleJump)
                {
                    //Jump();
                    DoubleJump();
                    doubleJump = false;
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
        rb.velocity = new Vector2(rb.velocity.x, jumpForce/2);
    }

    void CheckDirection()
    {
        if(rb.velocity.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if(rb.velocity.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
