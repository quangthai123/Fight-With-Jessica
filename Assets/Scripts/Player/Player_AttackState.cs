using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AttackState : PlayerStates
{
    public int attackCounter = 0;
    private float beforeAttackEndTime;
    private Vector3 bossPos;
    public Player_AttackState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        if(attackCounter > 3 || Time.time - beforeAttackEndTime > player.playerCombat.allowComboTime)
            attackCounter = 0;
        base.Enter();
        player.anim.SetInteger("AttackCount", attackCounter);
        bossPos = Boss.instance.transform.position;
        if (GameManager.instance.startBossPhase)
        {
            float angle = Mathf.Atan2(bossPos.x - player.transform.position.x, bossPos.z-player.transform.position.z) * Mathf.Rad2Deg;
            player.transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }

    public override void Exit()
    {
        base.Exit();
        attackCounter++;
        beforeAttackEndTime = Time.time;
    }

    public override void Update()
    {
        base.Update();
        if (finishAnim)
            stateMachine.ChangeState(player.idleState);
        rb.velocity = Vector3.zero;
    }
    public override void ChangeStateByInput()
    {
        base.ChangeStateByInput();
        if (Input.GetMouseButtonDown(1))
        {
            attackCounter = 0;
            stateMachine.ChangeState(player.blockState);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && player.dashTimer <= 0f)
        {
            attackCounter = 0;
            stateMachine.ChangeState(player.dashState);
        }
    }
}
