using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour {

	public float distance = 0.5f;
	CanvasRenderer canvasRenderer;
	GameObject canvas;
	GameObject player;
	public GameObject abilityOptions;
	public GameObject ability;
	public bool showAbilityOptions = false;
	ItemPickupManager pickupManager;

	float timer = 0.2f;

	GameObject abilityOptionsInstance = null;

	// Use this for initialization
	void Start () {
		canvasRenderer = GetComponentInChildren<CanvasRenderer>();
		player = GameObject.Find("Player");
		canvas = GameObject.Find("Canvas");
		pickupManager = GameObject.Find("Manager").GetComponent<ItemPickupManager>();
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

				if (showAbilityOptions) 
				{
					timer -= Time.deltaTime;
				}

				if (Input.GetKeyDown(KeyCode.E)) 
				{
					showAbilityOptions = true;
					player.GetComponent<PlayerController>().enabled = false; // Disable movement and attacking
					player.GetComponent<PlayerController>().rigidBody.velocity = Vector3.zero;
				}

				if (Input.GetKeyDown(KeyCode.E) && showAbilityOptions && timer <= 0) 
				{
					showAbilityOptions = false;
					timer = 0.2f;
				}
			}
		} else 
		{
			if (pickupManager.itemsInRange.Contains(gameObject)) 
			{
				pickupManager.itemsInRange.Remove(gameObject);
			}
			canvasRenderer.gameObject.SetActive(false);
			showAbilityOptions = false;
		}

		if (!showAbilityOptions && abilityOptionsInstance) 
		{
			Destroy(abilityOptionsInstance);
			abilityOptionsInstance = null;
			player.GetComponent<PlayerController>().enabled = true; // Disable movement and attacking
		}
		if (showAbilityOptions && !abilityOptionsInstance)
		{
			abilityOptionsInstance = Instantiate(abilityOptions) as GameObject;
			abilityOptionsInstance.transform.SetParent(canvas.transform, false);
			abilityOptionsInstance.GetComponent<CloseAbilityOptions>().itemPickupScript = this;

			AbilityStats stats;
			if (gameObject.GetComponent<AbilityStats>()) 
			{
				stats = gameObject.GetComponent<AbilityStats>();
			} else 
			{
				stats = ability.GetComponent<AbilityStats>();	
			}
			
			SwapAbility left;
			SwapAbility right;

			AbilityOptionInfo[] scripts = abilityOptionsInstance.GetComponentsInChildren<AbilityOptionInfo>();
			SwapAbility[] swapScripts = abilityOptionsInstance.GetComponentsInChildren<SwapAbility>();

			foreach (AbilityOptionInfo info in scripts) 
			{
				if (info.abilitySlot == 2) 
				{
					info.abilityNameTxt.text = stats.abilityName;
					info.damageTxt.text = stats.damageLowerBound + " - " + stats.damageUpperBound;
					info.manaTxt.text = "" + stats.manaCost;
					info.coolDownTxt.text = "" + stats.coolDown;
					info.specialEffectTxt.text = stats.specialEffectName;
					info.specialEffectDamageTxt.text = "" + stats.specialEffectDamage;
					info.specialEffectDurationTxt.text = "" + stats.specialEffectDuration;
					info.specialEffectRepeatTxt.text = "" + stats.specialEffectRepeat;
					info.icon.sprite = stats.abilityIcon;
				}	
			}

			foreach (SwapAbility s in swapScripts) 
			{
				if (s.gameObject.name == "LMBAbilityButton") 
				{
					left = s;
					left.ability = ability;
					left.stats = stats;
					left.item = gameObject;
					left.abilityMenu = abilityOptionsInstance;
				} else if (s.gameObject.name == "RMBAbilityButton") 
				{
					right = s;
					right.ability = ability;
					right.stats = stats;
					right.item = gameObject;
					right.abilityMenu = abilityOptionsInstance;
				}
			}
		}
	}
}
