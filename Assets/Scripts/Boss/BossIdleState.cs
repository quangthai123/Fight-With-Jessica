using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : BossStates
{
    public BossIdleState(Boss _boss, BossStateMachine _stateMachine, string _animBoolName) : base(_boss, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = Random.Range(boss.idleTimeMin, boss.idleTimeMax);
        boss.canHurt = true;
    }

    public override void Exit()
    {
        base.Exit();
        boss.canHurt = false;
    }

    public override void Update()
    {
        base.Update();
        FaceToPlayer();
        rb.velocity = Vector3.zero;
        if (stateTimer < 0f)
            stateMachine.ChangeState(boss.moveState);
        if (Vector3.Distance(playerPos, boss.transform.position) > 15f)
            stateMachine.ChangeState(boss.runForwardState);
        if (Vector3.Distance(playerPos, boss.transform.position) <= 3f)
        {
            if(Player.Instance.stateMachine.currentState == Player.Instance.attackState && boss.canBlock)
                stateMachine.ChangeState(boss.blockState);
        }
    }
}
