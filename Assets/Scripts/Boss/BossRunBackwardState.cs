using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRunBackwardState : BossStates
{
    public BossRunBackwardState(Boss _boss, BossStateMachine _stateMachine, string _animBoolName) : base(_boss, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("dau vcc");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        FaceToPlayer();
        rb.velocity = boss.runSpeed * 3 * (-playerPos + boss.transform.position).normalized;
        if (finishAnim)
            stateMachine.ChangeState(boss.idleState);
    }
}
