using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRunFowardState : BossStates
{
    public BossRunFowardState(Boss _boss, BossStateMachine _stateMachine, string _animBoolName) : base(_boss, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("chase player");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        FaceToPlayer();
        if (Vector3.Distance(playerPos, boss.transform.position) > 3f)
            rb.velocity = boss.runSpeed * boss.transform.forward;
        else
            stateMachine.ChangeState(boss.attackState);
    }
}
