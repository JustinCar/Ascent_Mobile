using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinSpellCasterAttack : MonoBehaviour {

    public Transform atkPos;
	public Transform spellCastPos;
    public LayerMask playerLayer;
    public float atkRange;
    public int damageLowerBound;
    public int damageUpperBound; 
    public EnemyHealth enemyHealth;
    public GoblinSpellCasterController enemyCtrl;

	public GameObject castingFX;

	public GameObject spell;
    LevelManager levelManager;
    public PlayerAudioManager audioManager;
    public Animator anim;

    public float attackTimer = 0; // Timer to track attack cooldown
	public float attackCooldown; // The time between attacks

    public float spellAttackTimer = 0; // Timer to track attack cooldown
	public float spellAttackCooldown; // The time between attacks

    void Awake()
    {
        levelManager = GameObject.Find("Manager").GetComponent<LevelManager>();
        damageLowerBound =  (int)(damageLowerBound * (levelManager.floorNumber));
        damageUpperBound =  (int)(damageUpperBound * (levelManager.floorNumber));
        audioManager = GameObject.Find("Player").GetComponent<PlayerAudioManager>();
        attackTimer = attackCooldown;
        spellAttackTimer = spellAttackCooldown;
    }

    void Update()
    {
        attackTimer -= Time.deltaTime;
        spellAttackTimer -= Time.deltaTime;
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

	void spellAttack() 
	{
		castingFX.gameObject.SetActive(false);
		Vector3 pos = transform.position;
		pos.y -= 0.11f;
        audioManager.spellCastAudio();

		GameObject abilityInstance = Instantiate(spell, spellCastPos.transform.position, spell.transform.rotation) as GameObject;
	}

	void showCastingFX () 
	{
        audioManager.spellPrepareAudio();
		castingFX.gameObject.SetActive(true);
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
            spellAttackTimer = spellAttackCooldown;
        }
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
