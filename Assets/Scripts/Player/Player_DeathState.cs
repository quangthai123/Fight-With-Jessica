using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DeathState : PlayerStates
{
    public Player_DeathState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.isDead = true;
        AudioManager.instance.playBgm = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        rb.velocity = Vector3.zero;
        if(finishAnim)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            GameManager.instance.endScreen.SetActive(true);
            player.enabled = false;
        }
    }
}
