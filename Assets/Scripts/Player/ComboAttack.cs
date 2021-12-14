using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttack : MonoBehaviour
{
    private Animator attackAnim;

    private PlayerMove playerMove;

    public Transform attackPoint;

    [SerializeField]private Vector2 boxSize;
    [SerializeField]private float damage;

    private bool comboCheck;
    private int comboStep = 0;

    private bool isForced = false;

    private string [] attackStates = new string[]{
        "PlayerAttackA", "PlayerAttackB", "PlayerAttackC"
    };

    public AudioSource mySfx;
    public AudioClip attackSfx;
    public AudioClip nonattackSfx;

    void Attack(int comboStep){
        attackAnim.Play(attackStates[comboStep-1]);

        float direction;

        if(playerMove.leftAttackBox.transform == attackPoint) {
            direction = -1.0f;
        }else {
            direction = 1.0f;
        }

        EnemyBase enemy = null;
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.position, boxSize, 0);

        foreach(Collider2D collider in hitEnemies)
        {
            if (enemy = collider.GetComponent<EnemyBase>())
            {
                //Enemy클래스가 있으면 함수를 호출합니다.
                enemy.TakeDamage(gameObject, direction, damage*comboStep);
            }
        }
    }

    public void SetDamageUp()
    {
        if(isForced) {            
            return;
        }

        isForced = true;
        damage++;

        Invoke("SetDamageDown", 5f);
    }

    public void SetDamageDown()
    {
        isForced = false;
        damage--;
    }

    public void OnAttackEvent()
    {
        if(comboStep == 0)
        {
            Attack(comboStep=1);
            return;
        }
        else
        {
            if(comboCheck)
            {
                comboStep += 1; //next step
                comboCheck = false;
            }
            else {
                AnimatorStateInfo stateInfo = attackAnim.GetCurrentAnimatorStateInfo(0);
                foreach (string state in attackStates)
                {
                    if(stateInfo.IsName(state)){
                        return;
                    }
                }
                //Debug.Log("Reset PlayerAttack");
                comboStep = 0;
            }
        }
    }

    public void OnComboCheck()
    {
        comboCheck = true;
    }

    public void OnCombo()
    {
        if(comboCheck){
            //Debug.Log("OnCombo - comboCheck:true, not yet run attack-procedure");
            comboStep += 1; //next step
            comboCheck = false;
        }
    
        if(comboStep == 2 || comboStep == 3)
        {
            Attack(comboStep);
        }
    }


    public void OnComboReset()
    {
        comboCheck = false;
        comboStep = 0;
    }

    void Start(){
        playerMove = GetComponent<PlayerMove>();
        attackAnim = GetComponent<Animator>();

        attackPoint = playerMove.rightAttackBox.transform;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.X))
        {
            OnAttackEvent();
        }
    }

    public void AttackSound()
    {
        mySfx.PlayOneShot (attackSfx);
    }
    public void nonAttackSound()
    {
        mySfx.PlayOneShot (nonattackSfx);
    }
}
