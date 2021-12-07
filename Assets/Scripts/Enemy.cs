using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//구조체 Enemystat을 만듭니다. 안에는 Health나 Speed등을 넣습니다. 구조체를 inspector창에 보일수 있게 Attribute[System.Serializable]을 써줍니다.
[System.Serializable]
struct Enemystat{
    public float health;
}


public class Enemy : MonoBehaviour
{
    //enemy스탯을 초기화 합니다.
    [SerializeField]private Enemystat enemystat;
    Rigidbody2D rigid;
    public GameObject bossHP;
    Animator anim;

    private QuestManager questManager;

    void Awake(){
        questManager = GameObject.FindObjectOfType<QuestManager>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void SetHealth(float health)
    {
        enemystat.health = health;
    }

    public float GetHealth()
    {
        return enemystat.health;
    }

    private void KnockBack(float direction)
    {
        anim.Play("HitEnemyGoblin");
        transform.Translate(Vector2.left*direction*.15f);
    }

    //데미지를 받는 함수 입니다. 인자에는 damage값을 설정해줍니다.
    public void TakeDamage(float direction, float damage)
    {
        EnemyAI enemyAI = GameObject.FindObjectOfType<EnemyAI>();

        //체력이 damage만큼 까지게 합니다.
        enemystat.health -= damage;

        //Debug.Log("Enemy-HP: "+ enemystat.health);

        if(bossHP == null){
            if(enemyAI.playerCatch == false)
                KnockBack(direction);
        }
        // rigid.AddForce(Vector2.right*1.2f, ForceMode2D.Impulse);
        //체력이 0이하로 내려가면 게임 오브젝트를 파괴합니다.
        if(enemystat.health <= 0)
        {
            if(bossHP != null){
                bossHP.SetActive(false);
                Destroy(gameObject);
            }
            else{
                anim.SetBool("isDeath", true);
                questManager.NotifyEvent(this);
                Destroy(gameObject, 2f);
            }
           
        } else {
            rigid.AddForce(Vector2.right*direction*1.2f, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        if(bossHP == null){
            if(transform.position.x < 7f) {
                transform.Translate(new Vector2(transform.position.x -7f, 0));
            } else if(transform.position.x > 14f) {
                transform.Translate(new Vector2(transform.position.x -14f, 0));
            }
        }
    }
}