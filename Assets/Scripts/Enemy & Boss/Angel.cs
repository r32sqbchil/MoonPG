using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel : EnemyMove
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    protected override float GetStandingTimeOnStart()
    {
        return 5.0f;
    }

    protected override bool IsAttackOn()
    {
        return animator.GetBool("isAttack");
    }

    protected override void SetAttackOn(bool attackOn)
    {
        animator.SetBool("isAttack", attackOn);
    }

    void Start()
    {
        base.Initialize();

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        AttackPlayerIfTouch();
    }

    protected override void Walk(int walkSpeed)
    {
        base.Walk(walkSpeed);

        //Sprite Animation
        //WalkSpeed변수를 nextMove로 초기화 
        if (animator) animator.SetInteger("WalkSpeed", walkSpeed);

        //Flip Sprite
        if (walkSpeed != 0) //서있을 때 굳이 방향을 바꿀 필요가 없음 
            spriteRenderer.flipX = walkSpeed == 1; //nextmove 가 1이면 방향을 반대로 변경
    }

    public override void OnKnockBack(float direction, float damage)
    {
        base.OnKnockBack(direction, damage);
        //animator.Play("HitEnemyGoblin");
    }

    public override void OnDied()
    {
        base.OnDied();
        animator.Play("DeathEnemyGoblin");
    }
}
