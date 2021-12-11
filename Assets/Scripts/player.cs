using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public float hp;
    public float maxHealth = 200f;
    public bool isHit;
    public int enemyDamage;

    CapsuleCollider2D col;
    public Image healthBar;
    public Text statText;
    Animator anim;
    public CameraShake cameraShake;



    public void Awake()
    {
        col = GetComponent<CapsuleCollider2D>();
        hp = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void Update()
    {
        healthBar.fillAmount = hp / maxHealth;
        statText.text = hp + " " + "/" + " " + maxHealth;
    }

    public void OnDamage(GameObject enemy)
    {
        if(isHit) {
            return;
        }

        isHit = true;
        if(enemy.tag == "Enemy") {
            //enemy.GetComponent<Animator>().SetBool("isAttack", true);
        }

        if(hp > 0 )
        {
            StartCoroutine(cameraShake.Shake(.15f, .4f));
            hp -= enemyDamage;
        }
        else if(hp <= 0)
        {
            StartCoroutine(cameraShake.Shake(.15f, .4f));
            Destroy(this.gameObject, 2f);
            GetComponent<PlayerMove>().enabled = false;
        }
        spriteRenderer.material.color = Color.red;
        Invoke("OnDamageEnd",1.5f);
        Invoke("ColorComeback",1.5f);
    }

    void OnDamageEnd()
    {
        isHit = false;
    }

    void ColorComeback()
    {
        spriteRenderer.material.color = Color.white;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // if(other.tag == "Enemy" || other.tag == "BossAttack")
        if(other.tag == "BossAttack")
        {
            OnDamage(other.gameObject);
        }
    }

    // void OnCollisionEnter2D(Collision2D coll)
    // {
    //     if(coll.gameObject.tag == "BossAttack")
    //     {
    //         GameObject bossObject = GameObject.FindGameObjectWithTag("Boss");
    //         if(bossObject != null){
    //             BossSkill bossSkill = bossObject.GetComponent<BossSkill>();

    //             if(bossSkill.lanceRigid.velocity.y < 0){
    //                 OnDamage(bossObject);
    //             }
    //         }
    //     } 
    // }

    // void PlayerDie()
    // {
    //     anim.SetTrigger("Die");
    // }
}