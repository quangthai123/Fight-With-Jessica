using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtState : PlayerStates
{
    public PlayerHurtState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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
        if (finishAnim)
            stateMachine.ChangeState(player.idleState);
    }
    public override void ChangeStateByInput()
    {
        base.ChangeStateByInput();
        if (Input.GetKeyDown(KeyCode.LeftShift) && player.dashTimer <= 0f)
        {
            stateMachine.ChangeState(player.dashState);
        }
    }

}
