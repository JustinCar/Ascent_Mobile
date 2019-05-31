using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingProjectile : MonoBehaviour {

    public Rigidbody m_Shell;                   // Prefab of the shell.
    public Transform m_FireTransform;           // A child of the tank where the shells are spawned.
    public float m_CurrentLaunchForce = 30f;        // The force given to the shell if the fire button is held for the max charge time.

    public float timeBetweenBullets = 0.15f;        // The time between each shot.
    public float timeToFirstBullet = 0.1f;         // The time to wait before firing first shot.

    float timer;                                    // A timer to determine when to fire.
    float timer2;
    ParticleSystem gunParticles;                    // Reference to the particle system.

    AudioSource gunAudio;                           // Reference to the audio source.
    Light gunLight;                                 // Reference to the light component.
    float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.

    void Awake()
    {

        // Set up the references.
        gunParticles = GetComponent <ParticleSystem> ();
        gunAudio = GetComponent <AudioSource> ();
        gunLight = GetComponent <Light> ();
    }

    void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;

        // If the Fire1 button is being pressed, it's time to fire...
        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && timer2 >= timeToFirstBullet)
        {

            // Can't shoot if game is paused
            if (PauseMenu.gameIsPaused)
            {
                return;
            }

            // ... shoot the gun.
            Shoot();
        }
        // Make sure the character is facing correct direction before firing.
        else if (!Input.GetButton("Fire1"))
        {
            // Reset the timer.
            timer2 = 0f;
        }

        // If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            // ... disable the effects.
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        // Disable the light.
        gunLight.enabled = false;
    }

    void Shoot()
    {
        // Reset the timer.
        timer = 0f;

        // Play the gun shot audioclip.
        gunAudio.Play();

        // Enable the light.
        gunLight.enabled = true;

        // Stop the particles from playing if they were, then start the particles.
        gunParticles.Stop();
        gunParticles.Play();

        Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

        shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;

    }
}
