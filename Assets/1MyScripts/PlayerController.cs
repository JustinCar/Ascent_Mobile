using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float jumpPower;
	public Animator anim;
	public Rigidbody2D rigidBody;
	public bool facingLeft = true;
	public bool grounded = false;
	public int groundedNum = 0;	
	public bool attacking = false;
	public bool attacked = false;
	public bool combo = false;
	public int comboNum = 1;
	float atkAnimLength = 0.4f;
	float timer;
	public SwordAttack swordAtk;
	public BowAttack bowAtk;
	public MartialAttack martialAtk;

	public int jumpCounter = 0;

	public float fazeTime;

	float fazeTimer;
	bool fazing = false;

	public SpriteRenderer faze;
	public SpriteRenderer character;

	public GameObject bubbles;

	public float timeBetweenSpaceButtons = 0.5f;

	public float spaceButtonTimer;

	public float knockPower;

	public PlayerAudioManager audioManager;

	public bool upButtonPressed = false;
	public bool downButtonPressed = false;
	public bool weaponButtonPressed = false;

	public float move = 0;

	int weapon = 2;

	public Joystick joyStick;

	public float jumpCoolDown;
	float jumpTimer;

	public float fallCoolDown;
	float fallTimer;

	bool falling = false;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D>();
		timer = atkAnimLength;
		spaceButtonTimer = timeBetweenSpaceButtons;
		weapon = SaveLoadManager.getFightingStyle();
		// fazeTimer = fazeTime;
		jumpTimer = jumpCoolDown;
		fallTimer = fallCoolDown;
	}
	
	// Update is called once per frame
	void Update () {
		float verticalMove = joyStick.Vertical;

		if (joyStick.Horizontal >= 0.1f)
			move = 1;
		else if (joyStick.Horizontal <= -0.1f) 
			move = -1;
		else
			move = 0;

		if (anim.GetInteger("AnimState") == 5) 
		{
			attacking = false;
			attacked = false;
		}

		if (jumpCounter >= 1) 
		{
			jumpTimer -= Time.deltaTime;
			if (jumpTimer <= 0) 
			{
				jumpCounter = 0;
				jumpTimer = jumpCoolDown;
			}
		}

		if ((Input.GetKeyDown(KeyCode.W) || upButtonPressed || verticalMove >= 0.5f) && jumpCounter < 1 && !attacking) 
		{
			// Cancel any previous force
			Vector2 v = rigidBody.velocity;
			v.y = 0;
			rigidBody.velocity = v;

			fazing = true;
			character.enabled = false;
			faze.gameObject.SetActive(true);
			audioManager.phaseAudio();

			rigidBody.velocity = Vector2.zero;
			rigidBody.AddForce(Vector2.up * jumpPower);

			jumpCounter++;
		}

		if (falling) 
		{
			fallTimer -= Time.deltaTime;
			if (fallTimer <= 0) 
			{
				falling = false;
				fallTimer = fallCoolDown;
			}
		}

		if ((Input.GetKeyDown(KeyCode.S) || downButtonPressed || verticalMove <= -0.5f) && !falling && !attacking) 
		{
			falling = true;
			Physics2D.IgnoreLayerCollision(9, 12, true); // Ignore collisions between player and soft ground
			Physics2D.IgnoreLayerCollision(12, 9, true);
			fazing = true;
			character.enabled = false;
			faze.gameObject.SetActive(true);
			audioManager.phaseAudio();
		}


		if (fazing) 
		{
			fazeTimer -= Time.deltaTime;

			if (fazeTimer <= 0) 
			{
				fazing = false;
				//collider.enabled = true;
				Physics2D.IgnoreLayerCollision(9, 12, false); // Reset collision detection
				Physics2D.IgnoreLayerCollision(12, 9, false);
				fazeTimer = fazeTime;
				character.enabled = true;
				faze.gameObject.SetActive(false);
			}
		}

		// if (grounded && jumpCounter != 0) 
		// {
		// 	jumpCounter = 0;
		// }

		if (groundedNum == 1) 
		{
			grounded = true;
		}
	
		if ((Input.GetKeyDown(KeyCode.Space) || weaponButtonPressed) && !attacking) 
		{
			weaponButtonPressed = false;
			attacking = true;
			rigidBody.velocity = Vector3.zero;
		}

		if (!attacking) 
		{
			spaceButtonTimer = timeBetweenSpaceButtons;
			attacked = false;
		}

		if (attacking) 
		{
			spaceButtonTimer -= Time.deltaTime;
			//rigidBody.velocity = Vector3.zero;

			if ((Input.GetKeyDown(KeyCode.Space) || weaponButtonPressed) && spaceButtonTimer <= 0) 
			{
				weaponButtonPressed = false;
				combo = true;
				spaceButtonTimer = timeBetweenSpaceButtons;
			}	
		}

		if (!attacking) 
		{
			if (Input.GetKeyDown(KeyCode.A)) 
			{
				move = -1;	
			}
			if (Input.GetKeyDown(KeyCode.D)) 
			{
				move = 1;	
			}
			if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) 
			{
				move = 0;
			}
		}
	}
	void FixedUpdate () {

		if (!attacking) 
		{
			// if (Input.GetKeyDown(KeyCode.A)) 
			// {
			// 	move = -1;	
			// }
			// if (Input.GetKeyDown(KeyCode.D)) 
			// {
			// 	move = 1;	
			// }
			// if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) 
			// {
			// 	move = 0;
			// }

			rigidBody.velocity = new Vector2 (speed * move, rigidBody.velocity.y);	

			if (move > 0 && facingLeft) 
			{
				flip();
			}
			else if (move < 0 && !facingLeft) 
			{
				flip();
			}

			// Run animation
			if (move != 0 && !attacking) 
			{
				anim.SetInteger("AnimState", 2);
			} else if (move == 0 && !attacking)
			{
				anim.SetInteger("AnimState", 0);
			}			
		}

		if (weapon == 0) 
		{
			//Bow attack animation
			if (attacking && !attacked) 
			{
				if (comboNum == 1) 
				{
					anim.SetTrigger("BowAttack");
				} else if (comboNum == 2) 
				{
					anim.SetTrigger("BowAttack2");
				}

				// Player gets knocked back slightly when shooting
				if (facingLeft) 
				{
					rigidBody.AddForce(new Vector2(knockPower, knockPower));
				}
				else if(!facingLeft) 
				{
					rigidBody.AddForce(new Vector2(-knockPower, knockPower));
				} 
				
				attacked = true;
			}	
		}
		else if (weapon == 1) 
		{
			//Martial attack animation
			if (attacking && !attacked) 
			{

				if (comboNum == 1) 
				{
					anim.SetTrigger("PunchAttack");
				} else if (comboNum == 2) 
				{
					anim.SetTrigger("PunchAttack2");
				} else if (comboNum == 3) 
				{
					anim.SetTrigger("PunchAttack3");
				} else if (comboNum == 4) 
				{
					anim.SetTrigger("KickAttack");
				} else if (comboNum == 5) 
				{
					anim.SetTrigger("KickAttack2");
				}

				if (facingLeft) 
				{
					rigidBody.AddForce(new Vector2(-knockPower, knockPower));
				}
				else if(!facingLeft) 
				{
					rigidBody.AddForce(new Vector2(knockPower, knockPower));
				} 
				
				martialAtk.attack(comboNum);
				attacked = true;
			}	
		}
		else if (weapon == 2) 
		{
			//Sword attack animation
			if (attacking && !attacked) 
			{

				if (comboNum == 1) 
				{
					anim.SetTrigger("Attack");
				} else if (comboNum == 2) 
				{
					anim.SetTrigger("Attack2");
				} else if (comboNum == 3) 
				{
					anim.SetTrigger("Attack3");
				}

				if (facingLeft) 
				{
					rigidBody.AddForce(new Vector2(-knockPower, knockPower));
				}
				else if(!facingLeft) 
				{
					rigidBody.AddForce(new Vector2(knockPower, knockPower));
				} 
				
				swordAtk.attack(comboNum);
				attacked = true;
			}			
		}


		if (fazing) 
		{
			anim.SetTrigger("Faze");
		}

	}

	// Flip the character horizontally
	void flip() 
	{
		facingLeft = !facingLeft;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

 
	void OnCollisionEnter2D(Collision2D col) 
	{
		if (!attacking) 
		{
			if (col.gameObject.tag == "Ground") 
			{
				groundedNum++;
				grounded = true;
			}
		}

	}

	void OnCollisionExit2D(Collision2D col) 
	{
		if (!attacking) 
		{
			if (col.gameObject.tag == "Ground") 
			{
				groundedNum--;

				// Prevent grounded boolean from becoming false when
				// Moving between seperate adjacent ground
				if (groundedNum <= 0) 
				{
					grounded = false;
				}
			}
		}
	}

	public void showBubbles() 
	{
		bubbles.SetActive(true);
	}

	public void hideBubbles() 
	{
		bubbles.SetActive(false);
	}

	public void endAttack() 
	{
		rigidBody.velocity = Vector3.zero;
		if (!combo) 
		{
			attacking = false;
			attacked = false;	
			comboNum = 1;
			spaceButtonTimer = timeBetweenSpaceButtons;
		} else 
		{
			attacked = false;
			comboNum++;
			combo = false;
		}

		if (comboNum > 3) 
		{
			attacking = false;
			attacked = false;	
			comboNum = 1;
			combo = false;
			spaceButtonTimer = timeBetweenSpaceButtons;
		}
	}

	public void endMartialAttack() 
	{
		rigidBody.velocity = Vector3.zero;
		if (!combo) 
		{
			attacking = false;
			attacked = false;	
			comboNum = 1;
			spaceButtonTimer = timeBetweenSpaceButtons;
		} else 
		{
			attacked = false;
			comboNum++;
			combo = false;
		}

		if (comboNum > 5) 
		{
			attacking = false;
			attacked = false;	
			comboNum = 1;
			combo = false;
			spaceButtonTimer = timeBetweenSpaceButtons;
		}
	}

	public void bowAttack () 
	{
		bowAtk.attack(1);
	}

	public void bowAttack2 () 
	{
		bowAtk.attack(2);
	}

	public void endBowAttack() 
	{
		rigidBody.velocity = Vector3.zero;
		if (!combo) 
		{
			attacking = false;
			attacked = false;	
			comboNum = 1;
			spaceButtonTimer = timeBetweenSpaceButtons;
		} else 
		{
			attacked = false;
			comboNum++;
			combo = false;
		}

		if (comboNum > 2) 
		{
			attacking = false;
			attacked = false;	
			comboNum = 1;
			combo = false;
			spaceButtonTimer = timeBetweenSpaceButtons;
		}
	}
}
