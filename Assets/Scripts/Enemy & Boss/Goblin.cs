using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour, IEnemyBody
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private GameObject hitBox;
    private Player player;
    private EnemyBase enemyBase;


    // Start is called before the first frame update
    void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
        enemyBase.SetIEnemyBody(this);

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        foreach(BoxCollider2D collider in GetComponentsInChildren<BoxCollider2D>()){
            if(collider.gameObject.name == "HitBox"){
                hitBox = collider.gameObject;
                break;
            }
        }

        player = GameObject.FindObjectOfType<Player>();
        Invoke("Think", 5); // 초기화 함수 안에 넣어서 실행될 때 마다(최초 1회) movingDirection 변수가 초기화 되도록함
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void OnAttackToPlayer() {
        if(player) player.OnDamage(gameObject);
    }

    void Attack()
    {
        if(enemyBase.isCatchPlayer()){
            if(!animator.GetBool("isAttack")){
                animator.SetBool("isAttack",true);
                enemyBase.movingDirection = 0;
                CancelInvoke();
                Invoke("SyncSetting", 0.7f);
            }
        } else if(animator.GetBool("isAttack")){
            animator.SetBool("isAttack",false);
            if(hitBox) hitBox.SetActive(false);
            Invoke("Think", 1f);
        }
    }

    void SyncSetting()
    {
        if(hitBox) hitBox.SetActive(true);
        enemyBase.movingDirection = 0;
    }

    void Think()
    {
        //몬스터가 스스로 생각해서 판단 (-1:왼쪽이동 ,1:오른쪽 이동 ,0:멈춤  으로 3가지 행동을 판단)
        //Set Next Active
        //Random.Range : 최소<= 난수 <최대 /범위의 랜덤 수를 생성(최대는 제외이므로 주의해야함)
        int walkSpeed = Random.Range(0,5)-2;
        Walk(walkSpeed>0?1:walkSpeed<0?-1:0);

        //Recursive (재귀함수는 가장 아래에 쓰는게 기본적) 
        float time = Random.Range(2f, 5f); //생각하는 시간을 랜덤으로 부여 
        //Think(); : 재귀함수 : 딜레이를 쓰지 않으면 CPU과부화 되므로 재귀함수쓸 때는 항상 주의 ->Think()를 직접 호출하는 대신 Invoke()사용
        Invoke("Think", time); //매개변수로 받은 함수를 time초의 딜레이를 부여하여 재실행 
    }

    void Walk(int walkSpeed){

        enemyBase.movingDirection = walkSpeed;

        //Sprite Animation
        //WalkSpeed변수를 nextMove로 초기화 
        if(animator) animator.SetInteger("WalkSpeed", walkSpeed);

        //Flip Sprite
        if(walkSpeed != 0) //서있을 때 굳이 방향을 바꿀 필요가 없음 
            spriteRenderer.flipX = walkSpeed == 1; //nextmove 가 1이면 방향을 반대로 변경
    }

    public void OnTurn()
    {
        Walk(-1*enemyBase.movingDirection);

        CancelInvoke(); //think 예약을 취소한다 -> 계속 걷도록 하고
        Invoke("Think",2); //2초 뒤에 결정토록 한다
    }

    public void InSightOfPlayer()
    {
        CancelInvoke(); //think 예약을 취소한다 -> 계속 걷도록 한다
    }

    public void OutSightOfPlayer()
    {
        CancelInvoke(); //think 예약을 취소한다 -> 계속 걷도록 한다
        Invoke("Think",2); //2초간 계속 걷는다
    }

    public void OnKnockBack(float direction, float damage)
    {
        animator.Play("HitEnemyGoblin");
        //transform.Translate(Vector2.left*direction*.15f);
    }
}
