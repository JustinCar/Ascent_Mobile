using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour {

    public int essenceValue;
    public GameObject floatingText;

    public bool canBeStunned = true; // Some enemies cannot be stunned (Goblin Grunt)

    public int startingHealth = 100;            // The amount of health the enemy starts the game with.
    public int currentHealth;                   // The current health the enemy has.
    float previousHealth;  
    public float sinkSpeed = 2.5f;              // The speed at which the enemy sinks through the floor when dead.  
    public Animator anim;                              // Reference to the animator.
    //AudioSource enemyAudio;                     // Reference to the audio source.
    public bool isDead;                                // Whether the enemy is dead.
    bool attacked = false;
    public float knockPower;
    public Rigidbody2D rb;
    public List<SimpleHealthBar> greenBars;
    public List<SimpleHealthBar> whiteBars;
    public Canvas canvas;
    float healthDecreasePerSecond = 10;
    EnemyState state;

    public bool stunned = false;
	public float stunTime;
	float stunTimer;
    public GameObject lootSpawner;

    LevelManager levelManager;

    float damageModifier;

    float deathTimer = 5f;
    public bool facingLeft = false;
    public float move = -1;
    public GameObject player;
    public GameObject sprite;

    public ParticleSystem bloodSpray;
    public Transform bloodSprayPos;

    void Awake()
    {
        levelManager = GameObject.Find("Manager").GetComponent<LevelManager>();
        essenceValue *= levelManager.floorNumber; // Essence value scales with the floor number
        startingHealth =  (int)(startingHealth * (levelManager.enemyHealthMultiplier + 1)); // The higher the level the more health the enemy has
        currentHealth = startingHealth;
        state = gameObject.GetComponent<EnemyState>();
        stunTimer = stunTime;

        damageModifier = 1f + (SaveLoadManager.getDamageModifier() / 100f);
        player = GameObject.Find("Player");
    }

    void Update()
    {

        if (isDead)
        {
            deathTimer -= Time.deltaTime;
            if (deathTimer <= 0) 
            {
                enabled = false;
            }
        }

        foreach (SimpleHealthBar b in greenBars) 
        {
            b.UpdateBar(currentHealth, startingHealth);
        }

        if (attacked) 
        {
            previousHealth -= healthDecreasePerSecond * Time.deltaTime;
            
            foreach (SimpleHealthBar b in whiteBars) 
            {
                b.UpdateBar(previousHealth, startingHealth);
            }

            if (previousHealth <= currentHealth) 
            {
                attacked = false;
            }
        }
        
        if (stunned) 
		{
			stunTimer -= Time.deltaTime;

			if (stunTimer <= 0) 
			{
				stunned = false;
			}
		}
    }


    public void TakeDamage(int amount, bool toRight, bool shouldStun, int damageType)
    {
        // If the enemy is dead...
        if (isDead)
        {
            // ... no need to take damage so exit the function.
            return;
        }

        Instantiate(bloodSpray, bloodSprayPos.transform.position, bloodSpray.transform.rotation);

        amount = (int) (amount * damageModifier); // Damage scales with the players damage modifier

        if (!anim.GetBool("isChasing")) 
        {
            anim.SetBool("isChasing", true);
        }
        

        // Damage taken is doubled if poisened
        if (state.poisoned) 
        {
            amount *= 2;
        }

        switch (levelManager.biome) 
        {
            // If biome is wastes
            case 1:
                // Increase fire damage
                if (damageType == 1) 
                {
                    amount = (int)(amount * 2);

                // Decrease ice damage
                } else if (damageType == 3)
                {
                    amount = (int)(amount * 0.5);
                }
            break;

            // If biome is wilds
            case 2:
                // Increase poison damage
                if (damageType == 2) 
                {
                    amount = (int)(amount * 2);

                // Decrease void damage
                } else if (damageType == 4)
                {
                    amount = (int)(amount * 0.5);
                }
            break;

            // If biome is tundra
            case 3:

                // Increase ice damage
                if (damageType == 3) 
                {
                    amount = (int)(amount * 2);

                // Decrease fire damage
                } else if (damageType == 1)
                {
                    amount = (int)(amount * 0.5);
                }
            break;

            // If biome is void
            case 4:

                // Increase void damage
                if (damageType == 4) 
                {
                    amount = (int)(amount * 2);

                // Decrease poison damage
                } else if (damageType == 2)
                {
                    amount = (int)(amount * 0.5);
                }
            break;

            case 0:
                // Damage type is neutral
                // No effect
            default:
                Debug.Log("ERROR ALTERING DAMAGE BASED ON BIOME");
              
            break;
        }

        // If health bars are inactive, set them active
        if (!canvas.gameObject.activeSelf) 
        {
            canvas.gameObject.SetActive(true);
        }

        if (shouldStun && canBeStunned) 
        {
            stun();

            if (toRight) 
            {
                rb.AddForce(new Vector2(-knockPower, knockPower));
            }
            if(!toRight) 
            {
                rb.AddForce(new Vector2(knockPower, knockPower));
            } 
        }

        previousHealth = currentHealth;

        // Reduce the current health by the amount of damage sustained.
        currentHealth -= amount;

        attacked = true;

        // If the current health is less than or equal to zero...
        if (currentHealth <= 0)
        {
            // ... the enemy is dead.
            Death();
        }

        if (floatingText)
        {
            showFloatingText(amount, toRight, shouldStun);
        }
    }
    public void stun() 
	{
		stunned = true;
		stunTimer = stunTime;
	}

    void showFloatingText(int amount, bool toRight, bool shouldStun)
    {
        Vector3 pos = gameObject.GetComponentInChildren<Canvas>().transform.position;
        GameObject text = Instantiate(floatingText, pos, Quaternion.identity, transform) as GameObject;
        text.gameObject.transform.SetParent(gameObject.GetComponentInChildren<Canvas>().transform);
        text.GetComponent<FloatingText>().damageOverTime = !shouldStun;
        text.GetComponent<TextMesh>().text = "" + amount;
    }

    void Death()
    {
        // The enemy is dead.
        isDead = true;

        spawnLoot();

        state.death();

        canvas.enabled = false;

        gameObject.layer = 13; // Change the enemies layer to FX, so that player cant interact with it anymore

        // Tell the animator that the enemy is dead.
        anim.SetBool("isDead", true);
    }

    public void spawnLoot () 
	{
		GameObject loot = Instantiate(lootSpawner, transform.position, lootSpawner.transform.rotation) as GameObject;
        LootSystem script = loot.GetComponent<LootSystem>();
        script.essenceVal = this.essenceValue;
        script.spawnBodyParts();
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
		Debug.Log("trigger");
        if (other.gameObject.tag == "PlatformEdge" && !anim.GetBool("isChasing")) 
		{
			Debug.Log("edge found");
			if (move == -1) 
			{
				move = 1;
			} else if (move == 1) 
			{
				move = -1;
			}
		}
        
    }

	public bool playerToLeft() 
		{
			if ((transform.position.x - player.transform.position.x) < 0) 
			{
				return false;
			}
			if ((transform.position.x - player.transform.position.x) > 0) 
			{
				return true;
			}
			Debug.Log("ERROR");
			return false;
		}

    void flip() 
	{
		facingLeft = !facingLeft;
		Vector3 scale = sprite.transform.localScale;
		scale.x *= -1;
		sprite.transform.localScale = scale;
	}
}
