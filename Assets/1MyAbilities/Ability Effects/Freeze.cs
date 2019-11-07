using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour {

	Effect effectInfo;

    public EnemyHealth enemy;
    public EnemyState enemyState;
    public float totalEffectTime; // The overall duration of the effect
    public float totalEffectTimer;	
    public float effectTime; // The timer between damage ticks of the effect
    public float effectTimer;
    public int specialEffectDamage;
    public bool travelingLeft = false;

    Rigidbody2D rigidBody;
    public Animator anim;
    Vector2 position;

    
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

        enemyState.frozen = true;  
		rigidBody = enemy.GetComponent<Rigidbody2D>();
		anim = enemy.anim; 

        anim.enabled = false;
        rigidBody.velocity = Vector3.zero;
        position = enemyState.gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        if (!enemyState.isDead) 
        {
            effectTimer += Time.deltaTime;
            totalEffectTimer += Time.deltaTime;
            enemyState.gameObject.transform.position = position;

            if (totalEffectTimer >= totalEffectTime) 
            {
                rigidBody.velocity = Vector3.zero;
                anim.enabled = true;
                enemyState.frozen = false;
                Destroy(gameObject);
            }

        }
        else 
        {
            Destroy(gameObject);
        }
	}
}
