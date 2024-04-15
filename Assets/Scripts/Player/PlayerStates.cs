using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates
{
    private string animBoolName;
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected bool finishAnim;
    protected float stateTimer;
    protected Rigidbody rb;
    protected float horizontalInput;
    protected float verticalInput;
    protected Vector3 orientation;

    public PlayerStates(Player player, PlayerStateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;
        finishAnim = false;
    }

    public virtual void Update()
    {
        ChangeStateByInput();
        stateTimer -= Time.deltaTime;
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (horizontalInput == 0 && verticalInput == 0)
            return;       
        orientation.x = horizontalInput;
        orientation.z = verticalInput;

    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }
    public void SetFinishAnim() { finishAnim = true; }
    public virtual void ChangeStateByInput()
    {

    }
}
