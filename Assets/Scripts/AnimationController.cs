using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Player player;
    void Start()
    {
        player = Player.Instance.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void SetFinishAnim()
    {
        player.SetFinishAnim();
    }
    private void FootStepSFX()
    {
        AudioManager.instance.PlaySfx(0);
    }
    private void AddSoundAttack1()
    {
        AudioManager.instance.PlaySfx(5);
    }
    private void AddSoundAttack2()
    {
        AudioManager.instance.PlaySfx(6);
    }
    private void AddSoundAttack3()
    {
        AudioManager.instance.PlaySfx(7);
    }
    private void AddSoundAttack4()
    {
        AudioManager.instance.PlaySfx(8);
    }
    private void AttackEnemy()
    {
        Boss.instance.canBlock = false;
        player.playerCombat.AttackEnemy();
    }
    private void SetBossCanBlock()
    {
        Boss.instance.canBlock = true;
    }
}
