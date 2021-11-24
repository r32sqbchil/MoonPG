using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//구조체 Enemystat을 만듭니다. 안에는 Health나 Speed등을 넣습니다. 구조체를 inspector창에 보일수 있게 Attribute[System.Serializable]을 써줍니다.
[System.Serializable]
struct Enemystat{
    public float Health;
}
public class Enemy : MonoBehaviour
{
    //enemy스탯을 초기화 합니다.
    [SerializeField]private Enemystat enemystat;
    // Start is called before the first frame update
    void Start()
    {
        //처음 시작시 체력은 100
        enemystat.Health = 100;
    }

    //데미지를 받는 함수 입니다. 인자에는 damage값을 설정해줍니다.
    public void TakeDamage(float damage)
    {
        //체력이 damage만큼 까지게 합니다.
        enemystat.Health -= damage;
        Debug.Log("Enemy-HP: "+ enemystat.Health);
        //체력이 0이하로 내려가면 게임 오브젝트를 파괴합니다.
        if(enemystat.Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}