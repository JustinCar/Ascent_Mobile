using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssenceFollow : MonoBehaviour {

	Transform player;
	public float modifier;
	Vector2 velocity = Vector2.zero;
	public float waitLowerBound;
	public float waitUpperBound;

	EssenceManager essenceTracker;

	float timer;

	public CircleCollider2D collider;
	public Rigidbody2D rigidbody;

	Vector2 offSet;
	bool offSetReached = false;


	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player").transform;
		timer = Random.Range(waitLowerBound, waitUpperBound);
		collider = gameObject.GetComponent<CircleCollider2D>();
		rigidbody = gameObject.GetComponent<Rigidbody2D>();
		essenceTracker = GameObject.Find("EssenceBackground").GetComponent<EssenceManager>();
		offSet = new Vector2(transform.position.x, (transform.position.y + Random.Range(-2, 2)));
	}
	
	// Update is called once per frame
	void Update () {

		timer -= Time.deltaTime;

		if (timer <= 0) 
		{
			collider.isTrigger = true;
			rigidbody.isKinematic = true;
			gameObject.layer = 15;

			if (!offSetReached) 
			{
			transform.position = Vector2.MoveTowards(transform.position, offSet, Time.deltaTime * modifier);
			}

			if (Vector2.Distance(offSet, transform.position) < 1) 
			{
				offSetReached = true;
			}

			if (offSetReached) 
			{
				transform.position = Vector2.MoveTowards(transform.position, player.position, Time.deltaTime * modifier);
			}
			
		}
	}

	void OnTriggerEnter2D (Collider2D other) 
	{
		if (other.gameObject.tag == "Player") 
		{
			essenceTracker.essence++;
			Destroy(gameObject);
		}
	}
}
