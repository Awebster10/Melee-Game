using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int EnemyAttackDamage;
    public Vector3 AttackOffset;
    public float attackRange = 1f;
    public LayerMask attackmask;
    public HeroKnight heroKnight;
    public Animator EnemyAnimator;
    public Animator PlayerAnimator;
    public Transform Player;
    public Transform Enemy;
    public Timer timer;
    public AudioSource Block;
    
    void Start()
    {
        EnemyAttackDamage = SettingsMenu.DifficultyLevel * 10;
    }
    public void Attack()
    {
        if(heroKnight.IsDead == false && heroKnight.PlayerSafe == false && heroKnight.Blocking == false)
        {
            Vector3 pos = transform.position;
            pos += transform.right * AttackOffset.x;
            pos += transform.up * AttackOffset.y;

            Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackmask);
            if(colInfo != null)
            {

                heroKnight.GetComponent<HeroKnight>().DamageTaken(EnemyAttackDamage);
            }
        }
        else if(heroKnight.IsDead == true)
        {
            EnemyAnimator.SetBool("PlayerDead", true);
            GetComponent<Animator>().enabled = false;
        }
        if(heroKnight.Blocking == true && Vector2.Distance(Player.position, Enemy.position) <= 3f)
        {
            PlayerAnimator.SetTrigger("AttackBlocked");
            Block.Play();
            heroKnight.Blocking = false;
        }
    }
}
