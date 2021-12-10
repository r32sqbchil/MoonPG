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
    public Rigidbody2D lanceRigid;
    Animator anim;
    public GameObject Boss;
    public GameObject lanceRange;
    public GameObject lightningRange;
    public SpriteRenderer lanceRangeColor;
    PlayerMove playerMove;
    public Animator lightningAnim;

    void Start()
    {
        playerMove = GameObject.FindObjectOfType<PlayerMove>();        
        anim = GetComponent<Animator>();
        StartCoroutine(SkillBundle());
        if (Boss.activeSelf == false)
        {
            StopCoroutine(SkillBundle());
            // lightning.SetActive(false);
            // lightningRange.SetActive(false);
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

        yield return new WaitForSeconds(3f);

        for(int i=0;i<6;i++)
        {
            pieceOfLight[i].SetActive(true);
        }

        yield return new WaitForSeconds(2f);

        for(int i=0;i<6;i++)
        {
            pieceOfLight[i].SetActive(false);
        }
    }

    IEnumerator SkillBundle()
    {
        while (true)
        {
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
