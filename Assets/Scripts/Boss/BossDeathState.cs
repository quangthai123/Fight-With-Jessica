using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathState : BossStates
{
    public BossDeathState(Boss _boss, BossStateMachine _stateMachine, string _animBoolName) : base(_boss, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        boss.isDead = true;
        AudioManager.instance.playBgm = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        rb.velocity = Vector3.zero;
    }
}
