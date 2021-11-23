using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comboattack : MonoBehaviour
{
    public Animator AttackAnim;
    public bool combocheck;
    public int combostep;

    public GameObject HitBox1;
    public GameObject HitBox2;
    public Transform Player;

    public bool leftlooking;
    public bool righrlooking;



    public void Attack()
    {
        if(combostep == 0)
        {
            AttackAnim.Play("playerattackA");
            combostep = 1;
            return;
        }
        if(combostep != 0)
        {
            if(combocheck)
            {
                combocheck = false;
                combostep += 1;
            }
        }
    }

    public void ComboCheck()
    {
        combocheck = true;
    }

    public void Combo()
    {
        if(combostep == 2)
        {
            AttackAnim.Play("playerattackB");
        }

        if(combostep == 3)
        {
            AttackAnim.Play("PlayerAttackC");
        }
    }

    public void LeftLook()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            righrlooking = false;
            leftlooking = true;
        }
    }

    public void RightLook()
    {
            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                righrlooking = true;
                leftlooking =false;
            }
    }

    public void ComboReset()
    {
        combocheck = false;
        combostep = 0;
    }

    void Update()
    {

        LeftLook();
        RightLook();

        if(Input.GetKey(KeyCode.X))
        {
            Attack();
            if(leftlooking == true)
            {
                HitBox2.SetActive(false);
                HitBox1.SetActive(true);
            }

            else if(righrlooking == true)
            {
                HitBox1.SetActive(false);
                HitBox2.SetActive(true);
            }
        }

        else
        {
                HitBox1.SetActive(false);
                HitBox2.SetActive(false); 
        }
        // else if(Input.GetKey(KeyCode.X) && righrlooking == true)
        // {
        //     Attack();
        //     HitBox2.SetActive(true);
        //     HitBox1.SetActive(false);
        // }
        // else
        // {
        //     HitBox1.SetActive(false);
        //     HitBox2.SetActive(false);
        // }
    }
}
