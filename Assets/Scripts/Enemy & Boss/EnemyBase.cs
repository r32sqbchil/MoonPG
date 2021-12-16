using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//구조체 Enemystat을 만듭니다. 안에는 Health나 Speed등을 넣습니다. 구조체를 inspector창에 보일수 있게 Attribute[System.Serializable]을 써줍니다.
[System.Serializable]
struct EnemyStat{
    public float health;
}

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private EnemyStat enemyStat;

    private GameManager gameManager;
    private CameraShake cameraShake;

    private Rigidbody2D rigid;

    private bool bTakenEffect = false;
    private bool bCatchPlayer = false;
    private bool insightOfPlayer = false;

    private EnemyMove enemyMove;

    [HideInInspector] public int movingDirection = 0; //(-1:왼쪽방향, 0:정지, 1:오른쪽방향)

    public GameObject movableArea;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        cameraShake = GameObject.FindObjectOfType<CameraShake>();
        rigid = GetComponent<Rigidbody2D>();
        if(!movableArea){
            movableArea = GameObject.Find("EnemyMovableArea");
        }
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        if(movingDirection != 0){
            if (enemyMove != null) enemyMove.OnMove(rigid, movingDirection);
            Vector2 direction2D = Vector2.right * movingDirection;

            Vector2 boundOf = rigid.position + direction2D * transform.localScale.x;
            //Vector2 frontVec = new Vector2(rigid.position.x + movingDirection*0.4f, rigid.position.y);

            RaycastHit2D groundTester = Physics2D.Raycast(boundOf, Vector2.down,1.0f,LayerMask.GetMask("Platform"));
            RaycastHit2D wallTester = Physics2D.Raycast(boundOf, direction2D,.4f,LayerMask.GetMask("Platform"));
            RaycastHit2D playerCheck = Physics2D.Raycast(boundOf, direction2D,5.0f,LayerMask.GetMask("Player"));

            if(groundTester.collider == null || wallTester.collider != null){
                if(enemyMove != null) enemyMove.OnTurn();
            }
            else if(playerCheck.collider != null)
            {
                if(!insightOfPlayer)
                {
                    insightOfPlayer = true;
                    if(enemyMove != null) enemyMove.InSightOfPlayer();
                }
            }
            else if(insightOfPlayer)
            {
                insightOfPlayer = false;
                if(enemyMove != null) enemyMove.OutSightOfPlayer();
            }
        }

        LimitMovingArea();
    }

    void LimitMovingArea(){
        if(movableArea!=null){
            float left = transform.position.x - transform.localScale.x/2;
            float right = transform.position.x + transform.localScale.x/2;

            float limitMoveXMin = movableArea.transform.position.x - movableArea.transform.localScale.x/2;
            float limitMoveXMax = movableArea.transform.position.x + movableArea.transform.localScale.x/2;

            if(left < limitMoveXMin) {
                transform.Translate(new Vector2(left - limitMoveXMin, 0));
            } else if(right > limitMoveXMax) {
                transform.Translate(new Vector2(right - limitMoveXMax, 0));
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.GetComponent<Player>()) bCatchPlayer = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.GetComponent<Player>()) bCatchPlayer = false;
    }

    public void SetIEnemyBody(EnemyMove enemyMove)
    {
        this.enemyMove = enemyMove;
    }

    public bool isAlive()
    {
        return enemyStat.health > 0;
    }

    public bool isCatchPlayer()
    {
        return bCatchPlayer;
    }
    

    //데미지를 받는 함수 입니다. 인자에는 damage값을 설정해줍니다.
    public void TakeDamage(GameObject playerObject, float direction, float damage)
    {
        //체력이 damage만큼 까지게 합니다.
        enemyStat.health -= damage;

        //Debug.Log("Enemy-HP: "+ enemyStat.health);

        if(isAlive())
        {
            StartCoroutine(cameraShake.ShakeHorizontalOnly(.1f, .1f));
            if(!isCatchPlayer())
            {
                if(enemyMove!=null) enemyMove.OnKnockBack(direction, damage);
            }
            rigid.AddForce(Vector2.right*direction*1.2f, ForceMode2D.Impulse);
        }
        else if(!bTakenEffect) 
        {
            bTakenEffect = true;
            if (enemyMove != null) enemyMove.OnDied();
            gameManager.Action(gameObject, GameManager.ACTION_ON_DIED);
            Destroy(gameObject, 2f);
        }
    }
}
