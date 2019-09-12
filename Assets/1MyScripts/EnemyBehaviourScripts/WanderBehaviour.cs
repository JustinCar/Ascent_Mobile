using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderBehaviour : StateMachineBehaviour
{

    private Transform playerPos;
    public float aggroDistance;
    public float speed;

    public float timeToSpendWanderingLowerBound;
    public float timeToSpendWanderingUpperBound;
    private float timer = 0.0f;
    private float timeToSpendWandering = 0.0f;

    Vector2 goal;
    Vector2 originalPosition;
    public float offset;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0.0f;
        timeToSpendWandering = Random.Range(timeToSpendWanderingLowerBound, timeToSpendWanderingUpperBound);
        originalPosition = animator.transform.position;
        goal.x = Random.Range(originalPosition.x - offset, originalPosition.x + offset);
        goal.y = Random.Range(originalPosition.y - offset, originalPosition.y + offset); 
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;

        if (Vector2.Distance(animator.transform.position, playerPos.transform.position) < aggroDistance) 
        {
            animator.SetBool("isChasing", true);
        }

        if (Vector2.Distance(animator.
        transform.position, goal) > 0.2f) 
        {
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, goal, speed * Time.deltaTime);
        }
        else 
        {
            goal.x = Random.Range(originalPosition.x - offset, originalPosition.x + offset);
            goal.y = Random.Range(originalPosition.y - offset, originalPosition.y + offset); 
        }

        if (timer > timeToSpendWandering) 
        {
            animator.SetBool("isWandering", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
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
