using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    private int health = 4;



    public void GetDamage(int amount)
    {
        health -= amount;

        CheckIfDead();
    }

    void CheckIfDead()
    {
        if(health<=0)
        {
            gameObject.transform.parent.gameObject.SetActive(false);

        }
    }
}
