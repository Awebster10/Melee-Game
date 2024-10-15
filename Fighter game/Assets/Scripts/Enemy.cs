using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int MaxHealth;
    public float speed = 2.5f;
    int CurrentHealth;

    public Animator EnemyAnimator;
    public Transform player;
    
	Rigidbody2D rb;
	public bool isFlipped = true;
    public EnemyCount enemyCount;

    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = SettingsMenu.DifficultyLevel * 100;
        CurrentHealth = MaxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
		rb = EnemyAnimator.GetComponent<Rigidbody2D>();
    }
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        EnemyAnimator.SetTrigger("Hurt");

        if(CurrentHealth <=0)
        {
            Die();
        }
    }
    void Die()
    {
        EnemyAnimator.SetBool("Died", true);
        GetComponent<Collider2D>().enabled = false;
        rb.gravityScale = 0;
        this.enabled = false;
        enemyCount.DeadEnemies += 1;
        enemyCount.CheckEnemies();
    }
    public void LookAtPlayer()
	{

		if (transform.position.x < player.position.x && isFlipped == false)
		{
			GetComponent<SpriteRenderer>().flipX = true;
            isFlipped = true;
		}
		else if (transform.position.x > player.position.x && isFlipped == true)
		{
			GetComponent<SpriteRenderer>().flipX = false;
			isFlipped = false;
		}
	}

}
