using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel : IEnemyBody
{
    protected override float GetStandingTimeOnStart()
    {
        return 5.0f;
    }

    protected override bool IsAttackOn()
    {
        return false;
    }

    protected override void SetAttackOn(bool attackOn)
    {
        //do nothing
    }

    // Start is called before the first frame update
    void Start()
    {
        base.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        AttackPlayerIfTouch();
    }

}
