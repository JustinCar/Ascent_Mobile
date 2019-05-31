using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour {

	public EnemyHealth enemy;
    public EnemyState enemyState;
	public AbilityStats stats;
    public float totalEffectTime; // The overall duration of the effect
    public float totalEffectTimer;	
    public float effectTime; // The timer between damage ticks of the effect
    public float effectTimer;
    public int specialEffectDamage;
    public bool travelingLeft = false;

	void Start () {

        if (enemyState.isEffected) 
        {
            Destroy(gameObject); // If a special effect is already active, delete the effect object
        }

		specialEffectDamage = stats.specialEffectDamage;
        totalEffectTime = stats.specialEffectDuration;
        effectTime = stats.specialEffectRepeat; 
	}
}
