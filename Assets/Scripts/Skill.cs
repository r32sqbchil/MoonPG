using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rigid;
    BoxCollider2D col;

    public float dashSpeed;
    public float TakeDownSpeed;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire3"))
        {
            col.enabled = false;
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * dashSpeed, rigid.velocity.y);
            anim.Play("PlayerDash");
            col.enabled = true;
        }

        if(Input.GetKeyDown(KeyCode.A)) {
            // 플레이어의 공격력을 올려줌
            anim.Play("PlayerSkillA");
        }
        else if(Input.GetKeyDown(KeyCode.B)) {
            // 콜라이더 offset size 수정
            rigid.velocity = Vector2.zero;
            rigid.AddForce(Vector2.down*TakeDownSpeed);
            anim.Play("PlayerSkillB");
        }
        else if(Input.GetKeyDown(KeyCode.C)) {
            // 플레이어 위치값 + x축으로 1만큼 더해주고
            // 그 곳에 적에게 데미지 주는 영역 설정
            anim.Play("PlayerSkillC");
        }

    }
}