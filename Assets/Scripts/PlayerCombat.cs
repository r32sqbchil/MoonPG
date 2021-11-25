using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    [SerializeField]private Vector2 boxSize;
    [SerializeField]private float damage;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.X))
        {
            Attack();
        }
    }

    void Attack()
    {
        //히트박스 만들기
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.position, boxSize, 0);
        //히트박스에서 맞은 적들의 정보 추출 TakeDamage 함수 불러오기
        foreach(Collider2D col in hitEnemies)
        {
            //print("닿은거 판정은 됨" + col.name);
            //지역변수 enemy에 닿은 녀석의 Enemy클래스를 넣음
            Enemy enemy = col.GetComponent<Enemy>();
            //enemy에 클래스 Enemy가 있는지 검사
            if (enemy)
            {
                //Enemy클래스가 있으면 함수를 호출합니다.
                enemy.TakeDamage(damage);
            }
        }
    }

    //눈에 보이지 않는 박스를 보이게 하는 역할
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(attackPoint.position, boxSize);
    }
}