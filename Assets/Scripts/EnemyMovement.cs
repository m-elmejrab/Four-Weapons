using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public Rigidbody2D Rb2d;
    private bool isFacingLeft = true;
    private bool isGrounded;
    private bool jump = false;
    private bool followPlayer = false;
    private GameObject player;
    public Transform attackArea;
    private EnemyAttack attackAreaBehavior;

    public float moveSpeed = 3.5f;
    public float jumpForce = 3f;
    public BoxCollider2D playerAlert;

    // Use this for initialization
    void Awake()
    {

        //Rb2d = GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player");
        //attackArea = gameObject.transform.Find("AttackArea");
        //attackArea = transform.parent.transform.Find("AttackArea");
        //attackArea = gameObject.GetComponentInParent<Transform>().Find("AttackArea");
        attackAreaBehavior = attackArea.GetComponent<EnemyAttack>();
    }

    void OnCollisionEnter2D(Collision2D colInfo)
    {
        if (colInfo.transform.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    

    // Update is called once per frame
    void FixedUpdate()
    {

        if(followPlayer)
        {
            Move();
            
        }

        //if (horizontalMovement == 0)
        //{
        //    animator.SetBool("IsMoving", false);

        //    Vector3 zeroVelocity = new Vector3(0, Rb2d.velocity.y, 0);
        //    Rb2d.velocity = zeroVelocity;
        //}
        //else
        //{
        //    animator.SetBool("IsMoving", true);
        //    Move(horizontal);
        //}


        //if (jump && isGrounded)
        //    Jump();

    }

    private void Jump()
    {
        isGrounded = false;
        Rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }

    private void Move()
    {
        //Rb2d.MovePosition(gameObject.transform.position + new Vector3(h* moveSpeed, 0,0)*Time.fixedDeltaTime);
        Vector3 direction = player.transform.position - gameObject.transform.position;
        direction = direction.normalized;
        direction.y = 0f;
        Vector3 targetVelocity = new Vector2(moveSpeed * direction.x, Rb2d.velocity.y);
        // And then smoothing it out and applying it to the character
        Vector3 zeroVelocity = Vector2.zero;
        Rb2d.velocity = Vector3.SmoothDamp(Rb2d.velocity, targetVelocity, ref zeroVelocity, 0.1f);


        if (Rb2d.velocity.x > 0 && isFacingLeft == true)
        {
            isFacingLeft = false;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        else if (Rb2d.velocity.x <0 && isFacingLeft == false)
        {
            isFacingLeft = true;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

    }

    void OnTriggerEnter2D (Collider2D colObj)
    {
        if(colObj.transform.tag == "Player")
        {
            followPlayer = true;
            attackAreaBehavior.playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D colObj)
    {
        if (colObj.transform.tag == "Player")
        {
            followPlayer = false;
            attackAreaBehavior.playerInRange = false;

            StopMovement();
            Debug.Log("EnemyMovement: StopMovementCalled");

        }
    }
    private void StopMovement()
    {
        Rb2d.velocity = Vector3.zero;
    }

}
