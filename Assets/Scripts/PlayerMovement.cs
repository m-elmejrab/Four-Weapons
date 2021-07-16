using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Animator animator;
    private Rigidbody2D Rb2d;
    private bool isFacingLeft = false;
    private bool isGrounded;
    private bool jump = false;
    private float horizontalMovement;


    public float moveSpeed = 3.5f; 
    public float jumpForce = 3f;
    AudioSource playerJump;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        
        Rb2d = GetComponent<Rigidbody2D>();
        animator.SetBool("IsMoving", false);

        playerJump = GetComponent<AudioSource>();
		
	}

    void OnCollisionEnter2D(Collision2D colInfo)
    {
        if(colInfo.transform.tag == "Ground")
        {
            isGrounded = true;
            animator.SetBool("IsGrounded", true);
        }
    }

    void Update()
    {
        jump = Input.GetButtonDown("Jump");
        horizontalMovement = Input.GetAxisRaw("Horizontal");

    }

    // Update is called once per frame
    void FixedUpdate () {

        if(horizontalMovement ==0)
        {
            animator.SetBool("IsMoving", false);
            
            Vector3 zeroVelocity = new Vector3(0, Rb2d.velocity.y,0);
            Rb2d.velocity = zeroVelocity;
        }
        else
        {
            animator.SetBool("IsMoving", true);
            Move(horizontalMovement);
        }

        
        if (jump && isGrounded)
            Jump();       

    }

    private void Jump()
    {
        isGrounded = false;
        Rb2d.AddForce(new Vector2(0f, jumpForce),ForceMode2D.Impulse);
        animator.SetBool("IsGrounded", false);
        animator.SetTrigger("Jump");

        playerJump.Play();

    }

    private void Move (float h)
    {
        //Rb2d.MovePosition(gameObject.transform.position + new Vector3(h* moveSpeed, 0,0)*Time.fixedDeltaTime);

        Vector3 targetVelocity = new Vector2(h * moveSpeed, Rb2d.velocity.y);
        // And then smoothing it out and applying it to the character
        Vector3 zeroVelocity = Vector2.zero;
        Rb2d.velocity = Vector3.SmoothDamp(Rb2d.velocity, targetVelocity, ref zeroVelocity, 0.1f);


        if (h>0 && isFacingLeft==true)
        {
            isFacingLeft = false;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        else if (h<0 && isFacingLeft == false)
        {
            isFacingLeft = true;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

    }
}
