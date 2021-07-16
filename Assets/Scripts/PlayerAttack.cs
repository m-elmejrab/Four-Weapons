using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private bool isAttacking = false;
    Animator anim;
    public BoxCollider2D attackArea;

	// Use this for initialization
	void Awake () {
        anim = GetComponent<Animator>();
        attackArea.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        bool attack = Input.GetKey("e");
        if(attack && isAttacking==false)
        {
            isAttacking = true;
            Attack();
        }
	}

    void Attack ()
    {
        anim.SetBool("IsAttacking", true);
        Invoke("ReenableAttack", 0.5f);
        attackArea.enabled = true;
        
    }
    void ReenableAttack()
    {
        isAttacking = false;
        attackArea.enabled = false;
        anim.SetBool("IsAttacking", false);

    }

    void OnTriggerEnter2D(Collider2D colInfo)
    {
        
        if (isAttacking && colInfo.transform.tag == "Enemy")
        {
            EnemyHealth enemyHealth = colInfo.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.GetDamage(3);

        }
    }
}
