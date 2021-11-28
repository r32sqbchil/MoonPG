using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHP : MonoBehaviour
{
    public float maxHealth = 1000;
    float health;
    // public Enemy Health;

    public Image healthBar;
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        health = maxHealth;
    }

    public void Update()
    {
        if(Input.GetKey(KeyCode.X))
        {
            health -= 1;
        }

        healthBar.fillAmount =  health / maxHealth;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
            if(health <= 0)
            {
                anim.Play("BossDie");
                Destroy(this.gameObject, 2f);
            }
    }
}