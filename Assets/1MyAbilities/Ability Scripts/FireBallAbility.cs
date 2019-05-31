using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallAbility : MonoBehaviour {
    public int damageLowerBound;
    public int damageUpperBound;
    public GameObject explosionPrefab;
    public Rigidbody2D thisRigidbody;
	public float speed;
	public bool travelingLeft = false;
    EnemyState enemyState;
    PlayerController playerCtrl;
    AbilityStats stats;
    public GameObject effect;

    public int damageType;

    public PlayerAudioManager audioManager;
	

    // Use this for initialization
    void Start()
    {
        playerCtrl = GameObject.Find("Player").GetComponent<PlayerController>();
        thisRigidbody = GetComponent<Rigidbody2D>();
        travelingLeft = playerCtrl.facingLeft;
        stats = gameObject.GetComponent<AbilityStats>();
        audioManager = GameObject.Find("Player").GetComponent<PlayerAudioManager>();

        damageLowerBound = stats.damageLowerBound;
        damageUpperBound = stats.damageUpperBound;
    }

	void Update () 
	{	
		if (!travelingLeft) 
		{
			thisRigidbody.velocity = new Vector2 (speed * 1, thisRigidbody.velocity.y);
		} else 
		{
			thisRigidbody.velocity = new Vector2 (speed * -1, thisRigidbody.velocity.y);
		}

	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ability") 
        {
            return;
        }

        switch (damageType) 
        {
            case 1:
                audioManager.fireHitAudio();
                break;
            case 2:
                audioManager.poisonHitAudio();
                break;
            case 3:
                audioManager.iceHitAudio();
                break;
            case 4:
                audioManager.voidHitAudio();
                break;
        }

        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(Random.Range(damageLowerBound, damageUpperBound), travelingLeft, true, damageType);
            Instantiate(explosionPrefab, new Vector3(collision.contacts[0].point.x, collision.contacts[0].point.y, 0),  Quaternion.identity);

            enemyState = collision.gameObject.GetComponent<EnemyState>();            
            if (enemyState.isEffected == true) 
            {
                Destroy(gameObject);               
            } else
            {
                GameObject specialEffect = Instantiate(effect) as GameObject; 
                Effect effectInfo = specialEffect.GetComponentInChildren<Effect>(); 

                effectInfo.enemy = collision.gameObject.GetComponent<EnemyHealth>();
                effectInfo.enemyState = enemyState;
                effectInfo.stats = stats;
                effectInfo.travelingLeft = travelingLeft;
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.tag != "FX" && collision.gameObject.tag != "Enemy")
        {
            Instantiate(explosionPrefab, new Vector3(collision.contacts[0].point.x, collision.contacts[0].point.y, 0),  Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
