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
    }
}