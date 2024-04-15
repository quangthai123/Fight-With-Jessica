using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockImpactState : PlayerStates
{
    private Vector3 bossPos;
    public PlayerBlockImpactState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        bossPos = Boss.instance.transform.position;
        if (GameManager.instance.startBossPhase)
        {
            float angle = Mathf.Atan2(bossPos.x - player.transform.position.x, bossPos.z - player.transform.position.z) * Mathf.Rad2Deg;
            player.transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
        AudioManager.instance.PlaySfx(10);
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
    }
}
