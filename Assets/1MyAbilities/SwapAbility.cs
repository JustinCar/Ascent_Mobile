using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapAbility : MonoBehaviour {

	AbilityStats itemToBePickedUpStats;
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

		if (abilitySlot.ability) 
		{
			GameObject instance = Instantiate (abilitySlot.ability.GetComponent<AbilityStats>().abilityItem, player.transform.position, Quaternion.identity) as GameObject;
			AbilityStats instanceStats = instance.GetComponent<AbilityStats>();
			instanceStats.newItem = false;
            instanceStats = stats;
		}

		audioManager.pickupAudio();
		
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

		abilitySlot.resetStats();
		player.GetComponent<PlayerController>().enabled = true; // Enable movement and attacking
		Destroy(item);
		Destroy(abilityMenu);
	}
}
