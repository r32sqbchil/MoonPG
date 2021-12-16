using UnityEngine;

public abstract class EnemyMove : MonoBehaviour
{
    protected EnemyBase enemyBase;
    protected Player player;
    
    private GameObject hitBox;

    protected abstract float GetStandingTimeOnStart();
    protected abstract bool IsAttackOn();
    protected abstract void SetAttackOn(bool attackOn);

    protected void Initialize()
    {
        enemyBase = GetComponent<EnemyBase>();
        enemyBase.SetIEnemyBody(this);

        player = GameObject.FindObjectOfType<Player>();

        foreach (BoxCollider2D collider in GetComponentsInChildren<BoxCollider2D>())
        {
            if (collider.gameObject.name == "HitBox")
            {
                hitBox = collider.gameObject;
                break;
            }
        }

        // 초기화 함수 안에 넣어서 실행될 때 마다(최초 1회) movingDirection 변수가 초기화 되도록함
        Invoke("Think", GetStandingTimeOnStart());
    }

    protected void ActivateHitBox(bool activate)
    {
        if (hitBox) hitBox.SetActive(true);
    }

    protected void EmitOnDamageToPlayer()
    {
        if (player) player.OnDamage(gameObject);
    }

    protected void AttackPlayerIfTouch()
    {
        if (enemyBase.isCatchPlayer())
        {
            if (!IsAttackOn())
            {
                SetAttackOn(true); //공격 모드 가동
                enemyBase.movingDirection = 0; // 이동 멈추기
                CancelInvoke();
            }
        }
        else if (IsAttackOn())
        { //잡기 못했는데 공격모드라면?
            SetAttackOn(false); //공격 모드를 풀고
            StandAndThink(); //1초 서있다가 다음 동작을 결정
        }
    }

    protected virtual void StandAndThink()
    {
        Walk(0);
        Invoke("Think", 1.0f);
    }

    protected virtual void Think()
    {
        //몬스터가 스스로 생각해서 판단 (-1:왼쪽이동 ,1:오른쪽 이동 ,0:멈춤  으로 3가지 행동을 판단)
        //Set Next Active
        //Random.Range : 최소<= 난수 <최대 /범위의 랜덤 수를 생성(최대는 제외이므로 주의해야함)
        int walkSpeed = Random.Range(0, 5) - 2;
        Walk(walkSpeed > 0 ? 1 : walkSpeed < 0 ? -1 : 0);

        //Recursive (재귀함수는 가장 아래에 쓰는게 기본적) 
        float time = Random.Range(2f, 5f); //생각하는 시간을 랜덤으로 부여 
        //Think(); : 재귀함수 : 딜레이를 쓰지 않으면 CPU과부화 되므로 재귀함수쓸 때는 항상 주의 ->Think()를 직접 호출하는 대신 Invoke()사용
        Invoke("Think", time); //매개변수로 받은 함수를 time초의 딜레이를 부여하여 재실행 
    }

    protected virtual void Walk(int walkSpeed)
    {
        enemyBase.movingDirection = walkSpeed;
    }

    public virtual void OnMove(Rigidbody2D rigid, float direction)
    {
        rigid.velocity = Vector2.right * direction;
    }

    public virtual void OnTurn()
    {
        Walk(-1 * enemyBase.movingDirection);

        if(false) //방향만 틀도록 하고 다른 부분은 그냥 유지토록
        {
            //CancelInvoke(); //think 예약을 취소한다 -> 계속 걷도록 하고
            //Invoke("Think", 2); //2초 뒤에 결정토록 한다
        }
    }

    public virtual void InSightOfPlayer()
    {
        CancelInvoke(); // 계속 걷도록 한다
    }

    public virtual void OutSightOfPlayer()
    {
        Invoke("StandAndThink", .5f); // 현재 걷기를 0.5초 더 진행한 후에 1초 서있다가 다음 동작을 결정
    }

    public virtual void OnKnockBack(float direction, float damage)
    {

    }
}
