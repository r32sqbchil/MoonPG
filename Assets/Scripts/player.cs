using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float hp;
    public float maxHealth = 200f;
    public bool isHit;
    public int enemyDamage;

    CapsuleCollider2D col;
    public Image healthBar;
    public Text statText;
    Animator anim;



    public void Awake()
    {
        col = GetComponent<CapsuleCollider2D>();
        hp = maxHealth;
    }


    public void Update()
    {
        healthBar.fillAmount = hp / maxHealth;
        statText.text = hp + " " + "/" + " " + maxHealth;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy" || other.tag == "BossAttack")
        {
            isHit = true;
            if(hp > 0 )
            {
                hp -= enemyDamage;
                // StartCoroutine(PlayerCollider());
            }
            else if(hp <= 0)
            {
                Destroy(this.gameObject, 2f);
                GetComponent<PlayerMove>().enabled = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        BossSkill bossSkill = GameObject.FindObjectOfType<BossSkill>();

        if(coll.gameObject.tag == "BossAttack" && bossSkill.lanceRigid.velocity.y < 0)
        {
            isHit = true;
            if(hp > 0 )
            {
                hp -= enemyDamage;
            }
            else if(hp <= 0)
            {
                Destroy(this.gameObject, 2f);
                GetComponent<PlayerMove>().enabled = false;
            }
        } 
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            isHit = false;
        }
    }

    IEnumerator PlayerCollider()
    {
        col.enabled = false;
        yield return new WaitForSeconds(1f);
        col.enabled = true;
    }

    // void PlayerDie()
    // {
    //     anim.SetTrigger("Die");
    // }
}