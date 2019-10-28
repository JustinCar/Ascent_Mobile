using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinGruntAttack : MonoBehaviour {

    public Transform normalAtkPos;
	public Transform bellySmashAtkPos;
    public LayerMask playerLayer;
    public float normalAtkRange;
	public float bellySmashAtkRange;
    public int normalDamageLowerBound;
    public int normalDamageUpperBound; 
	public int bellySmashDamageLowerBound;
    public int bellySmashDamageUpperBound; 
	public float bellySmashAtkKnockPower;
	public float bellySmashKnockBackTime;
	float bellySmashKnockBackTimer;
    public EnemyHealth enemyHealth;

	bool bellySmashed = false;

	public Rigidbody2D playerRB;
    LevelManager levelManager;

    public CameraShake cameraShake;

    public PlayerAudioManager audioManager;
    public Animator anim;

    public float attackTimer = 0; // Timer to track attack cooldown
	public float attackCooldown; // The time between attacks

    public float bellySmashTimer = 0; // Timer to track attack cooldown
	public float bellySmashCooldown; // The time between attacks
	void Start () 
	{
		playerRB = GameObject.Find("Player").GetComponent<Rigidbody2D>();
		bellySmashKnockBackTimer = bellySmashKnockBackTime;
        cameraShake = GameObject.Find("Manager").GetComponent<CameraShake>();
        levelManager = GameObject.Find("Manager").GetComponent<LevelManager>();
        float modifier = (1 + ((float)levelManager.floorNumber / 10));
        normalDamageLowerBound =  (int)(normalDamageLowerBound * modifier);
        normalDamageUpperBound =  (int)(normalDamageUpperBound * modifier);
        bellySmashDamageLowerBound =  (int)(bellySmashDamageLowerBound * modifier);
        bellySmashDamageUpperBound =  (int)(bellySmashDamageUpperBound * modifier);
        audioManager = GameObject.Find("Player").GetComponent<PlayerAudioManager>();

        attackTimer = attackCooldown;
        bellySmashTimer = attackCooldown;
	}

	void Update () 
	{	
        attackTimer -= Time.deltaTime;
        bellySmashTimer -= Time.deltaTime;

		if (bellySmashed) 
		{
			//playerRB = GameObject.Find("Player").GetComponent<Rigidbody2D>();
			playerRB.velocity = Vector2.zero;

			bellySmashKnockBackTimer -= Time.deltaTime;

			if (enemyHealth.playerToLeft()) 
			{
            	playerRB.AddForce(new Vector2(-bellySmashAtkKnockPower, 10));
			} else 
			{
				playerRB.AddForce(new Vector2(bellySmashAtkKnockPower, 10));
			}

			if (bellySmashKnockBackTimer <= 0) 
			{
				bellySmashKnockBackTimer = bellySmashKnockBackTime;
				bellySmashed = false;
			}
		}
	}

    // Called from the attack animation 
    void normalAttack()
    {
        StartCoroutine(cameraShake.Shake(0.2f, 1.5f));
        audioManager.explosionAudio();
        Collider2D[] player = Physics2D.OverlapCircleAll(normalAtkPos.position, normalAtkRange, playerLayer);
        if (player.Length > 0 && enemyHealth.currentHealth > 0) 
        {
            player[0].gameObject.GetComponent<PlayerHealth>().takeDamage(Random.Range(normalDamageLowerBound, normalDamageUpperBound), enemyHealth.playerToLeft());
            player[0].gameObject.GetComponent<PlayerController>().attacking = false;
            player[0].gameObject.GetComponent<PlayerController>().attacked = false;
        }
    } 

    void bellySmashAttack()
    {
        Collider2D[] player = Physics2D.OverlapCircleAll(bellySmashAtkPos.position, bellySmashAtkRange, playerLayer);
        if (player.Length > 0 && enemyHealth.currentHealth > 0) 
        {
            player[0].gameObject.GetComponent<PlayerHealth>().takeDamage(Random.Range(bellySmashDamageLowerBound, bellySmashDamageUpperBound), enemyHealth.playerToLeft());

			bellySmashed = true;
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
            bellySmashTimer = bellySmashCooldown;
        }
    }
    
    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(normalAtkPos.position, normalAtkRange);
		Gizmos.DrawWireSphere(bellySmashAtkPos.position, bellySmashAtkRange);
    }

    void destroy() 
    {
        Destroy(gameObject);
    }

    void walkCameraShake() 
    {
        if (Vector3.Distance(transform.position, playerRB.transform.position) < 3) 
        {
            StartCoroutine(cameraShake.Shake(0.1f, 1f));
            audioManager.gruntFootStepsAudio();
        }
        
    }

    void runCameraShake() 
    {
        if (Vector3.Distance(transform.position, playerRB.transform.position) < 3) 
        {
            StartCoroutine(cameraShake.Shake(0.1f, 2f));
            audioManager.gruntFootStepsAudio();
        }
    }
}
