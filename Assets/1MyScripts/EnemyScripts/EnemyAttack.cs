using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public Transform atkPos;
    public LayerMask playerLayer;
    public float atkRange;
    public int damageLowerBound;
    public int damageUpperBound; 
    public EnemyHealth enemyHealth;
    public EnemyController enemyCtrl;
    public float attackCooldown; // The time between attacks

    LevelManager levelManager;
    public PlayerAudioManager audioManager;

    void Awake()
    {
        levelManager = GameObject.Find("Manager").GetComponent<LevelManager>();
        damageLowerBound =  (int)(damageLowerBound * (levelManager.floorNumber));
        damageUpperBound =  (int)(damageUpperBound * (levelManager.floorNumber));
        audioManager = GameObject.Find("Player").GetComponent<PlayerAudioManager>();
    }

    void Update()
    {

    }

    // Called from the attack animation 
    void Attack()
    {
        audioManager.swordAttackAudio();
        Collider2D[] player = Physics2D.OverlapCircleAll(atkPos.position, atkRange, playerLayer);
        if (player.Length > 0 && enemyHealth.currentHealth > 0) 
        {
            player[0].gameObject.GetComponent<PlayerHealth>().takeDamage(Random.Range(damageLowerBound, damageUpperBound), enemyCtrl.playerIsLeft);
            player[0].gameObject.GetComponent<PlayerController>().attacking = false;
            player[0].gameObject.GetComponent<PlayerController>().attacked = false;
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
        Gizmos.DrawWireSphere(atkPos.position, atkRange);
    }

    void destroy() 
    {
        Destroy(gameObject);
    }
}
