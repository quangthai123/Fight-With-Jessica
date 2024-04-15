using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationController : MonoBehaviour
{
    private void SetFinishAnimOnModel()
    {
        Boss.instance.SetFinishAnim();
    }
    private void SetCanHurt()
    {
        Boss.instance.canHurt = true;
    }
    private void AttackPlayer()
    {
        Boss.instance.AttackPlayer();
    }
}
