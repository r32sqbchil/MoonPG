using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    private float hp;
    private float atk;

    public float maxHealth = 200f;
    public bool isHit;

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
    bool is0HP = false;

    public void Awake()
    {
        anim = GetComponent<Animator>();
        cameraShake = GameObject.FindObjectOfType<CameraShake>();
        col = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        PlayerHP playerHP = GameObject.FindObjectOfType<PlayerHP>();
        if(playerHP != null){
            healthBar = playerHP.GetComponent<Image>();
            statText = playerHP.GetComponentInChildren<Text>();
        } else {
            Debug.LogWarning("Can't find a component PlayerHP");
        }
    }

    void Start()
    {
        SettingForPlayerStat();
    }

    void Update()
    {
        if(healthBar) healthBar.fillAmount = GetHp() / maxHealth;
        if(statText) statText.text = GetHp() + " " + "/" + " " + maxHealth;

        if(GetHp() <= 0 && !is0HP)
        {
            is0HP = true;
            if(is0HP)
            {
                anim.SetTrigger("PlayerDie");
                Fade fade = GameObject.FindObjectOfType<Fade>();
                if(fade) fade.Invoke("FadeIn", 1f);
                Invoke("SceneLoad", 3f);
            }
            return;
        }


        if(hpText) hpText.text = GetHp() + " ";
        if(afkText) afkText.text = AFK + " ";
        if(defText) defText.text = DEF + " ";
        if(spdText) spdText.text = SPD + " ";
    }

    public void OnDamage(EnemyBase enemyBase)
    {
        if(isHit) {
            return;
        }

        isHit = true;
        if(enemyBase.tag == "Enemy") {
            //enemy.GetComponent<Animator>().SetBool("isAttack", true);
        }

        if(GetHp() > 0 )
        {
            // Enemy가 공격할 때
            StartCoroutine(cameraShake.ShakeHorizontalOnly(.1f, .1f));
            IncreaseHp(-enemyBase.GetAttackPoint());
        }
        else if(GetHp() <= 0)
        {
            StartCoroutine(cameraShake.ShakeHorizontalOnly(.1f, .1f));
        }
        spriteRenderer.material.color = Color.red;
        Invoke("OnDamageEnd",1.5f);
        Invoke("ColorComeback",1.5f);
    }

    void SceneLoad()
    {
        SceneManager.LoadScene(0);
    }

    public float GetHp(){
        return hp;
    }
    public void IncreaseHp(float point){
        hp = Mathf.Max(0, Mathf.Min(hp+point, maxHealth));
        globalContext["HP"] = hp;
    }
    public float GetAtk(){
        return atk;
    }

    public void IncreaseAtk(float point){
        atk += point;
        globalContext["ATK"] = atk;
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
            EnemyBase enemyBase = other.GetComponent<EnemyBase>();
            if(enemyBase) OnDamage(enemyBase);
        }
    }

    Dictionary<string, object> globalContext;
    void SettingForPlayerStat(){
        QuestManager questManager = GameManager.FindQuestManager();
        globalContext = questManager.GetQuestContext(Scene._GLOBAL_, 0, 0);
        
        hp = GetContextValue("HP", maxHealth);
        Debug.Log("hp:"+hp);
        atk = GetContextValue("ATK", 1);
        Debug.Log("atk:"+atk);
    }

    float GetContextValue(string key, float defaultValue){
        if(globalContext.ContainsKey(key)){
            return (float)globalContext[key];
        } else {
            globalContext.Add(key, defaultValue);
            return defaultValue;
        }
    }

    // void PlayerDie()
    // {
    //     anim.SetTrigger("Die");
    // }
}