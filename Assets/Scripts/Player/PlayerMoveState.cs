using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerOnGroundState
{

    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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
        float targetAngle = Mathf.Atan2(orientation.x, orientation.z) * Mathf.Rad2Deg + player.cameraTransform.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(player.transform.rotation.eulerAngles.y, targetAngle, ref player.turnSmoothVelocity, player.turnSmoothTime);
        player.transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        rb.velocity = player.moveSpeed * new Vector3(moveDir.x, 0f, moveDir.z).normalized;
        if (horizontalInput == 0f && verticalInput == 0f)
            stateMachine.ChangeState(player.idleState);
    }
}
