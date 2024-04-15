using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnGroundState : PlayerStates
{
    public PlayerOnGroundState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
    public override void ChangeStateByInput()
    {
        base.ChangeStateByInput();
        if (Input.GetKeyDown(KeyCode.Space) && player.CheckIsGrounded())
        {
            stateMachine.ChangeState(player.jumpState);
        }
        if(Input.GetMouseButtonDown(1)) 
        {
            stateMachine.ChangeState(player.blockState);
        }
        if(Input.GetKeyDown(KeyCode.LeftShift) && player.dashTimer <= 0f)
        {
            stateMachine.ChangeState(player.dashState);
        }
        if(Input.GetMouseButtonDown(0))
        {
            stateMachine.ChangeState(player.attackState);
        }
    }
}
