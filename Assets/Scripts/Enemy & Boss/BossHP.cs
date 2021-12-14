using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BossHP : MonoBehaviour
{
    public float maxHealth = 1000;

    public Image healthBar;
    Animator anim;
    private Enemy enemy;

    void Awake()
    {
        anim = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
        enemy.SetHealth(maxHealth);
    }

    public void Update()
    {
        healthBar.fillAmount =  enemy.GetHealth() / maxHealth;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // if(other.tag == "Player")
        //     if(bossStat.Health <= 0)
        //     {
        //         anim.Play("BossDie");
        //         Destroy(this.gameObject, 2f);
        //     }
    }
}