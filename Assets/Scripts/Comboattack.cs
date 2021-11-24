using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comboattack : MonoBehaviour
{
    public Animator attackAnim;
    public bool comboCheck;
    public int comboStep;

    // public GameObject leftHitBox;
    // public GameObject rightHitBox;
    public Transform player;

    // public float direction;

    public void Attack()
    {
        if(comboStep == 0)
        {
            comboStep=1;
            attackAnim.Play("PlayerAttackA");
            return;
        }
        else
        {
            if(comboCheck)
            {
                comboStep += 1; //next step
                comboCheck = false;
            }
        }
    }

    public void OnComboCheck()
    {
        comboCheck = true;
    }

    public void OnCombo()
    {
        if(comboCheck){
            //Debug.Log("OnCombo - comboCheck:true, not yet run attack-procedure");
            comboStep += 1; //next step
            comboCheck = false;
        }
    
        if(comboStep == 2)
        {
            attackAnim.Play("PlayerAttackB");
        } else if(comboStep == 3)
        {
            attackAnim.Play("PlayerAttackC");
        }
    }

    public void OnComboReset()
    {
        comboCheck = false;
        comboStep = 0;
    }

    public void ConfirmLeftSide()
    {
        // if(Input.GetKeyDown(KeyCode.LeftArrow))
        // {
        //     direction = -1.0f;
        // }
    }

    public void ConfirmRightSide()
    {
        // if(Input.GetKeyDown(KeyCode.RightArrow))
        // {
        //     direction = 1.0f;
        // }
    }

    void Update()
    {
        ConfirmLeftSide();
        ConfirmRightSide();

        // leftHitBox.SetActive(false);
        // rightHitBox.SetActive(false);

        if(Input.GetKey(KeyCode.X))
        {
            // leftHitBox.SetActive(direction<0);
            // rightHitBox.SetActive(direction>0);

            Attack();
        }
    }
}
