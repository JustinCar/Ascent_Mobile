using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    AudioSource playerAudio; // Reference to the AudioSource component.

    public List<AudioClip> footSteps;

    public List<AudioClip> swordAttacks;
    public List<AudioClip> missedMartialAttacks;
    public List<AudioClip> martialAttacks;

    public List<AudioClip> buffs;

    public List<AudioClip> phases;

    public AudioClip pickupClip; 
    public AudioClip explosionClip; 

    public AudioClip laserClip; 
    public AudioClip spellPrepareClip; 

    public List<AudioClip> spellCasts;
    public List<AudioClip> fireHitClip; 
    public List<AudioClip> poisonHitClip; 
    public List<AudioClip> iceHitClip; 
    public List<AudioClip> voidHitClip; 

    public List<AudioClip> arrowLooseClip;

    public List<AudioClip> arrowHitClip;

    public AudioClip gruntFootStepsClip;
    
    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent <AudioSource> ();
    }

    public void martialAttackAudio() 
    {
        int index = Random.Range(0, martialAttacks.Count);
        playerAudio.PlayOneShot(martialAttacks[index], 1);
    }

    public void missedMartialAttackAudio() 
    {
        int index = Random.Range(0, missedMartialAttacks.Count);
        playerAudio.PlayOneShot(missedMartialAttacks[index], 1);
    }

    // Play random sword attack sound
    public void swordAttackAudio() 
    {
        int index = Random.Range(0, swordAttacks.Count);
        playerAudio.PlayOneShot(swordAttacks[index], 1);
    }

    public void footStepAudio() 
    {
        int index = Random.Range(0, footSteps.Count);
        playerAudio.PlayOneShot(footSteps[index], 1f);
    }

    public void phaseAudio() 
    {
        Debug.Log("AUDIO PLAYED");
        int index = Random.Range(0, phases.Count);
        playerAudio.PlayOneShot(phases[index], 1);
    }

    public void buffAudio() 
    {
        int index = Random.Range(0, buffs.Count);
        playerAudio.PlayOneShot(buffs[index], 1);
    }

    public void pickupAudio() 
    {
        playerAudio.PlayOneShot(pickupClip, 1);
    }

    public void explosionAudio() 
    {
        playerAudio.PlayOneShot(explosionClip, 1);
    }

    public void spellCastAudio() 
    {
        int index = Random.Range(0, spellCasts.Count);
        playerAudio.PlayOneShot(spellCasts[index], 1);
    }

    public void spellPrepareAudio() 
    {
        playerAudio.PlayOneShot(spellPrepareClip, 1);
    }
    
    public void fireHitAudio() 
    {
        int index = Random.Range(0, fireHitClip.Count);
        playerAudio.PlayOneShot(fireHitClip[index], 1);
    }

    public void poisonHitAudio() 
    {
        int index = Random.Range(0, poisonHitClip.Count);
        playerAudio.PlayOneShot(poisonHitClip[index], 1);
    }
    public void iceHitAudio() 
    {
        int index = Random.Range(0, iceHitClip.Count);
        playerAudio.PlayOneShot(iceHitClip[index], 1);
    }
    public void voidHitAudio() 
    {
        int index = Random.Range(0, voidHitClip.Count);
        playerAudio.PlayOneShot(voidHitClip[index], 1);
    }

    public void arrowHitAudio() 
    {
        int index = Random.Range(0, arrowHitClip.Count);
        playerAudio.PlayOneShot(arrowHitClip[index], 1);
    }

    public void arrowLooseAudio() 
    {
        int index = Random.Range(0, arrowLooseClip.Count);
        playerAudio.PlayOneShot(arrowLooseClip[index], 0.7f);
    }

    public void laserAudio() 
    {
        playerAudio.PlayOneShot(laserClip, 1);
    }

    public void gruntFootStepsAudio() 
    {
        playerAudio.PlayOneShot(gruntFootStepsClip, 0.5f);
    }

}
