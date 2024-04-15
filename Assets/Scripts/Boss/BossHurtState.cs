using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHurtState : BossStates
{
    public BossHurtState(Boss _boss, BossStateMachine _stateMachine, string _animBoolName) : base(_boss, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = Random.Range(boss.hurtMinTime, boss.hurnMaxTime);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        FaceToPlayer();
        if (stateTimer < 0f)
            stateMachine.ChangeState(boss.runBackwardState);
    }
}
