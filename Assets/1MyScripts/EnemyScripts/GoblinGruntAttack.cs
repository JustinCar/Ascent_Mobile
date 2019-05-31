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
    public GoblinGruntController enemyCtrl;
    public float attackCooldown; // The time between attacks

	bool bellySmashed = false;

	public Rigidbody2D playerRB;
    LevelManager levelManager;

    public CameraShake cameraShake;

    public PlayerAudioManager audioManager;

	void Start () 
	{
		playerRB = GameObject.Find("Player").GetComponent<Rigidbody2D>();
		bellySmashKnockBackTimer = bellySmashKnockBackTime;

        cameraShake = GameObject.Find("Manager").GetComponent<CameraShake>();
        

        levelManager = GameObject.Find("Manager").GetComponent<LevelManager>();
        normalDamageLowerBound =  (int)(normalDamageLowerBound * (levelManager.floorNumber));
        normalDamageUpperBound =  (int)(normalDamageUpperBound * (levelManager.floorNumber));
        bellySmashDamageLowerBound =  (int)(bellySmashDamageLowerBound * (levelManager.floorNumber));
        bellySmashDamageUpperBound =  (int)(bellySmashDamageUpperBound * (levelManager.floorNumber));
        audioManager = GameObject.Find("Player").GetComponent<PlayerAudioManager>();
	}

	void Update () 
	{	
		if (bellySmashed) 
		{
			//playerRB = GameObject.Find("Player").GetComponent<Rigidbody2D>();
			playerRB.velocity = Vector2.zero;

			bellySmashKnockBackTimer -= Time.deltaTime;

			if (enemyCtrl.playerIsLeft) 
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
            player[0].gameObject.GetComponent<PlayerHealth>().takeDamage(Random.Range(normalDamageLowerBound, normalDamageUpperBound), enemyCtrl.playerIsLeft);
            player[0].gameObject.GetComponent<PlayerController>().attacking = false;
            player[0].gameObject.GetComponent<PlayerController>().attacked = false;
        }
    } 

    void bellySmashAttack()
    {
        Collider2D[] player = Physics2D.OverlapCircleAll(bellySmashAtkPos.position, bellySmashAtkRange, playerLayer);
        if (player.Length > 0 && enemyHealth.currentHealth > 0) 
        {
            player[0].gameObject.GetComponent<PlayerHealth>().takeDamage(Random.Range(bellySmashDamageLowerBound, bellySmashDamageUpperBound), enemyCtrl.playerIsLeft);
            player[0].gameObject.GetComponent<PlayerController>().attacking = false;
            player[0].gameObject.GetComponent<PlayerController>().attacked = false;

			bellySmashed = true;
        }
    } 

    // Called from attack animation when attack has finished
    void attackFinished () 
    {
        enemyCtrl.attacking = false;
		if (enemyCtrl.isBellySmashing) 
		{
			enemyCtrl.isBellySmashing = false;
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
            StartCoroutine(cameraShake.Shake(0.1f, 1.5f));
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
