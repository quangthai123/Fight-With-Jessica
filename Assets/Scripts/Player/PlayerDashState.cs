using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerStates
{
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashDuration;
        player.dashTimer = player.dashCooldown;
        AudioManager.instance.PlaySfx(1);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer > 0)
        {
            if(horizontalInput == 0 && verticalInput == 0)
                rb.velocity = player.dashSpeed * player.transform.forward;
            else
            {
                float targetAngle = Mathf.Atan2(orientation.x, orientation.z) * Mathf.Rad2Deg + player.cameraTransform.eulerAngles.y;
                player.transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
                rb.velocity = player.dashSpeed * player.transform.forward;
            }
        }
        else
            stateMachine.ChangeState(player.idleState);
    }
}
