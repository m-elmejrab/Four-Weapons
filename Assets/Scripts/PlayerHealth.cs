using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    private int health = 6;
    private bool damaged = false;
    private bool isInvincible = false;
    private float invnincibleDuration = 0f;
    private Color flashColor = new Color(1f, 0f, 0f, 0.3f);
    private float flashSpeed = 1f;

    public Image damageImage;


    void Update()
    {
        if(isInvincible)
        {
            invnincibleDuration -= Time.deltaTime;
            if(invnincibleDuration<=0f)
            {
                isInvincible = false;
                invnincibleDuration = 0f;
            }
        }
        if (damaged)
        {
            damageImage.color = flashColor;

        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    public void GetDamage(int amount)
    {
        if(!isInvincible)
        {
            health -= amount;
            damaged = true;
            CheckIfDead();
            SetInvincible(2f);
            Debug.Log("PlayerHealth: Health now is ->" + health);
        }
        

    }

    private void SetInvincible(float duaration)
    {
        invnincibleDuration = duaration;
        isInvincible = true;
    }

    void CheckIfDead()
    {
        if (health <= 0)
        {
            GameManager.instance.GameOver();
        }
    }
}
