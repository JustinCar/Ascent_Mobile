﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAttack : MonoBehaviour
{
    public Transform atkPos;
    public LayerMask playerLayer;
    public float atkRange;
    public int damageLowerBound;
    public int damageUpperBound; 
    public EnemyHealth enemyHealth;

    LevelManager levelManager;
    public PlayerAudioManager audioManager;

    public Animator anim;

    public float attackTimer = 0; // Timer to track attack cooldown
	public float attackCooldown; // The time between attacks
    public GameObject knightSlashEffect;

    void Awake()
    {
        levelManager = GameObject.Find("Manager").GetComponent<LevelManager>();
        float modifier = (1 + ((float)levelManager.floorNumber / 10));
        damageLowerBound =  (int)(damageLowerBound * modifier);
        damageUpperBound =  (int)(damageUpperBound * modifier);
        audioManager = GameObject.Find("Player").GetComponent<PlayerAudioManager>();
        attackTimer = attackCooldown;
    }

    void Update()
    {
        attackTimer -= Time.deltaTime;
    }

    // Called from the attack animation 
    void Attack()
    {
        audioManager.swordAttackAudio();
        Collider2D[] player = Physics2D.OverlapCircleAll(atkPos.position, atkRange, playerLayer);
        if (player.Length > 0 && enemyHealth.currentHealth > 0) 
        {
            GameObject slash = Instantiate(knightSlashEffect, player[0].transform.position, Quaternion.identity) as GameObject; 

            if (enemyHealth.playerToLeft())
                flip(slash);

            player[0].gameObject.GetComponent<PlayerHealth>().takeDamage(Random.Range(damageLowerBound, damageUpperBound), enemyHealth.playerToLeft());
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

    void flip(GameObject obj) 
	{
		Vector3 scale = obj.transform.localScale;
		scale.x *= -1;
		obj.transform.localScale = scale;
	}
}