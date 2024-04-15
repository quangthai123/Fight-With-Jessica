using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public static Boss instance { get; private set; }
    public BossStateMachine stateMachine { get; private set; }
    public BossIdleState idleState { get; private set; }
    public BossMoveState moveState { get; private set; }
    public BossAttackState attackState { get; private set; }
    public BossRunFowardState runForwardState { get; private set; }
    public BossBlockState blockState { get; private set; }
    public BossHurtState hurtState { get; private set; }
    public BossRunBackwardState runBackwardState { get; private set; }
    public BossDeathState deathState { get; private set; }
    [HideInInspector] public CharacterStats bossStats { get; private set; }
    public bool canHurt = false;

    public Animator anim { get; private set; }
    public Rigidbody rb { get; private set; }
    private Vector3 playerPos;
    [SerializeField] private Transform attackPointPos;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask playerLayer;

    [Header("States Time")]
    public float idleTimeMin;
    public float idleTimeMax;

    [Space]
    public float moveSpeed;
    public float runSpeed;
    public float blockTimeMin;
    public float blockTimeMax;
    [Space]
    public float hurtMinTime;
    public float hurnMaxTime;
    [HideInInspector] public bool canBlock = false;
    public bool isDead = false;

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
        stateMachine = new BossStateMachine();
        idleState = new BossIdleState(this, stateMachine, "Idle");
        moveState = new BossMoveState(this, stateMachine, "Move");
        attackState = new BossAttackState(this, stateMachine, "IsAttacking");
        runForwardState = new BossRunFowardState(this, stateMachine, "Run");
        blockState = new BossBlockState(this, stateMachine, "Block");
        hurtState = new BossHurtState(this, stateMachine, "Hurt");
        deathState = new BossDeathState(this, stateMachine, "Death");
        runBackwardState = new BossRunBackwardState(this, stateMachine, "RunBackward");
    }
    private void Start()
    {
        anim = transform.Find("Model").GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        bossStats = GetComponent<CharacterStats>();
        playerPos = Player.Instance.transform.position;
        attackPointPos = transform.Find("Attack Point Pos");
        stateMachine.Initialize(idleState);
    }
    void Update()
    {
        stateMachine.currentState.Update();
    }
    public void AttackPlayer()
    {
        Collider[] hit = Physics.OverlapSphere(attackPointPos.position, attackRadius, playerLayer);
        if (hit.Length > 0)
        {
            Player playerRef = Player.Instance;
            if (playerRef.stateMachine.currentState != playerRef.blockState && playerRef.stateMachine.currentState != playerRef.dashState)
            {
                if (attackState.attackRd == 1 || attackState.attackRd == 2)
                    playerRef.GetDamage(bossStats.damage);
                if (attackState.attackRd == 3)
                    playerRef.GetDamage(bossStats.damage * 2f);
                else
                    playerRef.GetDamage(bossStats.damage * 1.5f);
            }
            else
                playerRef.blocked = true;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPointPos.position, attackRadius);
    }
    public void SetFinishAnim() => stateMachine.currentState.SetFinishAnim();
    public void GetDamage(float _damage)
    {
        bossStats.DeductHealth(_damage);
        if(Player.Instance.attackState.attackCounter == 2)
            AudioManager.instance.PlaySfx(11);
        else
            AudioManager.instance.PlaySfx(9);
        if (bossStats.currentHp <= 0)
            stateMachine.ChangeState(deathState);
        if (canHurt)
            stateMachine.ChangeState(hurtState);

    }
}
