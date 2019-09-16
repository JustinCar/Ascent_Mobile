using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapAbility : MonoBehaviour {

	public AbilityStats itemToBePickedUpStats;
	public UseAbility abilitySlot;
	public GameObject item;
	public GameObject ability;
	public AbilityStats stats;
	public Image abilityPanel;

	public GameObject abilityMenu;

	public GameObject player;

	public PlayerAudioManager audioManager;

	// Use this for initialization
	void Start () {
		itemToBePickedUpStats = ability.GetComponent<AbilityStats>();

		if (gameObject.name == "LMBAbilityButton") 
		{
			abilitySlot = GameObject.FindGameObjectWithTag("LMBAbilitySlot").GetComponent<UseAbility>();
			stats = item.GetComponent<AbilityStats>();
			abilityPanel = GameObject.FindGameObjectWithTag("LMBSprite").GetComponent<Image>();
		} else if (gameObject.name == "RMBAbilityButton") 
		{
			abilitySlot = GameObject.FindGameObjectWithTag("RMBAbilitySlot").GetComponent<UseAbility>();
			stats = item.GetComponent<AbilityStats>();
			abilityPanel = GameObject.FindGameObjectWithTag("RMBSprite").GetComponent<Image>();
		}

		player = GameObject.Find("Player");
		audioManager = player.GetComponent<PlayerAudioManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void swap() 
	{
		abilityPanel.sprite = itemToBePickedUpStats.abilityIcon;

		// Drop current item
		if (abilitySlot.ability) 
		{
			GameObject instance = Instantiate (abilitySlot.ability.GetComponent<AbilityStats>().abilityItem, player.transform.position, Quaternion.identity) as GameObject;
			AbilityStats instanceStats = instance.GetComponent<AbilityStats>();
			instanceStats.newItem = false;

            AbilityStats statsToDrop = abilitySlot.gameObject.GetComponent<AbilityStats>();

			instanceStats.newItem = false;
			instanceStats.abilityName = statsToDrop.abilityName;
			instanceStats.damageLowerBound = statsToDrop.damageLowerBound;
			instanceStats.damageUpperBound = statsToDrop.damageUpperBound;
			instanceStats.manaCost = statsToDrop.manaCost;
			instanceStats.coolDown = statsToDrop.coolDown;
			instanceStats.specialEffectName = statsToDrop.specialEffectName;
			instanceStats.specialEffectDamage = statsToDrop.specialEffectDamage;
			instanceStats.specialEffectDuration = statsToDrop.specialEffectDuration;
			instanceStats.specialEffectRepeat = statsToDrop.specialEffectRepeat;
			instanceStats.abilityIcon = statsToDrop.abilityIcon;
			instanceStats.abilityItem = statsToDrop.abilityItem;
			instanceStats.rarity = statsToDrop.rarity;
			
		}

		audioManager.pickupAudio();
		
		// pickup new item
		abilitySlot.ability = ability;
		AbilityStats instanceStats2 = abilitySlot.gameObject.GetComponent<AbilityStats>();

		instanceStats2.newItem = false;
		instanceStats2.abilityName = stats.abilityName;
		instanceStats2.damageLowerBound = stats.damageLowerBound;
		instanceStats2.damageUpperBound = stats.damageUpperBound;
		instanceStats2.manaCost = stats.manaCost;
		instanceStats2.coolDown = stats.coolDown;
		instanceStats2.specialEffectName = stats.specialEffectName;
		instanceStats2.specialEffectDamage = stats.specialEffectDamage;
		instanceStats2.specialEffectDuration = stats.specialEffectDuration;
		instanceStats2.specialEffectRepeat = stats.specialEffectRepeat;
		instanceStats2.abilityIcon = stats.abilityIcon;
		instanceStats2.abilityItem = stats.abilityItem;
		instanceStats2.rarity = stats.rarity;

		abilitySlot.resetStats();
		player.GetComponent<PlayerController>().enabled = true; // Enable movement and attacking
		Destroy(item);
		Destroy(abilityMenu);
	}
}
