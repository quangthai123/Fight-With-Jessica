using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveState : BossStates
{
    private int rdMoveDir;
    public BossMoveState(Boss _boss, BossStateMachine _stateMachine, string _animBoolName) : base(_boss, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        if(!startMove)
        {
            moveNum = Random.Range(1, 4);
            startMove = true;
        }
        rdMoveDir = Random.Range(0, 2);
        if (rdMoveDir == 0)
            boss.anim.SetInteger("MoveDir", 0);
        else
            boss.anim.SetInteger("MoveDir", 1);
        stateTimer = Random.Range(boss.idleTimeMin, boss.idleTimeMax);
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        FaceToPlayer();
        if (Vector3.Distance(playerPos, boss.transform.position) > 15f)
        {
            startMove = false;
            stateMachine.ChangeState(boss.runForwardState);
        }
        if (Vector3.Distance(playerPos, boss.transform.position) <= 3f)
        {
            startMove = false;
            stateMachine.ChangeState(boss.attackState);
        }
        else if (stateTimer > 0f)
        {
            if (rdMoveDir == 0)
                boss.rb.velocity = boss.moveSpeed * boss.transform.right;
            else
                boss.rb.velocity = boss.moveSpeed * -boss.transform.right;
        } else
        {
            if (moveNum > 0)
            {
                moveNum--;
                stateMachine.ChangeState(boss.moveState);
            }
            else
            {
                startMove = false;
                stateMachine.ChangeState(boss.attackState);
            }
        }
    }
}
