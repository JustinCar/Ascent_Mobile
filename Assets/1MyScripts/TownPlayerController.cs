using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownPlayerController : MonoBehaviour {

	public float speed;
	public Animator anim;
	public Rigidbody2D rigidBody;
	public bool facingLeft = true;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

	}
	void FixedUpdate () {

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
		if (move != 0) 
		{
			anim.SetInteger("AnimState", 2);
		} else if (move == 0)
		{
			anim.SetInteger("AnimState", 0);
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
}
