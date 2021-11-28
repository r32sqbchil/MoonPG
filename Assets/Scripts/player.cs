using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float HP;
    public float maxHealth = 200f;
    public bool isHit;
    public int GoblinDamage;

    CapsuleCollider2D col;
    public Image healthBar;
    public Text statText;
    Animator anim;



    public void Awake()
    {
        col = GetComponent<CapsuleCollider2D>();
        HP = maxHealth;
    }


    public void Update()
    {
        healthBar.fillAmount = HP / maxHealth;
        statText.text = HP + " " + "/" + " " + maxHealth;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            isHit = true;
            if(HP > 0 )
            {
                HP -= GoblinDamage;
                // StartCoroutine(PlayerCollider());
            }
            else if(HP <= 0)
            {
                Destroy(this.gameObject, 2f);
                this.gameObject.GetComponent<PlayerMove>().enabled = false;
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