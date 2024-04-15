using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerStates
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SwitchColliderOnJumping();
        rb.AddForce(new Vector3(0f, player.jumpForce, 0f), ForceMode.Impulse);
        AudioManager.instance.PlaySfx(2);
    }

    public override void Exit()
    {
        base.Exit();
        player.SwitchColliderOnJumping();
    }

    public override void Update()
    {
        base.Update();

        if (finishAnim && (horizontalInput == 0 && verticalInput == 0))
        {
            AudioManager.instance.PlaySfx(3);
            stateMachine.ChangeState(player.idleState);
        }
        else if (finishAnim)
        {
            AudioManager.instance.PlaySfx(3);
            stateMachine.ChangeState(player.moveState);
        }

    }
}
