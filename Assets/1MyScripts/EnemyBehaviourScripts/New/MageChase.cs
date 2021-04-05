using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageChase : StateMachineBehaviour
{
    private Transform PlayerPos;
    EnemyHealth Health;
    bool Stunned = false;
    public bool FacingLeft;
    GameObject Sprite;
    Rigidbody2D RigidBody;
    public float AggroDistance;
    public float MeleeAttackRange;
    Transform AttackPos;
    MageTeleport TeleportScript;
    public int RaycastLayer;
    int Mask;
    public float MaxTeleportDistance;
    public float TeleportCooldown;
    float TeleportTimer;
    MageAttack EnemyAttack;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
        Sprite = animator.gameObject;
        RigidBody = animator.gameObject.GetComponentInParent<Rigidbody2D>();
        Health = animator.gameObject.GetComponentInParent<EnemyHealth>();
        AttackPos = animator.gameObject.transform.GetChild(0);
        EnemyAttack = animator.gameObject.GetComponent<MageAttack>();
        TeleportScript = animator.gameObject.GetComponent<MageTeleport>();
        Mask = 1 << RaycastLayer;
        TeleportTimer = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Stunned = Health.stunned;
        FacingLeft = Health.facingLeft;
        TeleportTimer -= Time.deltaTime;

		if (Vector2.Distance(AttackPos.transform.position, PlayerPos.position) > AggroDistance) 
		{
			animator.SetBool("isChasing", false);
            return;
		}

        if (!Stunned) 
        {
            if (Vector2.Distance(AttackPos.transform.position, PlayerPos.position) < MeleeAttackRange) 
            {
                if (TeleportTimer <= 0)
                {
                    TeleportTimer = TeleportCooldown;
                    Vector3 Pos = GetTeleportPositionFurthestFromPlayer();
                    Teleport(animator, Pos);
                }
            } 
            else if (EnemyAttack.attackTimer <= 0)
            {
                Vector3 Pos = GetAttackTeleportPosition();
                Teleport(animator, Pos);
                animator.SetBool("isAttacking", true);
            }

            // Always face the player
            Health.FlipIfNeeded(); 
        }
    }

    void Teleport(Animator animator, Vector3 Pos)
    {
        if (Pos != Vector3.zero)
        {
            TeleportScript.TeleportLocation = Pos;
            animator.SetBool("isTeleporting", true); 
        }
    }

    Vector3 GetTeleportPositionFurthestFromPlayer()
    {
        float FurthestDistance = 0.0f;
        Vector3 FurthestPosition = Vector3.zero;
        Collider2D[] HitColliders = Physics2D.OverlapCircleAll(PlayerPos.position, MaxTeleportDistance, Mask);
        foreach (var HitCollider in HitColliders)
        {
            float NewDistance = Vector2.Distance(HitCollider.transform.position, PlayerPos.position);
            if (NewDistance > FurthestDistance) 
            {
                FurthestDistance = NewDistance;
                FurthestPosition = HitCollider.transform.position;
            } 
        }

        return FurthestPosition;
    }

    Vector3 GetAttackTeleportPosition()
    {
        RaycastHit2D HitLeft = Physics2D.Raycast(PlayerPos.position, -Vector2.right, Mathf.Infinity, Mask);
        RaycastHit2D HitRight = Physics2D.Raycast(PlayerPos.position, Vector2.right, Mathf.Infinity, Mask);

        if (HitLeft && HitRight)
        {
            if (HitLeft.distance > HitRight.distance)
            {
                return HitLeft.point;
            }
            else
            {
                return HitRight.point;    
            }
        }
        
        return Vector3.zero;
    }
}
