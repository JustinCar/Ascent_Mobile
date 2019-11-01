using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinSwordsmanAttack : MonoBehaviour {

    public Transform normalAtkPos;
	public Transform spinAtkPos;
    public LayerMask playerLayer;
    public float normalAtkRange;
	public float spinAtkRange;
    public int normalDamageLowerBound;
    public int normalDamageUpperBound; 
	public int spinDamageLowerBound;
    public int spinDamageUpperBound; 
	public float spinAtkKnockPower;
    public EnemyHealth enemyHealth;
    public GoblinSwordsmanController enemyCtrl;
    
    LevelManager levelManager;
    public PlayerAudioManager audioManager;
    public Animator anim;

    public float attackTimer = 0; // Timer to track attack cooldown
	public float attackCooldown; // The time between attacks

    public float spinAttackTimer = 0; // Timer to track attack cooldown
	public float spinAttackCooldown; // The time between attacks

    void Awake()
    {
        levelManager = GameObject.Find("Manager").GetComponent<LevelManager>();
        float modifier = (1 + ((float)levelManager.floorNumber / 10));
        normalDamageLowerBound =  (int)(normalDamageLowerBound * modifier);
        normalDamageUpperBound =  (int)(normalDamageUpperBound * modifier);
        spinDamageLowerBound =  (int)(spinDamageLowerBound * modifier);
        spinDamageUpperBound =  (int)(spinDamageUpperBound * modifier);
        audioManager = GameObject.Find("Player").GetComponent<PlayerAudioManager>();
        attackTimer = attackCooldown;
        spinAttackTimer = spinAttackCooldown;
    }

    void Update()
    {
        attackTimer -= Time.deltaTime;
        spinAttackTimer -= Time.deltaTime;
    }

    // Called from the attack animation 
    void normalAttack()
    {
        audioManager.swordAttackAudio();
        Collider2D[] player = Physics2D.OverlapCircleAll(normalAtkPos.position, normalAtkRange, playerLayer);
        if (player.Length > 0 && enemyHealth.currentHealth > 0) 
        {
            player[0].gameObject.GetComponent<PlayerHealth>().takeDamage(Random.Range(normalDamageLowerBound, normalDamageUpperBound));
			Rigidbody2D rb = player[0].gameObject.GetComponent<Rigidbody2D>();
        }
    } 

    void spinAttack()
    {
        audioManager.swordAttackAudio();
        Collider2D[] player = Physics2D.OverlapCircleAll(spinAtkPos.position, spinAtkRange, playerLayer);
        if (player.Length > 0 && enemyHealth.currentHealth > 0) 
        {
            player[0].gameObject.GetComponent<PlayerHealth>().takeDamage(Random.Range(spinDamageLowerBound, spinDamageUpperBound));

			Rigidbody2D rb = player[0].gameObject.GetComponent<Rigidbody2D>();
        }
    } 

    // Called from attack animation when attack has finished
    void attackFinished () 
    {
        if (anim.GetBool("isAttacking")) 
        {
            anim.SetBool("isAttacking", false);
            attackTimer = attackCooldown;
        } else if (anim.GetBool("isSecondAttacking")) 
        {
            anim.SetBool("isSecondAttacking", false);
            spinAttackTimer = spinAttackCooldown;
        }
        
    }
    
    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(normalAtkPos.position, normalAtkRange);
		Gizmos.DrawWireSphere(spinAtkPos.position, spinAtkRange);
    }

    void destroy() 
    {
        Destroy(gameObject);
    }
}
