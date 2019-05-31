using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject normalEnemy;
	public GameObject goblinArcher;
	public GameObject goblinSwordsman;
	public GameObject goblinShaman;
	public GameObject goblinGrunt;

	// Use this for initialization
	void Start () {
		
		// Spawn an enemy 50 percent of the time
		if (Random.Range(0, 10) < 5) 
		{
			int randomNum = Random.Range(0, 1000);

			if (randomNum <= 250) 
			{
				Instantiate(normalEnemy, transform.position, normalEnemy.transform.rotation);
			} else if (randomNum > 250 && randomNum <= 500) 
			{
				Instantiate(goblinArcher, transform.position, goblinArcher.transform.rotation);
			} else if (randomNum > 500 && randomNum <= 700) 
			{
				Instantiate(goblinSwordsman, transform.position, goblinSwordsman.transform.rotation);
			} else if (randomNum > 700 && randomNum <= 850) 
			{
				Instantiate(goblinShaman, transform.position, goblinShaman.transform.rotation);
			} else if (randomNum > 850 && randomNum <= 1000) 
			{
				Instantiate(goblinGrunt, transform.position, goblinGrunt.transform.rotation);
			}

		}

	}
}
