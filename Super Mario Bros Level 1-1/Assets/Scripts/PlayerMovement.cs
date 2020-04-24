using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    internal bool extraLife;

    public float speed;             //this is the speed our player will move
    public float initialSpeed;             //this is the initial speed our player will move
    private Rigidbody2D rb;         //variable to store Rigidbody2D component
    private Animator anim;         //variable to store Rigidbody2D component
    private SpriteRenderer sprite;         //variable to store Rigidbody2D component
    private float moveInput;        //this store the input value

    public float jumpForce;         //force with which player jump
    public bool isGrounded;        //this variable will tell if our player is grounded or not
    public Transform feetPos;       //this variable will store reference to transform from where we will create a circle
    public float circleRadius;      //radius of circle
    public LayerMask whatIsGround;  //layer our ground will have.

    public float jumpTime;          //time till which we will apply jump force
    private float jumpTimeCounter;  //time to count how long player has pressed jump key
    public bool isJumping;         //bool to tell if player is jumping or not

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();   //get reference to 	Rigidbody2D component
        anim = GetComponent<Animator>();   //get reference to 	Rigidbody2D component
        sprite = GetComponent<SpriteRenderer>();   //get reference to 	Rigidbody2D component

        initialSpeed = speed;

    }

    //as we are going to use physics for our player , we must use FixedUpdate for it
    void FixedUpdate ()
    {
        //the moveInput will be 1 when we press right key and -1 for left key
        moveInput = Input.GetAxis("Horizontal");
        if (moveInput > 0)                                  //moving towards right side
        {
            sprite.flipX = false;
            anim.SetBool("Run", true);
        }
        else if (moveInput < 0)                             //moving towards left side
        {
            sprite.flipX = true;
            anim.SetBool("Run", true);
        }
        else
            anim.SetBool("Run", false);

        //here we set our player x velocity and y will not ne changed
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
	}

    private void Update()
    {
        //here we set the isGrounded
        isGrounded = Physics2D.OverlapCircle(feetPos.position, circleRadius, whatIsGround);
        
        //we check if isGrounded is true and we pressed Space button
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))  
        {
            isJumping = true;                           //we set isJumping to true
            jumpTimeCounter = jumpTime;                 //set jumpTimeCounter
            rb.velocity = Vector2.up * jumpForce;       //add velocity to player
            anim.SetBool("Jump", true);
        }
        else
        {
            anim.SetBool("Jump", false);
        }

        //if Space key is pressed and isJumping is true
        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)                    //we check if jumpTimeCounter is more than 0
            {
                rb.velocity = Vector2.up * jumpForce;   //add velocity
                jumpTimeCounter -= Time.deltaTime;      //reduce jumpTimeCounter by Time.deltaTime
            }
            else                                        //if jumpTimeCounter is less than 0
            {
                isJumping = false;                      //set isJumping to false
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))              //if we unpress the Space key
        {
            isJumping = false;                          //set isJumping to false
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag =="Enemy")
        {
            if (extraLife)
            {
                extraLife = false;
                anim.SetBool("ExtraLife", false);
                rb.AddForce(new Vector2 (sprite.flipX?-200:200, 200));
            }
            else
            {
                anim.Play("Die");
                Time.timeScale = 0;

            }

            return;
        }

        if (collision.gameObject.tag == "Mushroom")
        {
            anim.SetBool("ExtraLife", true);

            Destroy(collision.gameObject);
            extraLife = true;
            return;
        }

        //To avoid air colisions ock movement
        if (collision.gameObject.tag == "Ground")
        {
            speed = initialSpeed;
            return;
        }


        //To avoid air colisions ock movement
        if (collision.contacts[0].normal != Vector2.up && !isGrounded)
        {
            speed = 0;
        }
        else
        {
            speed = initialSpeed;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
            speed = initialSpeed;

    }


}
