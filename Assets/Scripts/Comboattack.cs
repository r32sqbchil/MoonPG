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
    private string [] attackStates = new string[]{
        "PlayerAttackA", "PlayerAttackB", "PlayerAttackC"
    };

    void Attack(int comboStep){
        attackAnim.Play(attackStates[comboStep-1]);

        float direction;

        if(playerMove.leftAttackBox.transform == attackPoint) {
            direction = -1.0f;
        }else {
            direction = 1.0f;
        }

        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.position, boxSize, 0);
        foreach(Collider2D collider in hitEnemies)
        {
            //지역변수 enemy에 닿은 녀석의 Enemy클래스를 넣음
            Enemy enemy = collider.GetComponent<Enemy>();
            //enemy에 클래스 Enemy가 있는지 검사
            if (enemy)
            {
                //Enemy클래스가 있으면 함수를 호출합니다.
                enemy.TakeDamage(direction, damage*comboStep);
            }
        }
    }

private bool isForced = false;
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
}
