using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : StateMachineBehaviour
{

    private Transform playerPos;
    public float aggroDistance;
    public float speed;

    public float timeToSpendWanderingLowerBound;
    public float timeToSpendWanderingUpperBound;
    private float timer = 0.0f;
    private float timeToSpendWandering = 0.0f;

    EnemyHealth health;
    bool stunned = false;
    public bool facingLeft;
    int enemyLayer = 8;
    int mask;
    public float rayDistance;
    GameObject sprite;
    public bool isSpellCaster;
    float move;
    Rigidbody2D rigidBody;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0.0f;
        timeToSpendWandering = Random.Range(timeToSpendWanderingLowerBound, timeToSpendWanderingUpperBound);
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        mask = ~(1 << enemyLayer); //Exclude enemy layer
        sprite = animator.gameObject;
        rigidBody = animator.gameObject.GetComponentInParent<Rigidbody2D>();
        health = animator.gameObject.GetComponentInParent<EnemyHealth>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        timer += Time.deltaTime;   

        if (timer > timeToSpendWandering) 
        {
            animator.SetBool("isPatrolling", false);
        }

        if (health.isDead) 
		{
			animator.SetBool("isDead", true);
		}
		
		stunned = health.stunned;
        facingLeft = health.facingLeft;
        move = health.move;

		// Enemy is aware of player if attacked
		if (stunned) 
		{
			animator.SetBool("isChasing", true);
		}
        
        if (isSpellCaster && Vector2.Distance(animator.transform.parent.transform.position, playerPos.transform.position) < aggroDistance) 
        {
            animator.SetBool("isChasing", true);
        }

        RaycastHit2D hit;

        if (facingLeft) 
			{
				hit = Physics2D.Raycast(animator.transform.parent.transform.position, -Vector2.right, rayDistance, mask);
				if (hit) 
				{
					if (hit.collider.gameObject.tag == "Player") 
					{
						Debug.Log("Player seen");
						Debug.DrawRay(animator.transform.parent.transform.position, new Vector2(-rayDistance, 0), Color.green);

						animator.SetBool("isChasing", true);
					}
				}
		}
		else
		{
				hit = Physics2D.Raycast(animator.transform.parent.transform.position, Vector2.right, rayDistance, mask);
							
				if (hit) 
				{
					if (hit.collider.gameObject.tag == "Player") 
					{
						Debug.Log("Player seen");
						Debug.DrawRay(animator.transform.parent.transform.position, new Vector2(rayDistance, 0), Color.green);

						animator.SetBool("isChasing", true);
					}
				}
		}

        // // Occasionally change direction
		// if (Random.Range(1, 200) <= 1) 
		// {
		// 	if (move == -1) 
		// 	{
		// 		health.move = 1;
		// 	} else if (move == 1) 
		// 	{
		// 		health.move = -1;
		// 	}
        //     flip();
		// }

        if (move > 0 && facingLeft) 
		{
			flip();
		}
		else if (move < 0 && !facingLeft) 
		{
			flip();
		}	

        rigidBody.velocity = new Vector2 (speed * move, rigidBody.velocity.y);

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
