using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinGruntController : MonoBehaviour {


	public Rigidbody2D rigidBody;
	public float speed;
	public float chaseSpeed;
	public bool facingLeft = false;
	float move = -1;
	public Animator anim;
	bool patroling = false;
	bool stunned = false;
	public GameObject sprite;
	public float rayDistance;
	public GameObject player;
	public bool playerIsLeft;
	public float meleeAttackRange;
	public bool chasing = false;
	public bool attacking = false;
	public bool attacked = false;
	public float attackTimer = 0; // Timer to track attack cooldown
	public float attackCooldown; // The time between attacks

	public float bellySmashTimer = 0; // Timer to track attack cooldown
	public float bellySmashCooldown; // The time between attacks

	public bool isBellySmashing = false;
	
	public float patrolTimerLowerBound;
	public float patrolTimerUpperBound;
	public float idleTimerLowerBound;
	public float idleTimerUpperBound;
	float patrolTimer;
	float idleTimer;

	float disengageDistanceX = 1.87f; // The distance on the x axis before the enemy will stop chasing
	float disengageDistanceY = 1f; // The distance on the y axis before the enemy will stop chasing

	EnemyHealth health;


	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D>();
		player = GameObject.Find("Player");
		health = GetComponent<EnemyHealth>();
		patrolTimer = Random.Range(patrolTimerLowerBound, patrolTimerUpperBound);
		idleTimer = Random.Range(idleTimerLowerBound, idleTimerUpperBound);

	}
	
	// Update is called once per frame
	void Update () {
		if (!attacking) 
		{
			attacked = false;
		}

		if (health.isDead) 
		{
			this.enabled = false;
		}

		stunned = health.stunned;

		// Enemy is aware of player if attacked
		if (stunned && !chasing) 
		{
			chasing = true;
		}

		if(chasing) 
		{
			Vector2 difference = transform.position - player.transform.position;
			if (Mathf.Abs(difference.x) > disengageDistanceX) 
			{
				chasing = false;
				attacking = false;
			}
			if (Mathf.Abs(difference.y) > disengageDistanceY) 
			{
				chasing = false;
				attacking = false;
			}
		}

		if (patroling) 
		{
			patrolTimer -= Time.deltaTime;
		}else if (!patroling) 
		{
			idleTimer -= Time.deltaTime;
		}

		playerIsLeft = playerToLeft();

		if (!stunned) 
		{
			RaycastHit2D hit;
			LayerMask mask = LayerMask.GetMask("Player");

			if (chasing) 
			{
				attackTimer -= Time.deltaTime;
				bellySmashTimer -= Time.deltaTime;

				if (!attacking && Vector2.Distance(transform.position, player.transform.position) > meleeAttackRange) 
				{
					transform.position = Vector2.MoveTowards(transform.position, player.transform.position, chaseSpeed * Time.deltaTime);						
				}

				if (Vector2.Distance(transform.position, player.transform.position) < meleeAttackRange && attackTimer <= 0) 
				{
					attacking = true;
					rigidBody.velocity = Vector2.zero;
				}

				if (!attacking) 
				{
					// Always face the player
					if (playerIsLeft && !facingLeft) 
					{
						flip();
					}
					if (!playerIsLeft && facingLeft) 
					{
						flip();
					}
				}
			}
			else if (!attacking)
			{
				if (facingLeft) 
				{
					hit = Physics2D.Raycast(transform.position, -Vector2.right, rayDistance);
					if (hit) 
					{
						if (hit.collider.gameObject.tag == "Player") 
						{
							Debug.DrawRay(transform.position, new Vector2(-rayDistance, 0), Color.green);

							chasing = true;
							patroling = false;
						}
					}
					}
				else
				{
					hit = Physics2D.Raycast(transform.position, Vector2.right, rayDistance);
							
					if (hit) 
					{
						if (hit.collider.gameObject.tag == "Player") 
						{
							Debug.DrawRay(transform.position, new Vector2(rayDistance, 0), Color.green);

							chasing = true;
							patroling = false;
						}
					}
				}
			}
		}
	}

	void FixedUpdate () {
		

		if (stunned) 
		{
			anim.SetBool("Chasing", true);
			
		} else 
		{
			if (!attacking && !chasing) 
			{
				anim.SetBool("Chasing", false);
				// Switching between idle and patrolling randomly
				if (patroling && patrolTimer <= 0) 
				{
					patroling = false;
					patrolTimer = Random.Range(patrolTimerLowerBound, patrolTimerUpperBound);
				} else if (!patroling && idleTimer <= 0)
				{
					patroling = true;
					idleTimer = Random.Range(idleTimerLowerBound, idleTimerUpperBound);
				}

				// Occasionally change direction
				if (Random.Range(1, 200) <= 1) 
				{
					if (move == -1) 
					{
						move = 1;
					} else if (move == 1) 
					{
						move = -1;
					}
				}
			}

			if (!attacking && chasing) 
			{
				anim.SetBool("Chasing", true);
				if ((Vector2.Distance(transform.position, player.transform.position) > meleeAttackRange) && anim.GetInteger("AnimState") != 1) 
				{
					anim.SetInteger("AnimState", 1);
				} else if ((Vector2.Distance(transform.position, player.transform.position) <= meleeAttackRange)) 
				{
					anim.SetInteger("AnimState", 5);
				}
			}

			// Attacks
			if (attacking && !attacked) 
			{
				if (bellySmashTimer <= 0) 
				{
					isBellySmashing = true;
					anim.SetInteger("AnimState", 3);
					bellySmashTimer = bellySmashCooldown;
					attackTimer = attackCooldown;
					attacked = true;
				} else if (anim.GetInteger("AnimState") != 3 && !isBellySmashing)
				{
					anim.SetInteger("AnimState", 2);
					attackTimer = attackCooldown;
					attacked = true;
				}
			}

			if (patroling) 
			{
				rigidBody.velocity = new Vector2 (speed * move, rigidBody.velocity.y);

				if (anim.GetInteger("AnimState") != 4) 
				{
					anim.SetInteger("AnimState", 4);
				}
				
			} else if (!patroling && !chasing)
			{
				anim.SetInteger("AnimState", 0);
			}

			if (!attacking && !chasing) 
			{
				if (move > 0 && facingLeft) 
				{
					flip();
				}
				else if (move < 0 && !facingLeft) 
				{
					flip();
				}		
			}
		}
	}

	void flip() 
	{
		facingLeft = !facingLeft;
		Vector3 scale = sprite.transform.localScale;
		scale.x *= -1;
		sprite.transform.localScale = scale;
	}

	// Prevent from falling off platform
	private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlatformEdge" && !chasing) 
		{
			if (move == -1) 
			{
				move = 1;
			} else if (move == 1) 
			{
				move = -1;
			}
		}
    }

	bool playerToLeft() 
	{
		if ((transform.position.x - player.transform.position.x) < 0) 
		{
			return false;
		}
		if ((transform.position.x - player.transform.position.x) > 0) 
		{
			return true;
		}

		return false;
	}

	void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, new Vector3 (disengageDistanceX, transform.position.y, transform.position.z));
		Gizmos.DrawLine(transform.position, new Vector3 (transform.position.x, disengageDistanceY, transform.position.z));
    }
}
