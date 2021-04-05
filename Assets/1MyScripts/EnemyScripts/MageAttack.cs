using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAttack : MonoBehaviour
{
	public Transform attackPos;
	public GameObject spell;
    LevelManager levelManager;
    PlayerAudioManager audioManager;
    public Animator anim;
    public float attackTimer = 0; // Timer to track attack cooldown
	public float attackCooldown; // The time between attacks
    EnemyHealth Health;

    void Awake()
    {
        levelManager = GameObject.Find("Manager").GetComponent<LevelManager>();
        audioManager = GameObject.Find("Player").GetComponent<PlayerAudioManager>();
        attackTimer = 0;
        Health = gameObject.GetComponentInParent<EnemyHealth>();
    }

    void Update()
    {
        attackTimer -= Time.deltaTime;
    }

	void spellAttack() 
	{
        if (spell)
        {
            audioManager.spellCastAudio();
            for (float i = 0; i < 3; i++)
            {
                GameObject abilityInstance = Instantiate(spell, attackPos.transform.position, spell.transform.rotation) as GameObject;
                MageBolt Bolt = abilityInstance.GetComponent<MageBolt>();
                float OffSet = i / 10;
                Bolt.initialCooldown = 0 + OffSet;
                if(Health.playerToLeft())
                {
                    Bolt.direction = -1;
                    Vector3 scale = abilityInstance.transform.localScale;
                    scale.x *= -1;
                    abilityInstance.transform.localScale = scale;
                }  
            }
            
        }
	}

    // Called from attack animation when attack has finished
    void attackFinished() 
    {
        if (anim.GetBool("isAttacking")) 
        {
            anim.SetBool("isAttacking", false);
            attackTimer = attackCooldown;
        } 
    }

    void destroy() 
    {
        Destroy(gameObject);
    }
}
