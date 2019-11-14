using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSystem : MonoBehaviour {

	public List<GameObject> abilityItems;
	public List<GameObject> bodyParts;
	public GameObject snack;
	public GameObject meal;
	public GameObject smallManaPot;
	public GameObject LargeManaPot;
	public GameObject smallHealthPot;
	public GameObject LargeHealthPot;

	bool droppingItem = false;

	public int essenceVal;

	public float bodyPartsSpawnOffset;

	public int maxParts;

	// Use this for initialization
	void Awake () {

		int num = Random.Range(1, 1000);

		if (num < 100) 
		{
			droppingItem = true; // Roughly ten percent chance of spawning an item
		}
		droppingItem = true;

		if (droppingItem == true) 
		{
			int itemNum = Random.Range(1, 1400);

			if (itemNum <= 100 || itemNum > 1000 ) 
			{
				int index = Random.Range(0, abilityItems.Count);
				Instantiate(abilityItems[index], transform.position, abilityItems[index].transform.rotation); // Spawn an ability
			} else if (itemNum > 100 && itemNum <= 150) 
			{
				Instantiate(LargeHealthPot, transform.position, LargeHealthPot.transform.rotation); // Spawn a large health pot
			} else if (itemNum > 150 && itemNum <= 400) 
			{
				Instantiate(LargeManaPot, transform.position, LargeManaPot.transform.rotation); // Spawn a large mana pot
			} else if (itemNum > 400 && itemNum <= 500) 
			{
				Instantiate(smallHealthPot, transform.position, smallHealthPot.transform.rotation); // Spawn a small health pot
			} else if (itemNum > 500 && itemNum <= 700) 
			{
				Instantiate(smallManaPot, transform.position, smallManaPot.transform.rotation); // Spawn a small mana pot
			} else if (itemNum > 700 && itemNum <= 800) 
			{
				Instantiate(meal, transform.position, meal.transform.rotation); // Spawn a meal
			} else if (itemNum > 800 && itemNum <= 1000) 
			{
				Instantiate(snack, transform.position, snack.transform.rotation); // Spawn a snack
			}
		}
	}

	public void spawnBodyParts() 
	{
		int numPartsToSpawn = essenceVal / 2;

		Debug.Log("ESSENCE VALUE " + essenceVal);

		Debug.Log("SPAWNING " + numPartsToSpawn + " BODY PARTS");

		if (numPartsToSpawn > maxParts) 
		{
			numPartsToSpawn = maxParts;
		}

		for (int i = 0; i < numPartsToSpawn; i++) 
		{
			int index = Random.Range(0, bodyParts.Count);
			Vector2 pos = transform.position;

			float xOffSet = Random.Range(-bodyPartsSpawnOffset, bodyPartsSpawnOffset);
			float yOffSet = Random.Range(0, bodyPartsSpawnOffset);
			pos.x += xOffSet;
			pos.y += yOffSet;
			Instantiate(bodyParts[index], pos, bodyParts[index].transform.rotation);
		}
		Destroy(gameObject);
	}
}
