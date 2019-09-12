using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinArcherAttack : MonoBehaviour {


    public Transform atkPos;
    public LayerMask playerLayer;
    public float atkRange;
    public int damageLowerBound;
    public int damageUpperBound; 
    public EnemyHealth enemyHealth;
    public GoblinArcherController enemyCtrl;

	public GameObject arrow;

    
    LevelManager levelManager;
    public Animator anim;
    public float attackTimer = 0; // Timer to track attack cooldown
	public float attackCooldown; // The time between attacks

    void Awake()
    {
        levelManager = GameObject.Find("Manager").GetComponent<LevelManager>();
        damageLowerBound =  (int)(damageLowerBound * (levelManager.floorNumber));
        damageUpperBound =  (int)(damageUpperBound * (levelManager.floorNumber));
        attackTimer = attackCooldown;
    }

    void Update()
    {
        attackTimer -= Time.deltaTime;
    }

    // Called from the attack animation 
    void meleeAttack()
    {
        Collider2D[] player = Physics2D.OverlapCircleAll(atkPos.position, atkRange, playerLayer);
        if (player.Length > 0 && enemyHealth.currentHealth > 0) 
        {
            player[0].gameObject.GetComponent<PlayerHealth>().takeDamage(Random.Range(damageLowerBound, damageUpperBound), enemyCtrl.playerIsLeft);
            player[0].gameObject.GetComponent<PlayerController>().attacking = false;
            player[0].gameObject.GetComponent<PlayerController>().attacked = false;
        }
    } 

	void bowAttack() 
	{
		Vector3 pos = transform.position;
		pos.y -= 0.11f;

		GameObject abilityInstance = Instantiate(arrow, atkPos.transform.position, arrow.transform.rotation) as GameObject;
		Arrow arrowScript = abilityInstance.GetComponent<Arrow>();

		if (enemyCtrl.facingLeft) 
        {
			arrowScript.travelingLeft = true;
            Quaternion target = Quaternion.Euler(0, 0, 45);
			abilityInstance.transform.rotation = Quaternion.Slerp(target, target, Time.deltaTime);
        }
	}

    // Called from attack animation when attack has finished
    void attackFinished () 
    {
        anim.SetBool("isAttacking", false);
        attackTimer = attackCooldown;
    }
    
    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(atkPos.position, atkRange);
    }

    void destroy() 
    {
        Destroy(gameObject);
    }
}
