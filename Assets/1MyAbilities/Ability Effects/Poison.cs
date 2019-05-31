﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour {

	Effect effectInfo;

    public EnemyHealth enemy;
    public EnemyState enemyState;
    public float totalEffectTime; // The overall duration of the effect
    public float totalEffectTimer;	
    public float effectTime; // The timer between damage ticks of the effect
    public float effectTimer;
    public int specialEffectDamage;
    public bool travelingLeft = false;

    
	// Use this for initialization
	void Start () {
        effectInfo = gameObject.GetComponent<Effect>();

        enemy = effectInfo.enemy;
        enemyState = effectInfo.enemyState;

        totalEffectTime = effectInfo.totalEffectTime; 
        totalEffectTimer = effectInfo.totalEffectTimer;	
        effectTime = effectInfo.effectTime; 
        effectTimer = effectInfo.effectTimer;
        specialEffectDamage = effectInfo.specialEffectDamage;
        travelingLeft = effectInfo.travelingLeft;

        enemyState.poisoned = true;  
	}
	
	// Update is called once per frame
	void Update () {

        effectTimer += Time.deltaTime;
        totalEffectTimer += Time.deltaTime;

        if (totalEffectTimer >= totalEffectTime) 
        {
            enemyState.poisoned = false;
            Destroy(gameObject);
        }
	}
}