using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : StateMachineBehaviour
{
    private Transform playerPos;
    public float aggroDistance;
    public float timeToSpendIdleLowerBound;
    public float timeToSpendIdleUpperBound;
    private float timer = 0.0f;
    private float timeToSpendIdle = 0.0f;




    EnemyHealth health;
    bool stunned = false;
    public bool facingLeft;
    LayerMask mask;
    public float rayDistance;
    GameObject sprite;
    public bool isSpellCaster;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0.0f;
        timeToSpendIdle = Random.Range(timeToSpendIdleLowerBound, timeToSpendIdleUpperBound);
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        mask = LayerMask.GetMask("Player");
        sprite = animator.gameObject;
        health = animator.gameObject.GetComponentInParent<EnemyHealth>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;        
        
        if (timer > timeToSpendIdle) 
        {
            animator.SetBool("isPatrolling", true);
        }

        if (health.isDead) 
		{
			animator.SetBool("isDead", true);
		}
		
		stunned = health.stunned;
        facingLeft = health.facingLeft;

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


    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
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