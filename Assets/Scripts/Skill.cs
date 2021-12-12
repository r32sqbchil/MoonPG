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
        if(Input.GetKeyDown(KeyCode.A)) {
            anim.Play("PlayerSkillA");
            text.gameObject.SetActive(true);
            comboAttack.SetDamageUp();
        }

        else if(Input.GetKeyDown(KeyCode.D)) {
            // 플레이어 위치값 + x축으로 1만큼 더해주고
            // 그 곳에 적에게 데미지 주는 영역 설정
            anim.Play("PlayerSkillC");
        }
    }
}