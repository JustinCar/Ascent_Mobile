using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadStats : MonoBehaviour
{

	public int essence;        // The player's essence.
	public int healthModifier;        // The player's essence.
	public int healthCost;
	public int manaModifier;        // The player's essence.
	public int manaCost;
	public int damageModifier;        // The player's essence.
	public int damageCost;
	
    public Text essenceTxt; // Reference to the Essence text component.
	public Text healthModifierTxt; // Reference to the Essence text component.
	public Text healthCostTxt; // Reference to the Essence text component.

	public Text manaModifierTxt; // Reference to the Essence text component.
	public Text manaCostTxt; // Reference to the Essence text component.
	public Text damageModifierTxt; // Reference to the Essence text component.
	public Text damageCostTxt; // Reference to the Essence text component.

	// Use this for initialization
	void Start () {
        essence = SaveLoadManager.getEssence();
		healthModifier = SaveLoadManager.getHealthModifier();
		manaModifier = SaveLoadManager.getManaModifier();
		damageModifier = SaveLoadManager.getDamageModifier();
	}
	
	// Update is called once per frame
	void Update () {

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

	public void saveStats() 
	{
		SaveLoadManager.SetEssence(essence);
		SaveLoadManager.SetHealthModifier(healthModifier);
		SaveLoadManager.SetManaModifier(manaModifier);
		SaveLoadManager.SetDamageModifier(damageModifier);
	}

	public void increaseHealthModifier() 
	{
		if (essence - healthCost >= 0) 
		{
			essence -= healthCost;
			healthModifier += 1;	
			FaceBookEvents.LogUpgradePurchasedEvent("HEALTH", healthModifier);
		}
	}

	public void increaseManaModifier() 
	{
		if (essence - manaCost >= 0) 
		{
			essence -= manaCost;
			manaModifier += 1;
			FaceBookEvents.LogUpgradePurchasedEvent("MANA", manaModifier);
		}
	}
	public void increaseDamageModifier() 
	{
		if (essence - damageCost >= 0) 
		{
			essence -= damageCost;
			damageModifier += 1;
			FaceBookEvents.LogUpgradePurchasedEvent("DAMAGE", damageModifier);
		}
	}
}
