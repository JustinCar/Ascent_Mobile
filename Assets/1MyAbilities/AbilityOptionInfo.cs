using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityOptionInfo : MonoBehaviour {

	public UseAbility abilityScript;
	public int abilitySlot;
	AbilityStats[] scripts;
	public AbilityStats stats;
	public Text abilityNameTxt;
	public Text damageTxt;
	public Text manaTxt;
	public Text coolDownTxt;
	public Text specialEffectTxt;
	public Text specialEffectDamageTxt;
	public Text specialEffectDurationTxt;
	public Text specialEffectRepeatTxt;
	public Image icon;
	public GameObject foreground;

	// Use this for initialization
	void Start () {
		scripts = GameObject.Find("Player").GetComponentsInChildren<AbilityStats>();

		if (abilitySlot != 2 && scripts[abilitySlot])
		{
			if (scripts[abilitySlot].gameObject.GetComponent<UseAbility>().ability) 
			{
				foreground.gameObject.SetActive(false);
				stats = scripts[abilitySlot];

				Debug.Log ("slot" + abilitySlot + "   Name:" + stats.abilityName + stats);

				abilityNameTxt.text = stats.abilityName;
				damageTxt.text = stats.damageLowerBound + " - " + stats.damageUpperBound;
				manaTxt.text = "" + stats.manaCost;
				coolDownTxt.text = "" + stats.coolDown;
				specialEffectTxt.text = stats.specialEffectName;
				specialEffectDamageTxt.text = "" + stats.specialEffectDamage;
				specialEffectDurationTxt.text = "" + stats.specialEffectDuration;
				specialEffectRepeatTxt.text = "" + stats.specialEffectRepeat;
				icon.sprite = stats.abilityIcon;				
			}

		}
	}
}
