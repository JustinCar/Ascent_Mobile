using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerAbility : MonoBehaviour {
    public int damageLowerBound;
    public int damageUpperBound;
	public bool travelingLeft = true;
    List<EnemyHealth> enemies;

    public GameObject player;
    public PlayerController playerController;

    public float coolDown;
    float timer;

    public float xOffset;
    public float yOffset;
	

    // Use this for initialization
    void Start()
    {
        enemies = new List<EnemyHealth>();
        timer = 0;
        gameObject.transform.Rotate(0, 0, 270);
        player = GameObject.Find("Player");
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        
    }

	void Update () 
	{	
        travelingLeft = playerController.facingLeft;
        position();
	}

    void damageEnemy () 
    {
        if (enemies.Count > 0) 
        {
            foreach (EnemyHealth enemy in enemies) 
            {
                enemy.TakeDamage(Random.Range(damageLowerBound, damageUpperBound), travelingLeft, true, 0);
            }
        }
    }

    void position() 
    {
        if (travelingLeft) 
        {
            transform.position = player.transform.position;

            Vector3 pos = transform.position;
            pos.x -= xOffset;
            pos.y += yOffset;
            transform.position = pos;
        } else 
        {
            //gameObject.transform.Rotate(0, 0, 180);

            Quaternion target = Quaternion.Euler(0, 0, 90);

            transform.rotation = Quaternion.Slerp(target, target, Time.deltaTime);

            transform.position = player.transform.position;
            Vector3 pos = transform.position;
            pos.x += xOffset;
            pos.y += yOffset;
            transform.position = pos;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        EnemyHealth health = collision.GetComponent<EnemyHealth>();

        if (health != null)
        {
            enemies.Add(health);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        EnemyHealth health = collision.GetComponent<EnemyHealth>();

        if (health != null)
        {
            enemies.Remove(health);
        }
    }

    void Destroy () 
    {
        Destroy(gameObject);
    }
}
