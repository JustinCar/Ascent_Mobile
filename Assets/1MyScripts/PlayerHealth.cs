using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public int startingHealth; // The amount of health the player starts the game with.
    public int startingMana;
    public int currentHealth; // The current health the player has.
    public int currentMana;
    AudioSource playerAudio; // Reference to the AudioSource component.
    PlayerController playerController; // Reference to the player's movement.
    bool isDead; // Whether the player is dead.

    public Material health;
    public Material mana;
    public Animator anim;
    public float knockPower;
    public Rigidbody2D rigidbody;
    public GameObject deathOverlay;

    float deathTimer = 8f;

    bool damaged = false;
    public Image damageImage;     
    public float flashSpeed = 10000f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.
    public AudioClip playerHurtAudio;   

    DebugInfo debugInfo;   


    void Awake()
    {
        startingHealth = (int)(100f * (1f + (SaveLoadManager.getHealthModifier() / 100f)));
        startingMana = (int)(100f * (1f + (SaveLoadManager.getManaModifier() / 100f)));
        // Setting up the references.
        playerAudio = GetComponent <AudioSource> ();
        playerController = GetComponent <PlayerController> ();

        // Set the initial health & mana of the player.
        currentHealth = startingHealth;
        currentMana = startingMana;

        health.SetFloat("_Progress", convertRange(currentHealth, startingHealth));
        mana.SetFloat("_Progress", convertRange(currentMana, startingMana));

        //debugInfo = GameObject.Find("DebugInfoToggle").GetComponent<DebugInfo>();
    }

    void Update()
    {
        if (isDead) 
        {
            deathTimer -= Time.deltaTime;
            rigidbody.velocity = Vector3.zero;
            deathOverlay.SetActive(true);

            if (deathTimer <= 0) 
            {
                //debugInfo.activate();
                SceneManager.LoadScene("Main Menu (Mobile)");
            }
        }

        // If the player has just been damaged...
        if (damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        // Reset the damaged flag.
        damaged = false;
        
    }

    public void reduceMana(int amount) 
    {
        currentMana -= amount;
        mana.SetFloat("_Progress", convertRange(currentMana, startingMana));
    }

    public void increaseHealth(int amount) 
    {
        currentHealth += amount;

        if (currentHealth > startingHealth) 
        {
            currentHealth = startingHealth;
        }
        health.SetFloat("_Progress", convertRange(currentHealth, startingHealth));
    }

    public void increaseMana(int amount) 
    {
        currentMana += amount;

        if (currentMana > startingMana) 
        {
            currentMana = startingMana;
        }
        mana.SetFloat("_Progress", convertRange(currentMana, startingMana));
    }

    public void takeDamage(int amount, bool toRight)
    {

        if (!isDead) 
        {
            damaged = true;
            
            playerAudio.PlayOneShot(playerHurtAudio, 1);

            // Reduce the current health by the damage amount.
            currentHealth -= amount;

            health.SetFloat("_Progress", convertRange(currentHealth, startingHealth));

            anim.SetInteger("AnimState", 0);
            anim.SetTrigger("Hurt");
            playerController.combo = false;
            playerController.attacking = false;

            rigidbody.velocity = Vector2.zero;

            if (toRight) 
            {
                rigidbody.AddForce(new Vector2(-knockPower, 0));
            }
            if(!toRight) 
            {
                rigidbody.AddForce(new Vector2(knockPower, 0));
            }
        }

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0 && !isDead)
        {
            // ... it should die.
            death();
        }
    }


    void death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;
        Debug.Log("Dead");

        anim.SetInteger("AnimState", 0);
        anim.SetTrigger("Dead");

        SaveLoadManager.SetEssence(GameObject.Find("EssenceBackground").GetComponent<EssenceManager>().essence);
        
        // Turn off the movement and shooting scripts.
        playerController.enabled = false;
    }

    // Converts a range of numbers to a range of 0 - 1
    // While maintaining the ratio
    float convertRange(float value, float startingValue) 
    {
        return (((value - 0) * (1 - 0)) / (startingValue - 0)) + 0;
    }
}
