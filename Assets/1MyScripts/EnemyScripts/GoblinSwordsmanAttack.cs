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
    public float attackCooldown; // The time between attacks
    
    LevelManager levelManager;
    public PlayerAudioManager audioManager;

    void Awake()
    {
        levelManager = GameObject.Find("Manager").GetComponent<LevelManager>();
        normalDamageLowerBound =  (int)(normalDamageLowerBound * (levelManager.floorNumber));
        normalDamageUpperBound =  (int)(normalDamageUpperBound * (levelManager.floorNumber));
        spinDamageLowerBound =  (int)(spinDamageLowerBound * (levelManager.floorNumber));
        spinDamageUpperBound =  (int)(spinDamageUpperBound * (levelManager.floorNumber));
        audioManager = GameObject.Find("Player").GetComponent<PlayerAudioManager>();
    }

    // Called from the attack animation 
    void normalAttack()
    {
        audioManager.swordAttackAudio();
        Collider2D[] player = Physics2D.OverlapCircleAll(normalAtkPos.position, normalAtkRange, playerLayer);
        if (player.Length > 0 && enemyHealth.currentHealth > 0) 
        {
            player[0].gameObject.GetComponent<PlayerHealth>().takeDamage(Random.Range(normalDamageLowerBound, normalDamageUpperBound), enemyCtrl.playerIsLeft);
            player[0].gameObject.GetComponent<PlayerController>().attacking = false;
            player[0].gameObject.GetComponent<PlayerController>().attacked = false;
			Rigidbody2D rb = player[0].gameObject.GetComponent<Rigidbody2D>();

			if (enemyCtrl.playerIsLeft) 
			{
            	rb.AddForce(new Vector2(-spinAtkKnockPower / 2, 20));
			} else 
			{
				rb.AddForce(new Vector2(spinAtkKnockPower / 2, 20));
			}
        }
    } 

    void spinAttack()
    {
        audioManager.swordAttackAudio();
        Collider2D[] player = Physics2D.OverlapCircleAll(spinAtkPos.position, spinAtkRange, playerLayer);
        if (player.Length > 0 && enemyHealth.currentHealth > 0) 
        {
            player[0].gameObject.GetComponent<PlayerHealth>().takeDamage(Random.Range(spinDamageLowerBound, spinDamageUpperBound), enemyCtrl.playerIsLeft);
            player[0].gameObject.GetComponent<PlayerController>().attacking = false;
            player[0].gameObject.GetComponent<PlayerController>().attacked = false;

			Rigidbody2D rb = player[0].gameObject.GetComponent<Rigidbody2D>();

			if (enemyCtrl.playerIsLeft) 
			{
            	rb.AddForce(new Vector2(-spinAtkKnockPower, 20));
			} else 
			{
				rb.AddForce(new Vector2(spinAtkKnockPower, 20));
			}
        }
    } 

    // Called from attack animation when attack has finished
    void attackFinished () 
    {
        enemyCtrl.attacking = false;
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
