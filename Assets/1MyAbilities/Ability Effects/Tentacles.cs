using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacles : MonoBehaviour {

	Effect effectInfo;

    public EnemyHealth enemy;
    public EnemyState enemyState;
    public float totalEffectTime; // The overall duration of the effect
    public float totalEffectTimer;	
    public float effectTime; // The timer between damage ticks of the effect
    public float effectTimer;
    public int specialEffectDamage;
    public bool travelingLeft = false;

	public float atkRange;

	public LayerMask enemyLayer;

    
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

        enemyState.tentacles = true;    
	}
	
	// Update is called once per frame
	void Update () {

        effectTimer += Time.deltaTime;
        totalEffectTimer += Time.deltaTime;

        if (totalEffectTimer >= totalEffectTime) 
        {
            enemyState.tentacles = false;
            Destroy(gameObject);
        }

        if (effectTimer >= effectTime) 
        {
            effectTimer = 0;
            attack();           
        }
	}

	void attack () 
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(enemy.transform.position, atkRange, enemyLayer);
        if (enemies.Length > 0) 
        {   
            foreach (Collider2D c in enemies) 
            {
                c.gameObject.GetComponent<EnemyHealth>().TakeDamage(specialEffectDamage, true, false, 0);
            }
            
        }
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(enemy.transform.position, atkRange);
    }
}
