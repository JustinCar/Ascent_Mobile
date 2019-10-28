﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityStats : MonoBehaviour {

	public GameObject commonArt;
	public GameObject uncommonArt;
	public GameObject rareArt;
	public GameObject epicArt;
	public GameObject legendaryArt;
	public int manaCostLowerBound;
	public int manaCostUpperBound;

	public int damageLBLowerBound;
	public int damageLBUpperBound;
	public int damageUBUpperBound;
	public int damageUBLowerBound;
	public float coolDownLowerBound;
	public float coolDownUpperBound;
	public int specialEffectDamageLowerBound;
	public int specialEffectDamageUpperBound;
	public int specialEffectDurationLowerBound;
	public int specialEffectDurationUpperBound;	
	public float specialEffectRepeatLowerBound;
	public float specialEffectRepeatUpperBound;


	public int manaCost = 0;
	public int damageLowerBound = 0;
	public int damageUpperBound = 0;
	public float coolDown = 0.0f;
	public string abilityName = "None";
	public string specialEffectName = "None";
	public int specialEffectDamage = 0;
	public int specialEffectDuration = 0;
	public float specialEffectRepeat = 0.0f;
	public Sprite abilityIcon;
	public GameObject abilityItem;

	public bool newItem = true;

	LevelManager levelManager;

	public int rarity;


	// Use this for initialization
    void Start()
    {
			levelManager = GameObject.Find("Manager").GetComponent<LevelManager>();

			Debug.Log("ITEM INSTANTIATED: " + abilityName);

			if (newItem) 
			{
				Debug.Log("NEW ITEM: " + abilityName);
				manaCost = Random.Range(manaCostLowerBound, manaCostUpperBound);

				// Better items are available on higher levels
				damageLowerBound = Random.Range(damageLBLowerBound, damageLBUpperBound);
				damageLowerBound =  (int)(damageLowerBound * (levelManager.floorNumber));

				damageUpperBound = Random.Range(damageUBLowerBound, damageUBUpperBound);
				damageUpperBound =  (int)(damageUpperBound * (levelManager.floorNumber));

				coolDown = Random.Range(coolDownLowerBound, coolDownUpperBound);

				specialEffectDamage = Random.Range(specialEffectDamageLowerBound, specialEffectDamageUpperBound);
				specialEffectDamage =  (int)(specialEffectDamage * (levelManager.floorNumber));

				specialEffectDuration = Random.Range(specialEffectDurationLowerBound, specialEffectDurationUpperBound);

				// Time between effect ticks
				specialEffectRepeat = Random.Range(specialEffectRepeatLowerBound, specialEffectRepeatUpperBound);	


				// 1 = common
				// 2 = uncommon
				// 3 = rare
				// 4 = epic
				// 5 = legendary
				int randomNum = Random.Range(0, 101);

				if (randomNum >= 60) 
				{
					rarity = 1;
				} else if (randomNum >= 30 && randomNum < 60) 
				{
					rarity = 2;
				} else if (randomNum >= 15 && randomNum < 30) 
				{
					rarity = 3;
				} else if (randomNum >= 5 && randomNum < 15) 
				{
					rarity = 4;
				} else if (randomNum >= 0 && randomNum < 5) 
				{
					rarity = 5;
				}

				// Stats are further affected by their rarity	
				// uncommon is the base stat
				switch (rarity)
				{
					case 1:
						commonArt.SetActive(true);
						manaCost *= 2;
						damageLowerBound = (int)(damageLowerBound * 0.5);
						damageUpperBound = (int)(damageUpperBound * 0.5);
						coolDown *= 2;
						specialEffectDamage = (int)(specialEffectDamage * 0.5);
						specialEffectDuration = (int)(specialEffectDuration * 0.5);
						specialEffectRepeat *= 2;
						break;
					case 2:
						uncommonArt.SetActive(true);
						break;
					case 3:
						rareArt.SetActive(true);
						manaCost = (int)(manaCost * 0.75);
						damageLowerBound = (int)(damageLowerBound * 2);
						damageUpperBound = (int)(damageUpperBound * 2);
						coolDown = (int)(coolDown * 0.75);
						specialEffectDamage = (int)(specialEffectDamage * 2);
						specialEffectDuration = (int)(specialEffectDuration * 2);
						specialEffectRepeat = (int)(specialEffectRepeat * 0.75);
						break;
					case 4:
						epicArt.SetActive(true);
						manaCost = (int)(manaCost * 0.5);
						damageLowerBound = (int)(damageLowerBound * 3);
						damageUpperBound = (int)(damageUpperBound * 3);
						coolDown = (int)(coolDown * 0.5);
						specialEffectDamage = (int)(specialEffectDamage * 3);
						specialEffectDuration = (int)(specialEffectDuration * 3);
						specialEffectRepeat = (int)(specialEffectRepeat * 0.5);
						break;
					case 5:
						legendaryArt.SetActive(true);
						manaCost = (int)(manaCost * 0.25);
						damageLowerBound = (int)(damageLowerBound * 5);
						damageUpperBound = (int)(damageUpperBound * 5);
						coolDown = (int)(coolDown * 0.25);
						specialEffectDamage = (int)(specialEffectDamage * 5);
						specialEffectDuration = (int)(specialEffectDuration * 5);
						specialEffectRepeat = (int)(specialEffectRepeat * 0.25);
						break;
				}
				newItem = false;

			} else 
			{

				Debug.Log("OLD INSTANTIATED: " + abilityName);
				// Stats are further affected by their rarity	
				// uncommon is the base stat
				switch (rarity)
				{
					case 1:
						commonArt.SetActive(true);
						break;
					case 2:
						uncommonArt.SetActive(true);
						break;
					case 3:
						rareArt.SetActive(true);
						break;
					case 4:
						epicArt.SetActive(true);
						break;
					case 5:
						legendaryArt.SetActive(true);
						break;
				}
			}
    }





}
