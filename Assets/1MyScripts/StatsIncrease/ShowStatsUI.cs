using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowStatsUI : MonoBehaviour {

	public float distance = 0.5f;
	public CanvasRenderer canvasRenderer;
	GameObject canvas;
	GameObject player;
	TownPlayerController controller;
	public GameObject StatsUI;
	public bool showStatsUI = false;
	float timer = 0.2f;


	public int essence;        // The player's essence.
	public int healthModifier;        // The player's essence.
	public int healthCost;
	public int manaModifier;        // The player's essence.
	public int manaCost;
	public int damageModifier;        // The player's essence.
	public int damageCost;
	public bool UIUpdated = false;
	
    public Text essenceTxt; // Reference to the Essence text component.
	public Text healthModifierTxt; // Reference to the Essence text component.
	public Text healthCostTxt; // Reference to the Essence text component.

	public Text manaModifierTxt; // Reference to the Essence text component.
	public Text manaCostTxt; // Reference to the Essence text component.
	public Text damageModifierTxt; // Reference to the Essence text component.
	public Text damageCostTxt; // Reference to the Essence text component.

	// Use this for initialization
	void Start () {
		player = GameObject.Find("TownPlayer");
		canvas = GameObject.Find("Canvas");
		controller = player.GetComponent<TownPlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector2.Distance(player.transform.position, transform.position) < distance) 
		{
			canvasRenderer.gameObject.SetActive(true);

			if (showStatsUI) 
			{
				timer -= Time.deltaTime;
			}

			if (Input.GetKeyDown(KeyCode.E) && !showStatsUI) 
			{
				showStatsUI = true;
				controller.enabled = false; // Disable movement
				controller.rigidBody.velocity = Vector3.zero;
			}

			if (Input.GetKeyDown(KeyCode.E) && showStatsUI && timer <= 0) 
			{
				showStatsUI = false;
				timer = 0.2f;
				SaveLoadManager.SetEssence(essence);
				SaveLoadManager.SetHealthModifier(healthModifier);
				SaveLoadManager.SetManaModifier(manaModifier);
				SaveLoadManager.SetDamageModifier(damageModifier);
				StatsUI.SetActive(false);
				controller.enabled = true; // Disable movement
				UIUpdated = false;
			}
		} else 
		{
			canvasRenderer.gameObject.SetActive(false);
		}

		if (showStatsUI)
		{
			if (UIUpdated == false) 
			{
				// Set the amount of essence
				essence = SaveLoadManager.getEssence();
				healthModifier = SaveLoadManager.getHealthModifier();
				manaModifier = SaveLoadManager.getManaModifier();
				damageModifier = SaveLoadManager.getDamageModifier();
				StatsUI.SetActive(true);
				controller.enabled = false; // Disable movement

				UIUpdated = true;
			}

			healthCost = healthModifier * 2;
			manaCost = manaModifier * 2;
			damageCost = damageModifier * 2;

			essenceTxt.text = "" + essence;
			healthModifierTxt.text = "+" + healthModifier + "%";
			healthCostTxt.text = "" + healthCost;

			manaModifierTxt.text = "+" + manaModifier + "%";
			manaCostTxt.text = "" + manaCost;

			damageModifierTxt.text = "+" + damageModifier + "%";
			damageCostTxt.text = "" + damageCost;
		}
	}

	public void increaseHealthModifier() 
	{
		if (essence - healthCost >= 0) 
		{
			essence -= healthCost;
			healthModifier += 1;	
		}

	}

	public void increaseManaModifier() 
	{
		if (essence - manaCost >= 0) 
		{
			essence -= manaCost;
			manaModifier += 1;
		}
	}
	public void increaseDamageModifier() 
	{
		if (essence - damageCost >= 0) 
		{
			essence -= damageCost;
			damageModifier += 1;
		}
	}
}
