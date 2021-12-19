using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttack : MonoBehaviour
{
    private Animator attackAnim;

    private PlayerMove playerMove;

    public Transform attackPoint;

    [SerializeField]private Vector2 boxSize;


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

        Attack(direction, attackPoint.position, boxSize, comboStep);
    }

    public void Attack(float direction, Vector3 point, Vector2 box, float multiple){
        if(direction == 0) return;

        EnemyBase enemy = null;
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(point, box, 0);
        foreach(Collider2D collider in hitEnemies) {
            if (enemy = collider.GetComponent<EnemyBase>()) {
                if(enemy.isAlive()) {
                    float damage = GetComponent<Player>().GetAtk();
                    if(multiple == 4) {
                        Debug.Log("Take Down!!!");
                    }
                    enemy.TakeDamage(gameObject, direction, damage*multiple);
                }
            }
        }
    }

    public void SetDamageUp()
    {
        if(isForced) {            
            return;
        }

        isForced = true;
        Player player = GetComponent<Player>();
        player.IncreaseAtk(1);

        Invoke("SetDamageDown", 5f);
    }

    public void SetDamageDown()
    {
        isForced = false;

        Player player = GetComponent<Player>();
        player.IncreaseAtk(-1);
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
