using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour {

	public float distance = 0.5f;
	CanvasRenderer canvasRenderer;
	GameObject player;
	ItemPickupManager pickupManager;
	public bool healthItem;
	public int amount;
	PlayerHealth health;
	PlayerController controller;
	Animator anim;

	GameObject bubbles;

	public PlayerAudioManager audioManager;
	LevelManager levelManager;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		health = player.GetComponent<PlayerHealth>();
		controller = player.GetComponent<PlayerController>();
		canvasRenderer = GetComponentInChildren<CanvasRenderer>();
		pickupManager = GameObject.Find("Manager").GetComponent<ItemPickupManager>();
		audioManager = player.GetComponent<PlayerAudioManager>();

		levelManager = GameObject.Find("Manager").GetComponent<LevelManager>();

		amount =  (int)(amount * levelManager.floorNumber);
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector2.Distance(player.transform.position, transform.position) < distance) 
		{
			
			if (!pickupManager.itemsInRange.Contains(gameObject)) 
			{
				pickupManager.itemsInRange.Add(gameObject);
			}

			// Only able to pickup closest item
			if (pickupManager.closestItem == gameObject) 
			{
				canvasRenderer.gameObject.SetActive(true);

				if (Input.GetKeyDown(KeyCode.E)) 
				{
					consume();
				}
			}
		} else 
		{
			if (pickupManager.itemsInRange.Contains(gameObject)) 
			{
				pickupManager.itemsInRange.Remove(gameObject);
			}
			canvasRenderer.gameObject.SetActive(false);
		}
	}

	void consume () 
	{
		if (healthItem) 
		{
			health.increaseHealth(amount);
		} else 
		{
			health.increaseMana(amount);
		}
		controller.showBubbles();
		audioManager.buffAudio();

		Destroy(gameObject);
	}
}
