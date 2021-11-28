using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    int nextMove;//다음 행동지표를 결정할 변수
    Animator animator;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    public GameObject hitBox;

    bool playerCatch;
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        Invoke("Think", 5); // 초기화 함수 안에 넣어서 실행될 때 마다(최초 1회) nextMove변수가 초기화 되도록함    
    }

    void OnAttackToPlayer(){
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null) {
            player.GetComponent<Player>().OnDamage(gameObject);
        }
    }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.tag == "PlayerHitBox")
    //     {
    //         Player player = other.gameObject.GetComponentInParent<Player>();
    //         Debug.Log("Player : " + player);
    //         player.OnDamage(gameObject);
    //     }
    // }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            playerCatch = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            playerCatch = false;
        }
    }

    void Update()
    {
        Attack();
    }
    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove,rigid.velocity.y);

        Vector2 frontVec = new Vector2(rigid.position.x + nextMove*0.4f, rigid.position.y);
        Vector2 ChekPlatform = new Vector2(rigid.position.x + nextMove*0.4f, rigid.position.y);
        
        RaycastHit2D raycast = Physics2D.Raycast(frontVec, Vector3.down,1,LayerMask.GetMask("Platform"));
        RaycastHit2D Platformcheck = Physics2D.Raycast(ChekPlatform, Vector3.right,1,LayerMask.GetMask("Platform"));

        if(raycast.collider == null){
            Turn();
        }
        else if(Platformcheck.collider != null)
        {
            Turn();
        }
    }

    void Attack()
    {
        if(playerCatch == true)
        {
            animator.SetBool("isAttack",true);
            hitBox.SetActive(true);            
            nextMove = 0;
        }

        else
        {
            animator.SetBool("isAttack",false);
            hitBox.SetActive(false);        
        }    
    }

    void Think(){//몬스터가 스스로 생각해서 판단 (-1:왼쪽이동 ,1:오른쪽 이동 ,0:멈춤  으로 3가지 행동을 판단)
        //Set Next Active
        //Random.Range : 최소<= 난수 <최대 /범위의 랜덤 수를 생성(최대는 제외이므로 주의해야함)
        nextMove = Random.Range(-1,2);

        //Sprite Animation
        //WalkSpeed변수를 nextMove로 초기화 
        animator.SetInteger("WalkSpeed",nextMove);


        //Flip Sprite
        if(nextMove != 0) //서있을 때 굳이 방향을 바꿀 필요가 없음 
            spriteRenderer.flipX = nextMove == 1; //nextmove 가 1이면 방향을 반대로 변경  


        //Recursive (재귀함수는 가장 아래에 쓰는게 기본적) 
        float time = Random.Range(2f, 5f); //생각하는 시간을 랜덤으로 부여 
        //Think(); : 재귀함수 : 딜레이를 쓰지 않으면 CPU과부화 되므로 재귀함수쓸 때는 항상 주의 ->Think()를 직접 호출하는 대신 Invoke()사용
        Invoke("Think", time); //매개변수로 받은 함수를 time초의 딜레이를 부여하여 재실행 
    }
    void Turn(){
        nextMove= nextMove*(-1); //우리가 직접 방향을 바꾸어 주었으니 Think는 잠시 멈추어야함
        spriteRenderer.flipX = nextMove == 1;

        CancelInvoke(); //think를 잠시 멈춘 후 재실행
        Invoke("Think",2);//  

    }
}
