using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrow : MonoBehaviour
{
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
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ability") 
        {
            return;
        }
        Debug.Log("COLLISION");
        audioManager.arrowHitAudio();

        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("ENEMY COLLISION");
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(Random.Range(damageLowerBound, damageUpperBound), travelingLeft, false, 0);
			Destroy(gameObject);
        }
        else if (collision.gameObject.tag != "FX" && collision.gameObject.tag != "Player")
        {
			Destroy(gameObject);
        }
    }
}
