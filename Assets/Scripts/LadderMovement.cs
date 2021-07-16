using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{

    GameObject playerOBJ;
    bool canClimb = false;
    float speed = 1f;

    void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "Player")
        {
            canClimb = true;
            playerOBJ = coll.gameObject;
            Rigidbody2D rb = playerOBJ.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
        }
    }

    void OnTriggerExit2D(Collider2D coll2)
    {
        if (coll2.gameObject.tag == "Player")
        {
            canClimb = false;
            if(playerOBJ!=null)
            {
                Rigidbody2D rb = playerOBJ.GetComponent<Rigidbody2D>();
                rb.gravityScale = 1;
                playerOBJ = null;
            }
            
        }
    }
    void Update()
    {
        if (canClimb)
        {

            Rigidbody2D rb = playerOBJ.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            Vector3 zeroVelocity = new Vector3(0, 0, 0);
            rb.velocity = zeroVelocity;


            if (Input.GetKey(KeyCode.W))
            {
                playerOBJ.transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                playerOBJ.transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * speed);
            }
        }
    }
}