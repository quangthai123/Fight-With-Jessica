using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Player player;
    public float allowComboTime = 0.2f;
    [SerializeField] private Transform attackPointPos;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask enemyLayer;
    private RaycastHit hitEnemy;

    void Start()
    {
        player = GetComponent<Player>();
        attackPointPos = transform.Find("Attack Point Pos");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AttackEnemy()
    {
        Collider[] hit = Physics.OverlapSphere(attackPointPos.position, attackRadius, enemyLayer);
        //bool hit = Physics.SphereCast(attackPointPos.position, attackRadius, transform.forward, out hitEnemy, 0f, enemyLayer);
        foreach(Collider col in hit)
        {
            Boss boss = Boss.instance;
            if (boss.stateMachine.currentState != boss.blockState)
            {
                if (player.attackState.attackCounter == 0)
                    boss.GetDamage(player.stats.damage);
                if (player.attackState.attackCounter == 1)
                    boss.GetDamage(player.stats.damage * 1.5f);
                if (player.attackState.attackCounter == 2)
                    boss.GetDamage(player.stats.damage * 2f);
                if (player.attackState.attackCounter == 3)
                    boss.GetDamage(player.stats.damage * 2.5f);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPointPos.position, attackRadius);
    }
}
