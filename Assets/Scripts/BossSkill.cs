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

    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(SkillBundle());
    }

    IEnumerator LanceSkill()
    {
        yield return new WaitForSeconds(2f);

        PlayerMove playerMove = GameObject.FindObjectOfType<PlayerMove>();
        lanceTrans.position = playerMove.transform.position;
        anim.Play("BossSkillB");

        yield return new WaitForSeconds(2f);
        
        Lance.SetActive(true);

        RaycastHit2D rayHit = Physics2D.Raycast(lanceRigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider != null)
        {
            if (rayHit.distance < 0.5f)
            {
                Lance.SetActive(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Lance.SetActive(false);
            // 데미지
        }
        else if(other.tag == "Platform")
        {
            Lance.SetActive(false);
        }
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
