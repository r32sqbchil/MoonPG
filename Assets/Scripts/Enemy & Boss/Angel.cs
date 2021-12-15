using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel : MonoBehaviour, IEnemyBody
{
    private EnemyBase enemyBase;

    // Start is called before the first frame update
    void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
        enemyBase.SetIEnemyBody(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTurn()
    {
    }

    public void InSightOfPlayer()
    {
    }

    public void OutSightOfPlayer()
    {
    }

    public void OnKnockBack(float direction, float damage)
    {
    }
}
