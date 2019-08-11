using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseAbility : MonoBehaviour {

    public GameObject ability;                    
    public float timeBetweenAttacks;        // The time between each shot.
    float timer;                                    // A timer to determine when to fire.               

    // AudioSource gunAudio;                           // Reference to the audio source.
    // Light gunLight;                                 // Reference to the light component.

    public PlayerController playerCtrl;
    public PlayerHealth playerHealth;
    public Animator anim;
    public string button;

    public GameObject noManaTxt;
    float noManatxtTimer = 1f;
    bool showNoManaTxt = false;

    public SimpleHealthBar coolDownBar;

    int manaCost;

    public PlayerAudioManager audioManager;

    public bool activated = false;

    void Awake()
    {
        timer = timeBetweenAttacks;

        if (button == "Fire1") 
        {
            coolDownBar = GameObject.FindGameObjectWithTag("LMB").GetComponentInChildren<SimpleHealthBar>();
        } else if (button == "Fire2") 
        {
            coolDownBar = GameObject.FindGameObjectWithTag("RMB").GetComponentInChildren<SimpleHealthBar>();
        }

        if (ability) 
        {
            manaCost = gameObject.GetComponent<AbilityStats>().manaCost;
            timeBetweenAttacks = gameObject.GetComponent<AbilityStats>().coolDown;  
        }


        // Set up the references.
        // gunParticles = GetComponent <ParticleSystem> ();
        // gunAudio = GetComponent <AudioSource> ();
        // gunLight = GetComponent <Light> ();
    }

    public void resetStats() 
    {
        manaCost = gameObject.GetComponent<AbilityStats>().manaCost;
        timeBetweenAttacks = gameObject.GetComponent<AbilityStats>().coolDown;  
    }

    void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        if (coolDownBar && timer <= timeBetweenAttacks && ability) 
        {
            coolDownBar.UpdateBar(timeBetweenAttacks - timer, timeBetweenAttacks); 
        }
        

        // If the Fire1 button is being pressed, it's time to fire...
        // if (Input.GetButton(button) && timer >= timeBetweenAttacks)
        // {
        
        if (activated && timer >= timeBetweenAttacks)
        {

            // // Can't shoot if game is paused
            // if (PauseMenu.gameIsPaused)
            // {
            //     return;
            // }
            activated = false;

            // ... shoot the gun.
            if (!((playerHealth.currentMana - manaCost) <= 0) && playerCtrl.enabled == true) 
            {
                shoot();  
            } else if (!showNoManaTxt && playerCtrl.enabled == true)
            {
                showNoManaTxt = true;
                noManaTxt.SetActive(true);
            }
        } else 
        {
            activated = false;
        } 

        if (showNoManaTxt) 
        {
            noManatxtTimer -= Time.deltaTime;

            if (noManatxtTimer <= 0) 
            {
                noManaTxt.SetActive(false);
                showNoManaTxt = false;
                noManatxtTimer = 1f;
            }
        }

    }

    void shoot()
    {
        // Reset the timer.
        timer = 0f;

        if (ability) 
        {
            audioManager.spellCastAudio();
            anim.SetTrigger("Cast");
            GameObject abilityInstance = Instantiate(ability, transform.position, transform.rotation) as GameObject;
            AbilityStats instanceStats = abilityInstance.GetComponent<AbilityStats>();

            AbilityStats stats = gameObject.GetComponent<AbilityStats>();

            instanceStats.newItem = stats.newItem;
            instanceStats.abilityName = stats.abilityName;
            instanceStats.damageLowerBound = stats.damageLowerBound;
            instanceStats.damageUpperBound = stats.damageUpperBound;
            instanceStats.manaCost = stats.manaCost;
            instanceStats.coolDown = stats.coolDown;
            instanceStats.specialEffectName = stats.specialEffectName;
            instanceStats.specialEffectDamage = stats.specialEffectDamage;
            instanceStats.specialEffectDuration = stats.specialEffectDuration;
            instanceStats.specialEffectRepeat = stats.specialEffectRepeat;
            instanceStats.abilityIcon = stats.abilityIcon;
            instanceStats.abilityItem = stats.abilityItem;

            playerHealth.reduceMana(manaCost);    

            if (playerCtrl.facingLeft) 
            {
                Vector3 scale = abilityInstance.transform.localScale;
                scale.x *= -1;
                abilityInstance.transform.localScale = scale;
            }
        }
    }
}
