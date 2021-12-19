using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;// 최대속도 설정
    public float jumpPower; // 점프파워 
    Rigidbody2D rigid;
    CapsuleCollider2D col;
    SpriteRenderer spriteRenderer;
    Animator anim;
    public float runspeed;


    float currentDirection = 0;
    float lastDirection = 0;
    float waitingTime = 0;

    GameObject scanObject;
    public float detect_range = 1.5f;
    
    
    public GameObject leftAttackBox;
    public GameObject rightAttackBox;

    private GameManager gameManager;
    private ComboAttack comboAttack;

    bool isDash = false;
    float dashSpeed = 10f;
    bool isSkillA = false;
    bool isSkillC = false;


    // 사운드
    public AudioSource mySfx;
    public AudioClip walkSfx;
    public AudioClip runSfx;
    public AudioClip jumpSfx;
    public AudioClip dashSfx;

    CameraShake cameraShake;

    public GameObject playerUltimate;
    public Transform playerUltimateTrans;

    public GameObject[] exclamation;
    public Text text;

    Player player;
    public Animator dashAnim;
    public Animator skillAAnim;
    public Animator skillBAnim;
    public Animator skillCAnim;
    
    // public Transform dash_fx;
    // public Transform skillB_fx;
    
    BoxCollider2D takeDownBox;

    void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();

        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider2D>();
        comboAttack = GetComponent<ComboAttack>();
        player = GetComponent<Player>();

        takeDownBox = GetTakeDownBox();

        cameraShake = GameObject.FindObjectOfType<CameraShake>();
    }

    BoxCollider2D GetTakeDownBox(){
        foreach(BoxCollider2D c2d in GetComponentsInChildren<BoxCollider2D>()){
            if(c2d.name == "TakeDownBox"){
                return c2d;
            }
        }
        Debug.LogWarning("Can't find TakeDownBox");
        return null;
    }

    void OnInputJump()
    {
        if(anim.GetBool("isJumping"))
        {
            // 점프 중인 상태일 때 점프를 한 번 더 할 경우는 아직 아무 조작도 하지 않는다
            return;
        }

        // 점프가 아닌 상태일 때 점프가 입력되었을 때의 처리를 해야 함

        // scanObject 가 null 이면 raycast 가 탐지를 하지 못한 상태
        // 반대로 scanObject 가 not null 이면 raycast 가 탐지한 상태
        bool hasDetectedObject = (scanObject != null);

        if(hasDetectedObject) {
            //캐릭터의 움직임을 멈춰야 함
            CharacterStopMoving();
            //게임매니저에게 처리를 위임
            gameManager.Action(scanObject);
            return;
        }

        anim.SetBool("isJumping", true);
        JumpSound();

        float thrustThreshold = 1.0f;
        bool isStandingState = !anim.GetBool("isRunning") && !anim.GetBool("isWalking");
        if(isStandingState) {
            //제자리 점프하도록 처리
            thrustThreshold = 1.0f;
        } else if (anim.GetBool("isWalking")){
            //걷는 중 점프 처리
            thrustThreshold = 1.1f;
        } else if (anim.GetBool("isRunning")){
            //달리는 중 점프 처리.. 높이는 걸을 때와 동일하게 설정함
            thrustThreshold = 1.1f;
        }
        rigid.AddForce(Vector2.up * jumpPower * thrustThreshold, ForceMode2D.Impulse);
    }

    void CharacterStartMoving() {
        if(this.lastDirection == this.currentDirection) {
            runspeed = 1.5f;
            rigid.AddForce(Vector2.right*currentDirection*1.2f, ForceMode2D.Impulse);
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", true);
        } else {
            runspeed = 0;
            rigid.AddForce(Vector2.right*currentDirection, ForceMode2D.Impulse);
            anim.SetBool("isWalking", true);
            anim.SetBool("isRunning", false);
            this.lastDirection = currentDirection;
        }
    }

    void CharacterStopMoving() {
        this.lastDirection = 0;
        rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        anim.SetBool("isWalking", false);
        anim.SetBool("isRunning", false);
    }

    void Update()
    {

        //Talk - temp       
        if(gameManager.isAction) {
            if (Input.GetButtonDown("Jump"))
            {
                gameManager.Action();
            }
            return;
        }

        if(anim.GetBool("isJumping"))
        {
            // 점프 중인 상태일 때 점프를 한 번 더 할 경우는 아직 아무 조작도 하지 않는다
        }
        //Jump
        if (Input.GetButtonDown("Jump"))
        {
            OnInputJump();
            return;
        }

        
        //Left
        if (Input.GetKeyDown(KeyCode.LeftArrow)){
            comboAttack.attackPoint = leftAttackBox.GetComponent<Transform>();
            this.currentDirection = -1.0f;
            CharacterStartMoving();
            return;
        } 
        //Right
        if (Input.GetKeyDown(KeyCode.RightArrow)){
            comboAttack.attackPoint = rightAttackBox.GetComponent<Transform>();
            this.currentDirection = 1.0f;
            CharacterStartMoving();
            return;
        }

        if(this.currentDirection != 0){
            bool doubleDownState = false;
            if(this.currentDirection == -1.0f && !Input.GetKey(KeyCode.LeftArrow)) {
                doubleDownState = true;
            } else if(this.currentDirection == 1.0f && !Input.GetKey(KeyCode.RightArrow)) {
                doubleDownState = true;
            }
            if(doubleDownState) {
                this.currentDirection = 0;
                if(anim.GetBool("isJumping")) {
                    CharacterStopMoving();
                } else {
                    this.waitingTime = 0.3f;
                }
            }
        } else if(this.lastDirection != 0){
            if(this.waitingTime > 0) {
                this.waitingTime -= Time.deltaTime;
            } else {
                CharacterStopMoving();
            }
        }

        if(Input.GetKeyDown(KeyCode.A)) {
            if(!isSkillA) {
            isSkillA = true;
            anim.Play("PlayerSkillA");
            skillAAnim.Play("PlayerSkillA_fx");
            text.gameObject.SetActive(true);
            comboAttack.SetDamageUp();
            if(player.GetHp() <= 190){
                player.IncreaseHp(10);
            }
            Invoke("SkillACool", 15f);
            }
        }

        if(Input.GetKeyDown(KeyCode.S)) {
            if(anim.GetBool("isJumping")) {
                rigid.velocity = Vector2.zero;
                anim.Play("PlayerSkillB");
                // if(currentDirection == -1)
                // {
                //     Quaternion rotation = skillB_fx.rotation;
                //     rotation.y = 0;
                //     skillB_fx.rotation = rotation;
                // }
                // else {
                //     Quaternion rotation = skillB_fx.rotation;
                //     rotation.y = 180;
                //     skillB_fx.rotation = rotation;
                // }
                skillBAnim.Play("PlayerSkillB_fx");
                rigid.AddForce(Vector2.down * 3f, ForceMode2D.Impulse);

                comboAttack.Attack(currentDirection<0?-1:1, takeDownBox.transform.position, takeDownBox.transform.localScale, 4);
            }
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            if(!isSkillC) {
                isSkillC = true;
                playerUltimateTrans.position = new Vector2(transform.position.x + currentDirection, playerUltimateTrans.position.y);
                playerUltimate.SetActive(true);
                anim.Play("PlayerSkillC");
                skillCAnim.Play("PlayerSkillC_fx");
                Invoke("EndUltimate", 5f); 
                Invoke("SkillCCool", 10f); 
            }
        }

        if (Input.GetButton("Horizontal")) {
            if(this.currentDirection != 0){
                rigid.AddForce(Vector2.right*currentDirection, ForceMode2D.Impulse);
                spriteRenderer.flipX = (currentDirection<0);
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) && !isDash)
            {
                isDash = true;
                rigid.AddForce(Vector2.right*currentDirection*dashSpeed, ForceMode2D.Impulse);
                anim.Play("PlayerDash");
                // if(currentDirection == -1)
                // {
                //     Quaternion rotation = dash_fx.rotation;
                //     rotation.y = 0;
                //     dash_fx.rotation = rotation;
                // }
                // else {
                //     Quaternion rotation = dash_fx.rotation;
                //     rotation.y = 180;
                //     dash_fx.rotation = rotation;
                // }
                dashAnim.Play("PlayerDash_fx");
                DashSound();
                Invoke("ForDelay", 0.2f);
            }
            return;
        }
    }

    void SkillACool()
    {
        isSkillA = false;
    }

    void SkillCCool()
    {
        isSkillC = false;
    }

    void EndUltimate()
    {
        playerUltimate.SetActive(false);
    }

    void FixedUpdate()
    {     
        RaycastHit2D rayHit_detect = Physics2D.Raycast(rigid.position, new Vector3(currentDirection,0,0), detect_range, LayerMask.GetMask("NPC"));

		//감지되면 scanObject에 오브젝트 저장 
        if(rayHit_detect.collider != null)
        {
            if(scanObject != rayHit_detect.collider.gameObject)
            {
                scanObject = rayHit_detect.collider.gameObject;
            }
        }
        else
        {
            scanObject = null;
        }

        // Start X Position Check
        LimitPlayerMovingArea();

        // Lending Platform
        if(rigid.velocity.y < 0)
        {
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null) // 바닥 감지를 위해서 레이저를 쏜다! 
            {
                if (rayHit.distance < 0.5f)
                {
                     anim.SetBool("isJumping", false);
                }
            }
        }

        // MaxSpeed Limit
        if (rigid.velocity.x > maxSpeed + runspeed && !isDash)// right
            rigid.velocity = new Vector2(maxSpeed + runspeed, rigid.velocity.y);
        else if (rigid.velocity.x < (maxSpeed + runspeed) * (-1) && !isDash) // Left Maxspeed
            rigid.velocity = new Vector2((maxSpeed + runspeed) * (-1), rigid.velocity.y);
        

        if(isDash) //대쉬 속도제한
        {
            if (rigid.velocity.x > maxSpeed + runspeed + dashSpeed)// right
            rigid.velocity = new Vector2(maxSpeed + runspeed + dashSpeed, rigid.velocity.y);
            else if (rigid.velocity.x < (maxSpeed + runspeed) * (-1) + dashSpeed)
            rigid.velocity = new Vector2((maxSpeed + runspeed + dashSpeed) * (-1), rigid.velocity.y);
        }
    }

    void LimitPlayerMovingArea(){
        float center = transform.position.x;
        float width = transform.localScale.x;
        float left = center - width/2;
        float right = center + width/2;
        if(left < gameManager.limitMoveXMin) {
            transform.Translate(new Vector2(left - gameManager.limitMoveXMin, 0));
            // 퀘스트 액션을 보내야 함.
            gameManager.Action(gameObject, GameManager.ACTION_ON_TOUCH_LEFT_BOUNDARY);
        } else if(right > gameManager.limitMoveXMax) {
            transform.Translate(new Vector2(right - gameManager.limitMoveXMax, 0));
            // 퀘스트 액션을 보내야 함.
            gameManager.Action(gameObject, GameManager.ACTION_ON_TOUCH_RIGHT_BOUNDARY);
        }
    }

    void ForDelay()
    {
        isDash = false;
    }

    public void WalkSound()
    {
        mySfx.PlayOneShot (walkSfx);
    }

    public void RunSound()
    {
        mySfx.PlayOneShot (runSfx);
    }

    public void JumpSound()
    {
        mySfx.PlayOneShot (jumpSfx);
    }

    public void DashSound()
    {
        mySfx.PlayOneShot (dashSfx);
    }
}