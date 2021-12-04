using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : MonoBehaviour
{
    public Transform lanceTrans;
    public Transform playerTrans;
    public Transform lanceRangeTrans;
    public GameObject lance;
    public GameObject lightning;
    public Rigidbody2D lanceRigid;
    Animator anim;
    public GameObject Boss;
    public GameObject lanceRange;
    public GameObject lightningRange;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(SkillBundle());
        if(Boss.activeSelf == false)
        {
            StopCoroutine(SkillBundle());
            lightning.SetActive(false);
        }    
    }

    IEnumerator LanceSkill()
    {
        PlayerMove playerMove = GameObject.FindObjectOfType<PlayerMove>();
        lanceTrans.position = playerMove.transform.position;
        anim.Play("BossSkillB");
        lanceRangeTrans.position = new Vector2 (playerMove.transform.position.x,lanceRangeTrans.position.y);
        lanceRange.SetActive(true);

        yield return new WaitForSeconds(2f);
        
        lance.SetActive(true);
        lanceRange.SetActive(false);

        yield return new WaitForSeconds(2f);

        lance.SetActive(false);
    }



    IEnumerator LightningSkill()
    {
        anim.Play("BossSkillC");
        lightningRange.SetActive(true);

        yield return new WaitForSeconds(3f);
        
        lightning.SetActive(true);
        lightningRange.SetActive(false);

        yield return new WaitForSeconds(1f);
        lightning.SetActive(false);
    }

    IEnumerator SkillBundle()
    {
        while(true) {
            StartCoroutine(LanceSkill());
            StopCoroutine(LanceSkill());
            yield return new WaitForSeconds(4f);
            StartCoroutine(LightningSkill());
            StopCoroutine(LightningSkill());
            yield return new WaitForSeconds(4f);
        }
    }
}
