using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Stat : MonoBehaviour
{
    [SerializeField] Text textHp;
    [SerializeField] Text textMp;

    static int MaxHP = 50;
    static int MaxMP = 50;

    int curruntHp = 50;
    int curruntMp = 50;

    void StartStat()
    {
        textHp.text = curruntHp.ToString();
        textMp.text = curruntMp.ToString();
    }

    private void Start()
    {
        StartStat();
    }
    public void StatManager(int hp, int mp)
    {
        if( curruntHp <= MaxHP)
        {
            curruntHp += hp;
            if (curruntHp > MaxHP) curruntHp = MaxHP;
        }
        if (curruntMp <= MaxMP)
        {
            curruntMp += mp;
            if (curruntMp > MaxMP) curruntMp = MaxMP;
        }
        HpMpText();
    }
    void HpMpText()
    {
        textHp.text = curruntHp.ToString();
        textMp.text = curruntMp.ToString();
    }
}