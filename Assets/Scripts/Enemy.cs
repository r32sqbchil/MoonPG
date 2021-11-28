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
    public GameObject BossHP;

    void Awake(){
        rigid = GetComponent<Rigidbody2D>();
    }

    public void SetHealth(float health)
    {
        enemystat.health = health;
    }

    public float GetHealth()
    {
        return enemystat.health;
    }
    //데미지를 받는 함수 입니다. 인자에는 damage값을 설정해줍니다.
    public void TakeDamage(float direction, float damage)
    {
        //체력이 damage만큼 까지게 합니다.
        enemystat.health -= damage;

        Debug.Log("Enemy-HP: "+ enemystat.health);
        //체력이 0이하로 내려가면 게임 오브젝트를 파괴합니다.
        if(enemystat.health <= 0)
        {
            BossHP.SetActive(false);
            Destroy(gameObject);
        } else {
            rigid.AddForce(Vector2.right*direction*1.2f, ForceMode2D.Impulse);
        }
    }
}