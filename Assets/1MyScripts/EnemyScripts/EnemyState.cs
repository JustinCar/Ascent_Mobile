using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Keep track of states that the enemy may be in
public class EnemyState : MonoBehaviour {

	public bool isEffected = false; // An enemy can only have one effect at a time 
	public bool burning = false;
	public bool frozen = false;
	public bool poisoned = false;
	public bool tentacles = false;
	public GameObject burningFX;
	public GameObject freezingFX;
	public GameObject poisonFX;
	public GameObject voidFX;
    public GameObject castFX;

	LevelManager manager;

	bool isDead;

	void Start () 
	{
		manager = GameObject.Find("Manager").GetComponent<LevelManager>();

		manager.levelComponents.Add(gameObject);
	}

	void Update () 
	{
		if (!isDead) 
		{
			if (burning || frozen || poisoned || tentacles) 
			{
				isEffected = true;
			} else if (!burning && !frozen && !poisoned && !tentacles) 
			{
				isEffected = false;
			}

			if (burning) 
			{
				burningFX.SetActive(true);
			} else 
			{
				burningFX.SetActive(false);
			}

			if (frozen) 
			{
				freezingFX.SetActive(true);
			} else
			{
				freezingFX.SetActive(false);
			}

			if (poisoned) 
			{
				poisonFX.SetActive(true);
			} else
			{
				poisonFX.SetActive(false);
			}

			if (tentacles) 
			{
				voidFX.SetActive(true);
			} else
			{
				voidFX.SetActive(false);
			}
		}
	}

	public void death () 
	{
		Debug.Log("HIDING EFFECTS");
        isDead = true;
		burning = false;
		burningFX.SetActive(false);
		frozen = false;
		freezingFX.SetActive(false);
		poisoned = false;
		poisonFX.SetActive(false);
		tentacles = false;
		voidFX.SetActive(false);

        if (castFX)
        {
            castFX.SetActive(false);
        }
	}


}
