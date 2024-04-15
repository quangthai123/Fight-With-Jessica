using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public PlayerCombat playerCombat { get; private set; }

    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerBlockState blockState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public Player_AttackState attackState { get; private set; }
    public PlayerHurtState hurtState { get; private set; }
    public PlayerBlockImpactState blockImpactState { get; private set; }
    public Player_DeathState deathState { get; private set; }
    #endregion
    public float moveSpeed;
    public float jumpForce;
    public float turnSmoothTime = .1f;
    [HideInInspector] public float turnSmoothVelocity = 0;
    [HideInInspector] public Transform cameraTransform;
    [HideInInspector] public CharacterStats stats { get; private set; }

    [Header("Ground Collision")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;

    [Header("Dash Infor")]
    public float dashSpeed;
    public float dashDuration;
    public float dashCooldown;
    [HideInInspector] public float dashTimer;
    [HideInInspector] public bool blocked = false;
    public bool isDead = false;

    private GameObject normalCol;
    private GameObject jumpCol;

    public Animator anim { get; private set; }
    public Rigidbody rb { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        blockState = new PlayerBlockState(this, stateMachine, "Block");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        attackState = new Player_AttackState(this, stateMachine, "IsAttacking");
        hurtState = new PlayerHurtState(this, stateMachine, "Hurt");
        blockImpactState = new PlayerBlockImpactState(this, stateMachine, "Blocked");
        deathState = new Player_DeathState(this, stateMachine, "Death");
        normalCol = transform.Find("Normal Col No Trigger").gameObject;
        jumpCol = transform.Find("Jump Col No Trigger").gameObject;
        playerCombat = GetComponent<PlayerCombat>();
        stats = GetComponent<CharacterStats>();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        rb = GetComponent<Rigidbody>();
        anim = transform.Find("Model").GetComponent<Animator>();
        cameraTransform = FindObjectOfType<Camera>().transform;
        stateMachine.Initialize(idleState);
        Physics.gravity *= 2f;
    }
    public bool CheckIsGrounded() => Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, whatIsGround);
    // Update is called once per frame
    void Update()
    {
        stateMachine.currentState.Update();
        ActionTimer();
    }
    private void ActionTimer()
    {
        if (dashTimer < 0f)
            return;
        dashTimer -= Time.deltaTime;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance, transform.position.z));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Capfire")
        {
            GameManager.instance.EText.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Capfire")
        {
            GameManager.instance.EText.SetActive(false);
        }
    }
    public void SetFinishAnim() => stateMachine.currentState.SetFinishAnim();
    public void SwitchColliderOnJumping()
    {
        normalCol.SetActive(!normalCol.activeInHierarchy);
        jumpCol.SetActive(!jumpCol.activeInHierarchy);
    }
    public void GetDamage(float _damage)
    {
        stats.DeductHealth(_damage);
        if(Boss.instance.attackState.attackRd == 1 || Boss.instance.attackState.attackRd == 2)
            AudioManager.instance.PlaySfx(11);
        else
            AudioManager.instance.PlaySfx(9);
        rb.AddForce(3f * (-Boss.instance.transform.position + transform.position).normalized, ForceMode.Impulse);
        if (stats.currentHp <= 0)
            stateMachine.ChangeState(deathState);
        else
            stateMachine.ChangeState(hurtState);
    }
}
