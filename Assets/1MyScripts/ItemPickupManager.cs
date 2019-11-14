using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickupManager : MonoBehaviour {

	public List<GameObject> itemsInRange;
	public GameObject closestItem;
	float closestDistance;
	GameObject player;
	PlayerHealth health;

	public GameObject pickupButton;

	UseAbility leftSlot;
	UseAbility rightSlot;

	// Use this for initialization
	void Start () {
		itemsInRange = new List<GameObject>();
		player = GameObject.Find("Player");
		leftSlot = GameObject.FindGameObjectWithTag("LMBAbilitySlot").GetComponent<UseAbility>();
		rightSlot = GameObject.FindGameObjectWithTag("RMBAbilitySlot").GetComponent<UseAbility>();
		health = player.GetComponent<PlayerHealth>();
	}
	
	// Update is called once per frame
	void Update () {

		if (itemsInRange.Count > 0) 
		{
			pickupButton.SetActive(true);
		} else 
		{
			pickupButton.SetActive(false);
		}

		if (itemsInRange.Count == 1) 
		{
			if (itemsInRange[0] == null) 
			{
				itemsInRange.RemoveAt(0);
			} else 
			{
				closestItem = itemsInRange[0];
				closestDistance = Vector3.Distance(itemsInRange[0].transform.position, player.transform.position);				
			}

			autoPickupConsumeable();
			autoPickupSpell();
		}

		if (itemsInRange.Count >= 2) 
		{
			foreach (GameObject item in itemsInRange) 
			{
				if (item == null) 
				{
					itemsInRange.Remove(item);
				} else 
				{
					if (Vector3.Distance(item.transform.position, player.transform.position) < closestDistance) 
					{
						closestItem = item;
						closestDistance = Vector3.Distance(item.transform.position, player.transform.position);
					}				
				}
			}
			autoPickupConsumeable();
			autoPickupSpell(); 
		}
	}

	void autoPickupConsumeable()
	{
		UseItem item = closestItem.GetComponent<UseItem>();
		if (item)
		{
			if (item.healthItem && item.amount <= (health.startingHealth - health.currentHealth)) 
			{
				item.consume();
			} else if (!item.healthItem && item.amount <= (health.startingMana - health.currentMana)) 
			{
				item.consume();
			}
		}
	}

	void autoPickupSpell() 
	{
		ItemPickup spell = closestItem.GetComponent<ItemPickup>();

		if (spell) 
		{
			if (!leftSlot.ability) 
			{
				pickupSpell(spell, true);

			} else if (!rightSlot.ability) 
			{
				pickupSpell(spell, false);
			}
		}
	}

	public void pickupSpell(ItemPickup spellItem, bool slotIsLeft) 
	{
		AbilityStats itemToBePickedUpStats;
		UseAbility abilitySlot;
		GameObject ability = spellItem.ability;
		AbilityStats stats;
		Image abilityPanel;

		GameObject player;

		PlayerAudioManager audioManager;

		itemToBePickedUpStats = ability.GetComponent<AbilityStats>();

		if (slotIsLeft) 
		{
			abilitySlot = GameObject.FindGameObjectWithTag("LMBAbilitySlot").GetComponent<UseAbility>();
			stats = spellItem.GetComponent<AbilityStats>();
			abilityPanel = GameObject.FindGameObjectWithTag("LMBSprite").GetComponent<Image>();
		} else
		{
			abilitySlot = GameObject.FindGameObjectWithTag("RMBAbilitySlot").GetComponent<UseAbility>();
			stats = spellItem.GetComponent<AbilityStats>();
			abilityPanel = GameObject.FindGameObjectWithTag("RMBSprite").GetComponent<Image>();
		}

		player = GameObject.Find("Player");
		audioManager = player.GetComponent<PlayerAudioManager>();
		abilityPanel.sprite = itemToBePickedUpStats.abilityIcon;

		audioManager.pickupAudio();

		FaceBookEvents.LogItemPickedUpOrConsumedEvent(itemToBePickedUpStats.abilityName);
		
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
		Destroy(spellItem.gameObject);
	}

	public void pickupClosestItem() 
	{
		if (closestItem.GetComponent<ItemPickup>()) 
		{
			closestItem.GetComponent<ItemPickup>().showAbilityOptions = true;
		} else 
		{
			closestItem.GetComponent<UseItem>().consume();
		}
	}
}
