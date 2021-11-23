using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;//다음 행동지표를 결정할 변수
    public Animator animator;
    SpriteRenderer spriteRenderer;
    public int Enenmydirection;
    int direction2;
    public GameObject HitBox;


    public LayerMask layerMask;

    public bool isHit;
    // public GameObject Playerch;
    // public GameObject Player;

    public bool Playerch;

    public int damage;
    public int maxHP;
    public int nowHP;
    public float DeathTime;

    public int Questcount;


    // Start is called before the first frame update
    private void Awake() {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        Invoke("Think", 5); // 초기화 함수 안에 넣어서 실행될 때 마다(최초 1회) nextMove변수가 초기화 되도록함
        maxHP = 100;
        damage = 0;
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerHitBox")
        {
            damage += 10;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Playerch = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Playerch = false;
        }
    }

    void Update()
    {
        Attack();
        nowHP = maxHP - damage;

        if(nowHP <= 0)
        {
            animator.SetBool("isDeath", true);
            Destroy(this.gameObject, 2f);
            nextMove =  0;
            Questcount += 1;
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //Move
       rigid.velocity = new Vector2(nextMove,rigid.velocity.y); //nextMove 에 0:멈춤 -1:왼쪽 1:오른쪽 으로 이동 


       //Platform check(맵 앞이 낭떨어지면 뒤돌기 위해서 지형을 탐색)


       //자신의 한 칸 앞 지형을 탐색해야하므로 position.x + nextMove(-1,1,0이므로 적절함)
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove*0.4f, rigid.position.y);
        Vector2 ChekPlatform = new Vector2(rigid.position.x + nextMove*0.4f, rigid.position.y);
        // Vector2 CheckPlayer = new Vector2(rigid.position.x + nextMove*0.5f, rigid.position.y);


        //한칸 앞 부분아래 쪽으로 ray를 쏨
        Debug.DrawRay(frontVec, Vector3.down, new Color(0,1,0));
        Debug.DrawRay(ChekPlatform, Vector3.right, new Color(0, 1, 0));
        // Debug.DrawRay(CheckPlayer, Vector3.down, new Color(0,2,0));



        //레이를 쏴서 맞은 오브젝트를 탐지 
        RaycastHit2D raycast = Physics2D.Raycast(frontVec, Vector3.down,1,LayerMask.GetMask("Platform"));
        // RaycastHit2D PlayCheck = Physics2D.Raycast(frontVec, Vector3.down,1,LayerMask.GetMask("player"));
        RaycastHit2D Platformcheck = Physics2D.Raycast(ChekPlatform, Vector3.right,1,LayerMask.GetMask("Platform"));

        //탐지된 오브젝트가 null : 그 앞에 지형이 없음
        if(raycast.collider == null){
            Turn();
        }

        // else if(PlayCheck.collider != null)
        // {
        //     Playerch = true;
        // }
        // else
        // {
        //     Playerch = false;
        // }

        if(Platformcheck.collider != null)
        {
            Turn();
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

    void Attack()
    {
        if(Playerch == true)
        {
            animator.SetBool("isAttack",true);
            HitBox.SetActive(true);
            nextMove = 0;
        }

        else
        {
            animator.SetBool("isAttack",false);
            HitBox.SetActive(false);
        }
        
        
    }

    void Turn(){

        nextMove= nextMove*(-1); //우리가 직접 방향을 바꾸어 주었으니 Think는 잠시 멈추어야함
        spriteRenderer.flipX = nextMove == 1;

        CancelInvoke(); //think를 잠시 멈춘 후 재실행
        Invoke("Think",2);//  

    }
}
