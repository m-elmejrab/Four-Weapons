using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
    private float timeSinceAttack = 0f;
    public bool playerInRange = false;
    private bool isAttacking = false;
    public Animator anim;

    private BoxCollider2D attackArea;
    private AudioSource attackSound;

    // Use this for initialization
    void Awake () {
        attackArea = GetComponent<BoxCollider2D>();
        attackArea.enabled = false;
        attackSound = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (playerInRange)
        {

            timeSinceAttack += Time.fixedDeltaTime;
            if (timeSinceAttack>2f)
            {
                timeSinceAttack = 0f;
                isAttacking = true;
                Attack();
            }
        }

        
    }

    void Attack()
    {
        attackSound.time = 0.5f;
        attackSound.Play();

        anim.SetTrigger("Attack");
        Invoke("ReenableAttack", 0.5f);
        attackArea.enabled = true;

    }

    void ReenableAttack()
    {
        isAttacking = false;
        attackArea.enabled = false;

    }

    void OnTriggerEnter2D(Collider2D colInfo)
    {
        if(colInfo.transform.tag == "Player")
        {

            if(isAttacking)
            {
                PlayerHealth playerHealth = colInfo.gameObject.GetComponent<PlayerHealth>();
                playerHealth.GetDamage(2);
            }
        }
       
    }

    void OnTriggerExit2D(Collider2D colInfo)
    {
        if (colInfo.transform.tag == "Player")
        {
            isAttacking = false;            
        }

    }
}
