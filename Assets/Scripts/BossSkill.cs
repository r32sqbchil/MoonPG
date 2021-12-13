using System.Collections;
using UnityEngine;

public class BossSkill : MonoBehaviour
{
    public Transform lanceTrans;
    public Transform fakeLanceTrans;
    public Transform playerTrans;
    public Transform lanceRangeTrans;
    public GameObject lance;
    public GameObject fakeLance;
    public GameObject lightning;
    public GameObject[] pieceOfLight = new GameObject[6];
    public GameObject[] pieceOfLightRange = new GameObject[6];
    public Rigidbody2D lanceRigid;
    Animator anim;
    public GameObject Boss;
    public GameObject lanceRange;
    public GameObject lightningRange;
    public SpriteRenderer lanceRangeColor;
    PlayerMove playerMove;
    public Animator lightningAnim;
    private BossHP bossHP;
    private Player player;
    bool phaseCheck7 = false;
    bool phaseCheck4 = false;


    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        playerMove = player.GetComponent<PlayerMove>();        
        bossHP = GetComponent<BossHP>();
        
        anim = GetComponent<Animator>();
        StartCoroutine(SkillPhase1());
        if (Boss.activeSelf == false)
        {
            StopCoroutine(SkillPhase3());
        }
    }

    void Update()
    {
        if(bossHP.healthBar.fillAmount < 0.7 && !phaseCheck7)
        {
            phaseCheck7 = true;
            for(int i=0;i<6;i++)
            {
                pieceOfLightRange[i].SetActive(false);
            }
            lightningRange.SetActive(false);
            StopCoroutine(SkillPhase1());
            StartCoroutine(SkillPhase2());
        }

        if(bossHP.healthBar.fillAmount < 0.4 && !phaseCheck4)
        {
            phaseCheck4 = true;

            for(int i=0;i<6;i++)
            {
                pieceOfLightRange[i].SetActive(false);
            }
            lightningRange.SetActive(false);
            
            StopCoroutine(SkillPhase2());
            StartCoroutine(SkillPhase3());
        }
    }


    IEnumerator LanceSkill()
    {
        lanceRangeColor.color = new Color(255f, 0f, 255f, 1f);
        lanceRange.SetActive(true);
        fakeLance.SetActive(true);

        PlayerMove playerMove = GameObject.FindObjectOfType<PlayerMove>();
        lanceRangeTrans.position = new Vector2(playerMove.transform.position.x, lanceRangeTrans.position.y);
        fakeLanceTrans.position = new Vector2(playerMove.transform.position.x, 1f);

        yield return new WaitForSeconds(1f);

        anim.Play("BossSkillB");
        lanceRangeTrans.position = new Vector2(playerMove.transform.position.x, lanceRangeTrans.position.y);
        fakeLanceTrans.position = new Vector2(playerMove.transform.position.x, 1f);

        yield return new WaitForSeconds(1f);

        lanceRangeColor.color = new Color(255f, 0f, 0f, 1f);
        fakeLanceTrans.position = new Vector2(playerMove.transform.position.x, 1f);
        lanceRangeTrans.position = new Vector2(playerMove.transform.position.x, lanceRangeTrans.position.y);
        lanceTrans.position = new Vector2(playerMove.transform.position.x, 1f);

        yield return new WaitForSeconds(0.15f);
        // 여기 사운드 있으면 좋을거같음
        lance.SetActive(true);
        fakeLance.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        lanceRange.SetActive(false);
        lance.SetActive(false);
    }

    IEnumerator LightningSkill()
    {
        anim.Play("BossSkillC");
        lightningRange.SetActive(true);

        yield return new WaitForSeconds(3f);

        lightning.SetActive(true);
        lightningAnim.Play("BossSkillC_Effect");
        lightningRange.SetActive(false);
        yield return new WaitForSeconds(1f); // 애니메이션 실행 시간
        lightning.SetActive(false);
    }
    IEnumerator PieceOfLightSkill()
    {
        anim.Play("BossSkillA");

        for(int i=0;i<6;i++)
        {
            pieceOfLightRange[i].SetActive(true);
        }

        yield return new WaitForSeconds(3f);

        for(int i=0;i<6;i++)
        {
            pieceOfLight[i].SetActive(true);
        }
        
        for(int i=0;i<6;i++)
        {
            pieceOfLightRange[i].SetActive(false);
        }

        yield return new WaitForSeconds(2f);

        for(int i=0;i<6;i++)
        {
            pieceOfLight[i].SetActive(false);
        }
    }

    IEnumerator SkillPhase1()
    {
        while (true)
        {
            StartCoroutine(PieceOfLightSkill());
            StopCoroutine(PieceOfLightSkill());
            yield return new WaitForSeconds(6f);
            StartCoroutine(LightningSkill());
            StopCoroutine(LightningSkill());
            yield return new WaitForSeconds(6f);
        }
    }
    IEnumerator SkillPhase2()
    {
        while (true)
        {
            StartCoroutine(PieceOfLightSkill());
            StopCoroutine(PieceOfLightSkill());
            yield return new WaitForSeconds(6f);
            StartCoroutine(LanceSkill());
            StopCoroutine(LanceSkill());
            yield return new WaitForSeconds(6f);
            StartCoroutine(LightningSkill());
            StopCoroutine(LightningSkill());
            yield return new WaitForSeconds(6f);
        }
    }
    IEnumerator SkillPhase3()
    {
        while (true)
        {
            player.enemyDamage *= 2;
            StartCoroutine(PieceOfLightSkill());
            StopCoroutine(PieceOfLightSkill());
            yield return new WaitForSeconds(4f);
            StartCoroutine(LanceSkill());
            StopCoroutine(LanceSkill());
            yield return new WaitForSeconds(4f);
            StartCoroutine(LightningSkill());
            StopCoroutine(LightningSkill());
            yield return new WaitForSeconds(4f);
        }
    }
}
