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
    public Animator anim;

    bool collided = false;
    public float collideCoolDown;
    float timer;

    Transform playerPos;
    public float deleteDistance;

    // Use this for initialization
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody2D>();
        audioManager = GameObject.Find("Player").GetComponent<PlayerAudioManager>();
        audioManager.arrowLooseAudio();
        timer = collideCoolDown;
        playerPos = GameObject.Find("Player").transform;
    }

	void Update () 
	{	

        if (!collided) 
        {
            if (!travelingLeft) 
            {
                thisRigidbody.velocity = new Vector2 (speed * 1, thisRigidbody.velocity.y);
            } else 
            {
                thisRigidbody.velocity = new Vector2 (speed * -1, thisRigidbody.velocity.y);
            }
        } else 
        {
            timer -= Time.deltaTime;

            if (timer <= 0) 
            {
                transform.parent = null;
                float step =  speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, playerPos.position, step);
                anim.SetBool("returnArrow", true);

                if (Vector2.Distance(transform.position, playerPos.position) < deleteDistance) 
                {
                    Destroy(gameObject);
                }
            }
        }

	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ability" || collision.gameObject.tag == "FX") 
        {
            return;
        }
        Debug.Log("COLLISION");
        audioManager.arrowHitAudio();

        collided = true;

        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("ENEMY COLLISION");
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(Random.Range(damageLowerBound, damageUpperBound), travelingLeft, false, 0);
			Destroy(gameObject);
        }
        else
        {
            anim.enabled = true;
        }
    }
}
