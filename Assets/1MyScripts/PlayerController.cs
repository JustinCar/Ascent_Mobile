﻿using System.Collections;
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
	public BoxCollider2D collider;

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

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D>();
		timer = atkAnimLength;
		spaceButtonTimer = timeBetweenSpaceButtons;
		// fazeTimer = fazeTime;
	}
	
	// Update is called once per frame
	void Update () {

		if (anim.GetInteger("AnimState") == 5) 
		{
			attacking = false;
			attacked = false;
		}

		if (Input.GetKeyDown(KeyCode.W) && jumpCounter < 1 && !attacking) 
		{
			// Cancel any previous force
			Vector2 v = rigidBody.velocity;
			v.y = 0;
			rigidBody.velocity = v;

			fazing = true;
			character.enabled = false;
			faze.gameObject.SetActive(true);

			rigidBody.AddForce(Vector2.up * jumpPower);

			jumpCounter++;
		}

		if (Input.GetKeyDown(KeyCode.S) && !attacking) 
		{
			//collider.enabled = false;
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

		if (grounded && jumpCounter != 0) 
		{
			jumpCounter = 0;
		}

		if (groundedNum == 1) 
		{
			grounded = true;
		}
	
		if (Input.GetKeyDown(KeyCode.Space) && !attacking) 
		{
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

			if (Input.GetKeyDown(KeyCode.Space) && spaceButtonTimer <= 0) 
			{
				combo = true;
				spaceButtonTimer = timeBetweenSpaceButtons;
			}	
		}
	}
	void FixedUpdate () {

		if (!attacking) 
		{
			float move = Input.GetAxis("Horizontal"); // a = -1 / d = 1

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

		//attack animation
		if (attacking && !attacked) 
		{
			//anim.SetInteger("AnimState", 0);

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
			
			swordAtk.attack();
			attacked = true;
		}

		if (fazing) 
		{
			anim.SetTrigger("Faze");
		}

	}

	// Flip the character horizontally
	void flip() 
	{
		Debug.Log("Flipping");
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
}
