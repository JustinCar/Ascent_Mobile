using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityStats : MonoBehaviour {
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


	// Use this for initialization
    void Start()
    {
			levelManager = GameObject.Find("Manager").GetComponent<LevelManager>();

			if (newItem) 
			{
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
				specialEffectRepeat = Random.Range(specialEffectRepeatLowerBound, specialEffectRepeatUpperBound);			
			}
    }





}
