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

    int enemyType;
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

        if (enemy.gameObject.name == "Enemy") 
        {  
            enemyType = 1;
            enemy.gameObject.GetComponent<EnemyController>().enabled = false;
        } else if (enemy.gameObject.name == "Goblin_Archer") 
        {  
            enemyType = 2;
            enemy.gameObject.GetComponent<GoblinArcherController>().enabled = false;
        }else if (enemy.gameObject.name == "Goblin_Grunt") 
        {  
            enemyType = 3;
            enemy.gameObject.GetComponent<GoblinGruntController>().enabled = false;
        }else if (enemy.gameObject.name == "Goblin_Shaman") 
        {  
            enemyType = 4;
            enemy.gameObject.GetComponent<GoblinSpellCasterController>().enabled = false;
        }else if (enemy.gameObject.name == "Goblin_Swordsman") 
        {  
            enemyType = 5;
            enemy.gameObject.GetComponent<GoblinSwordsmanController>().enabled = false;
        }
		
	}
	
	// Update is called once per frame
	void Update () {

        effectTimer += Time.deltaTime;
        totalEffectTimer += Time.deltaTime;
        enemyState.gameObject.transform.position = position;

        if (totalEffectTimer >= totalEffectTime) 
        {
            rigidBody.velocity = Vector3.zero;


            if (enemyType == 1) 
            {  
                enemy.gameObject.GetComponent<EnemyController>().enabled = true;
            } else if (enemyType == 2) 
            {  
                enemy.gameObject.GetComponent<GoblinArcherController>().enabled = true;
            }else if (enemyType == 3) 
            {  
                enemy.gameObject.GetComponent<GoblinGruntController>().enabled = true;
            }else if (enemyType == 4) 
            {  
                enemy.gameObject.GetComponent<GoblinSpellCasterController>().enabled = true;
            }else if (enemyType == 5) 
            {  
                enemy.gameObject.GetComponent<GoblinSwordsmanController>().enabled = true;
            }

			//enemy.GetComponent<EnemyController>().enabled = true;
			anim.enabled = true;
            enemyState.frozen = false;
            Destroy(gameObject);
        }
	}
}
