using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpell : MonoBehaviour {
    public int damageLowerBound;
    public int damageUpperBound;
    public GameObject explosionPrefab;
    public Rigidbody2D thisRigidbody;
	public float speed;
	public bool travelingLeft = false;
    PlayerController playerCtrl;
    AbilityStats stats;
	public float spellDuration;
	float spellTimer;
	LevelManager levelManager;

	public PlayerAudioManager audioManager;

	public Vector3 target;

    // Use this for initialization
    void Start()
    {
		levelManager = GameObject.Find("Manager").GetComponent<LevelManager>();
        damageLowerBound =  (int)(damageLowerBound * (levelManager.floorNumber));
        damageUpperBound =  (int)(damageUpperBound * (levelManager.floorNumber));

        playerCtrl = GameObject.Find("Player").GetComponent<PlayerController>();
        thisRigidbody = GetComponent<Rigidbody2D>();
        travelingLeft = playerCtrl.facingLeft;
        stats = gameObject.GetComponent<AbilityStats>();
		spellTimer = spellDuration;
		audioManager = GameObject.Find("Player").GetComponent<PlayerAudioManager>();

		target = playerCtrl.gameObject.transform.position - transform.position;
		target.y += 0.3f;
		target.x -= 0.1f;
    }

	void Update () 
	{	
		spellTimer -= Time.deltaTime;

		if (spellTimer <= 0) 
		{
			audioManager.iceHitAudio();
			Instantiate(explosionPrefab, transform.position,  explosionPrefab.transform.rotation);
			Destroy(gameObject);
		}

		float step = speed * Time.deltaTime;

        // move sprite towards the target location
        transform.position += target * step;
	}

	bool playerToLeft(GameObject player) 
	{
		if ((transform.position.x - player.transform.position.x) < 0) 
		{
			return false;
		}
		if ((transform.position.x - player.transform.position.x) > 0) 
		{
			return true;
		}
		Debug.Log("ERROR");
		return false;
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag != "Player") 
        {
            return;
        }

        if (collision.gameObject.tag == "Player")
        {
			audioManager.iceHitAudio();
            collision.gameObject.GetComponent<PlayerHealth>().takeDamage(Random.Range(damageLowerBound, damageUpperBound));
            Instantiate(explosionPrefab, new Vector3(collision.contacts[0].point.x, collision.contacts[0].point.y, 0),  explosionPrefab.transform.rotation);
			Destroy(gameObject);
        }
    }
}
