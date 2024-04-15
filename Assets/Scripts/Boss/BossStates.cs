using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStates
{
    private string animBoolName;
    protected Boss boss;
    protected BossStateMachine stateMachine;
    protected float stateTimer;
    protected Vector3 playerPos;
    protected Rigidbody rb;
    protected bool finishAnim;
    protected float turnSmoothVelocity = 0;
    protected float turnSmoothTime = 0.05f;

    protected int moveNum = 0;
    protected bool startMove = false;

    protected bool startAttack = false;
    protected int attackCounter;

    protected bool startBlock = false;
    protected int blockCounter = 0;
    public BossStates(Boss _boss, BossStateMachine _stateMachine, string _animBoolName)
    {
        this.boss = _boss;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;   
    }
    public virtual void Enter()
    {
        boss.anim.SetBool(animBoolName, true);
        rb = boss.rb;
        playerPos = Player.Instance.transform.position;
        finishAnim = false;
    }
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        playerPos = Player.Instance.transform.position;
    }
    public virtual void Exit()
    {
        boss.anim.SetBool(animBoolName, false);
        rb.velocity = Vector3.zero;
    }
    protected void FaceToPlayer()
    {
        float targetAngle = Mathf.Atan2(playerPos.x - boss.transform.position.x, playerPos.z - boss.transform.position.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(boss.transform.rotation.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        boss.transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }
    public void SetFinishAnim() => finishAnim = true;
}
