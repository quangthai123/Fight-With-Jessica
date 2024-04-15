using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockState : PlayerStates
{
    private Vector3 bossPos;
    public PlayerBlockState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();
        player.blocked = false;
        AudioManager.instance.PlaySfx(4);
        bossPos = Boss.instance.transform.position;
        if (GameManager.instance.startBossPhase)
        {
            float angle = Mathf.Atan2(bossPos.x - player.transform.position.x, bossPos.z - player.transform.position.z) * Mathf.Rad2Deg;
            player.transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        rb.velocity = Vector3.zero;
        if (finishAnim)
            stateMachine.ChangeState(player.idleState);
        if (player.blocked)
            stateMachine.ChangeState(player.blockImpactState);
    }
}
