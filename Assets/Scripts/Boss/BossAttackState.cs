using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : BossStates
{
    public int attackRd;
    public BossAttackState(Boss _boss, BossStateMachine _stateMachine, string _animBoolName) : base(_boss, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        rb = boss.rb;
        playerPos = Player.Instance.transform.position;
        finishAnim = false;
        if (!startAttack)
        {
            startAttack = true;
            attackCounter = Random.Range(1, 9);
            Debug.Log("Start Attack!");
        }
        int oldAttack = attackRd;
        while(oldAttack == attackRd )
        {
            attackRd = Random.Range(1, 7);
        }
        boss.anim.SetInteger("AttackRd", attackRd);
        base.Enter();
        FaceToPlayer();
        if (Vector3.Distance(playerPos, boss.transform.position) > 3f)
            stateMachine.ChangeState(boss.runForwardState);
    }

    public override void Exit()
    {
        boss.canHurt = false;
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        rb.velocity = Vector3.zero;
        if (finishAnim)
        {
            if (attackCounter > 0)
            {
                attackCounter--;
                stateMachine.ChangeState(boss.attackState);
            }
            else
            {
                Debug.Log("End Attack!");
                startAttack = false;
                stateMachine.ChangeState(boss.idleState);
            }
        }
    }
}
