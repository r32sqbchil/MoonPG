using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public float hp;
    public float maxHealth = 200f;
    public bool isHit;
    public int enemyDamage;

    CapsuleCollider2D col;
    
    
    Animator anim;
    CameraShake cameraShake;

    private Image healthBar;
    private Text statText;

    public float AFK = 200f;

    public float DEF = 100f;

    public float SPD = 100f;

    public Text hpText;

    public Text afkText;

    public Text defText;

    public Text spdText;

    public void Awake()
    {
        cameraShake = GameObject.FindObjectOfType<CameraShake>();
        col = GetComponent<CapsuleCollider2D>();
        hp = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();

        PlayerHP playerHP = GameObject.FindObjectOfType<PlayerHP>();
        if(playerHP != null){
            healthBar = playerHP.GetComponent<Image>();
            statText = playerHP.GetComponentInChildren<Text>();
        } else {
            Debug.LogWarning("Can't find a component PlayerHP");
        }
    }


    public void Update()
    {
        if(healthBar) healthBar.fillAmount = hp / maxHealth;
        if(statText) statText.text = hp + " " + "/" + " " + maxHealth;

        if(hp <= 0)
        {
            SceneManager.LoadScene(0);
            return;
        } 

        if(hpText) hpText.text = hp + " ";
        if(afkText) afkText.text = AFK + " ";
        if(defText) defText.text = DEF + " ";
        if(spdText) spdText.text = SPD + " ";
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
            // Enemy가 공격할 때
            StartCoroutine(cameraShake.ShakeHorizontalOnly(.1f, .1f));
            hp -= enemyDamage;
        }
        else if(hp <= 0)
        {
            StartCoroutine(cameraShake.ShakeHorizontalOnly(.1f, .1f));
            SceneManager.LoadScene(0);
            // Destroy(this.gameObject, 2f);
            // GetComponent<PlayerMove>().enabled = false;
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

    // void PlayerDie()
    // {
    //     anim.SetTrigger("Die");
    // }
}