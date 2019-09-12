using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinArcherChase : StateMachineBehaviour
{
    private Transform playerPos;
    public float speed;


    EnemyHealth health;
    bool stunned = false;
    public bool facingLeft;
    GameObject sprite;
    float move;
    Rigidbody2D rigidBody;

    public float disengageDistanceX; // The distance on the x axis before the enemy will stop chasing
	public float disengageDistanceY; // The distance on the y axis before the enemy will stop chasing

    public float attackRange;
    Transform attackPos;
    EnemyAttack enemyAttack;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        sprite = animator.gameObject;
        rigidBody = animator.gameObject.GetComponentInParent<Rigidbody2D>();
        health = animator.gameObject.GetComponentInParent<EnemyHealth>();
        enemyAttack = animator.gameObject.GetComponent<EnemyAttack>();
        attackPos = animator.gameObject.transform.GetChild(0);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        stunned = health.stunned;
        facingLeft = health.facingLeft;
        move = health.move;

        Vector2 difference = animator.transform.parent.transform.position - playerPos.position;
		if (Mathf.Abs(difference.x) > disengageDistanceX) 
		{
			animator.SetBool("isChasing", false);
		}
		if (Mathf.Abs(difference.y) > disengageDistanceY) 
		{
			animator.SetBool("isChasing", false);
		}

        if (!stunned) 
        {

            if (Vector2.Distance(attackPos.transform.position, playerPos.position) > attackRange) 
            {
                animator.transform.parent.transform.position = Vector2.MoveTowards(animator.transform.parent.transform.position, playerPos.position, speed * Time.deltaTime);	
                animator.SetBool("isInRange", false);					
            } else if (Vector2.Distance(attackPos.transform.position, playerPos.position) < attackRange) 
            {
                animator.SetBool("isInRange", true);
            } 

            if (Vector2.Distance(attackPos.transform.position, playerPos.position) < attackRange && enemyAttack.attackTimer <= 0) 
            {
                animator.SetBool("isAttacking", true);
                rigidBody.velocity = new Vector2(0,0);
            }

            // Always face the player
            if (health.playerToLeft() && !facingLeft) 
            {
                flip();
            }
            if (!health.playerToLeft() && facingLeft) 
            {
                flip();
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
    void flip() 
	{
		health.facingLeft = !health.facingLeft;
		Vector3 scale = sprite.transform.localScale;
		scale.x *= -1;
		sprite.transform.localScale = scale;
	}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
