using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	public int damageLowerBound;
    public int damageUpperBound;
    public Rigidbody2D thisRigidbody;
	public float speed;
	public bool travelingLeft = false;
	
    LevelManager levelManager;
    public PlayerAudioManager audioManager;



    // Use this for initialization
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody2D>();
        levelManager = GameObject.Find("Manager").GetComponent<LevelManager>();
        damageLowerBound =  (int)(damageLowerBound * (levelManager.floorNumber));
        damageUpperBound =  (int)(damageUpperBound * (levelManager.floorNumber));
        audioManager = GameObject.Find("Player").GetComponent<PlayerAudioManager>();
        audioManager.arrowLooseAudio();
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
        
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Ability") 
        {
            return;
        }

        audioManager.arrowHitAudio();

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().takeDamage(Random.Range(damageLowerBound, damageUpperBound), travelingLeft);
			Destroy(gameObject);
        }
        else if (collision.gameObject.tag != "FX" && collision.gameObject.tag != "Player")
        {
			Destroy(gameObject);
        }
    }
}
