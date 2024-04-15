using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBlockState : BossStates
{
    
    public BossBlockState(Boss _boss, BossStateMachine _stateMachine, string _animBoolName) : base(_boss, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if(!startBlock) 
        {
            startBlock = true;
            blockCounter = Random.Range(1, 6);
        }
        AudioManager.instance.PlaySfx(10);
        //else if(blockCounter < 0)
        //{
        //    startBlock = false;
        //    blockCounter = 0;
        //}

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        FaceToPlayer();
        rb.velocity = Vector3.zero;
        if (finishAnim)
        { 
            if(blockCounter > 0) 
            { 
                blockCounter--;
                stateMachine.ChangeState(boss.idleState);
            }
            else
            {
                startBlock = false;
                stateMachine.ChangeState(boss.attackState);
            }
        }
    }
}
