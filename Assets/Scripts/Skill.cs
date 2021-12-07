using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rigid;
    CapsuleCollider2D col;

    ComboAttack comboAttack;

    public float dashSpeed;
    public float takeDownSpeed;
    public Text text;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider2D>();
        comboAttack = GetComponent<ComboAttack>();
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
            anim.Play("PlayerSkillA");
            text.gameObject.SetActive(true);
            comboAttack.SetDamageUp();
        }
        else if(Input.GetKeyDown(KeyCode.S)) {
            // 콜라이더 offset size 수정
            rigid.velocity = Vector2.zero;
            rigid.AddForce(Vector2.down*takeDownSpeed);
            anim.Play("PlayerSkillB");
        }
        else if(Input.GetKeyDown(KeyCode.D)) {
            // 플레이어 위치값 + x축으로 1만큼 더해주고
            // 그 곳에 적에게 데미지 주는 영역 설정
            anim.Play("PlayerSkillC");
        }
    }
}