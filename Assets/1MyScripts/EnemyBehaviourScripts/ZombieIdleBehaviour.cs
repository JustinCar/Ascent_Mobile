using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieIdleBehaviour : StateMachineBehaviour
{
    private Transform playerPos;
    public float aggroDistance;
    public float timeToSpendIdleLowerBound;
    public float timeToSpendIdleUpperBound;
    private float timer = 0.0f;
    private float timeToSpendIdle = 0.0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0.0f;
        timeToSpendIdle = Random.Range(timeToSpendIdleLowerBound, timeToSpendIdleUpperBound);
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        
        if (Vector2.Distance(animator.transform.position, playerPos.transform.position) < aggroDistance) 
        {
            animator.SetBool("isChasing", true);
        }

        if (timer > timeToSpendIdle) 
        {
            animator.SetBool("isWandering", true);
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
