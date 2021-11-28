using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : MonoBehaviour
{
    public Transform lanceTrans;
    public Transform playerTrans;
    public GameObject Lance;
    public GameObject Lightning;
    public Rigidbody2D lanceRigid;
    Animator anim;
    public GameObject Boss;

    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(SkillBundle());
        if(Boss.activeSelf == false)
        {
            StopCoroutine(SkillBundle());
            Lightning.SetActive(false);
        }    
    }

    IEnumerator LanceSkill()
    {
        yield return new WaitForSeconds(2f);

        PlayerMove playerMove = GameObject.FindObjectOfType<PlayerMove>();
        lanceTrans.position = playerMove.transform.position;
        anim.Play("BossSkillB");

        yield return new WaitForSeconds(2f);
        
        Lance.SetActive(true);

        yield return new WaitForSeconds(2f);

        Lance.SetActive(false);
    }



    IEnumerator LightningSkill()
    {
        anim.Play("BossSkillC");

        yield return new WaitForSeconds(3f);
        
        Lightning.SetActive(true);
        // 데미지

        yield return new WaitForSeconds(1f);
        Lightning.SetActive(false);
    }

    IEnumerator SkillBundle()
    {
        while(true) {
            StartCoroutine(LanceSkill());
            StopCoroutine(LanceSkill());
            yield return new WaitForSeconds(3f);
            StartCoroutine(LightningSkill());
            StopCoroutine(LightningSkill());
            yield return new WaitForSeconds(3f);
        }
    }
}
