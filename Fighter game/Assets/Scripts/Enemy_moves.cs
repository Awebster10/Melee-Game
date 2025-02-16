using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_moves : StateMachineBehaviour
{
    public float speed = 2.5f;
    public float AttackRange = 3;

    Transform Player;
    Rigidbody2D rb;
    Enemy enemy;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        enemy = animator.GetComponent<Enemy>();
    }
     //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.LookAtPlayer();

        Vector2 target = new Vector2(Player.position.x, rb.position.y);
        Vector2 NewPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(NewPos);
        
        if(Vector2.Distance(Player.position, rb.position) <= AttackRange)
        {
            animator.SetTrigger("Attack");  
        }
    }
     //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }


}
