using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeAbility : MonoBehaviour {
    public int damageLowerBound;
    public int damageUpperBound;
	public float explosionRange;
	public float explodeTimer;
    public GameObject explosionPrefab;
    public Rigidbody2D thisRigidbody;
	public float speed;
	public bool travelingLeft = false;
    EnemyState enemyState;
    PlayerController playerCtrl;
    AbilityStats stats;
    public GameObject effect;
	public LayerMask enemyLayer;

	public float explosionOffset;

	public PlayerAudioManager audioManager;
	

    // Use this for initialization
    void Start()
    {
        playerCtrl = GameObject.Find("Player").GetComponent<PlayerController>();
        thisRigidbody = GetComponent<Rigidbody2D>();
        travelingLeft = playerCtrl.facingLeft;
        stats = gameObject.GetComponent<AbilityStats>();

		if (travelingLeft) 
		{
			thisRigidbody.AddForce(new Vector2(-speed, speed/2));
		} else 
		{
			thisRigidbody.AddForce(new Vector2(speed, speed/2));
		}
		

        damageLowerBound = stats.damageLowerBound;
        damageUpperBound = stats.damageUpperBound;
		audioManager = GameObject.Find("Player").GetComponent<PlayerAudioManager>();
    }

	void Update () 
	{	
		explodeTimer -= Time.deltaTime;

		if (explodeTimer <= 0) 
		{
			GameObject explosion = Instantiate(explosionPrefab, gameObject.transform.position,  Quaternion.identity);
			Vector3 pos = explosion.transform.position;
			pos.y += explosionOffset;
			explosion.transform.position = pos;

			audioManager.explosionAudio();

			Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, explosionRange, enemyLayer);
			if (enemies.Length > 0) 
			{   
				foreach (Collider2D c in enemies) 
				{
					bool toRight;
					if (c.gameObject.transform.position.x < gameObject.transform.position.x) 
					{
						toRight = true;
					} else 
					{
						toRight = false;
					}
					c.gameObject.GetComponent<EnemyHealth>().TakeDamage(Random.Range(damageLowerBound, damageUpperBound), toRight, true, 0);

					enemyState = c.gameObject.GetComponent<EnemyState>();

					if (enemyState.isEffected == false && effect) 
					{
						GameObject specialEffect = Instantiate(effect) as GameObject; 
						Effect effectInfo = specialEffect.GetComponentInChildren<Effect>(); 

						effectInfo.enemy = c.gameObject.GetComponent<EnemyHealth>();
						effectInfo.enemyState = enemyState;
						effectInfo.stats = stats;
						effectInfo.travelingLeft = travelingLeft;
						
					}
				}	
			}
			Destroy(gameObject);
		}
	}

	void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
