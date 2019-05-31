using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour
{
    public GameObject controls;
    public GameObject spells;
    public GameObject spellEffects;
    public GameObject enemies;
    public GameObject biomes;
    public GameObject essence;
    public GameObject home;

    public GameObject mainMenu;

    void deactivateAll () 
    {
        controls.SetActive(false);
        spells.SetActive(false);
        spellEffects.SetActive(false);
        enemies.SetActive(false);
        biomes.SetActive(false);
        essence.SetActive(false);
        home.SetActive(false);
    }

    public void activateHome () 
    {
        deactivateAll();
        home.SetActive(true);
    }  

    public void quitGuide () 
    {
        deactivateAll();
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }  

    public void activateControls () 
    {
        deactivateAll();
        controls.SetActive(true);
    }  

    public void activateSpells () 
    {
        deactivateAll();
        spells.SetActive(true);
    }  

    public void activateSpellEffects () 
    {
        deactivateAll();
        spellEffects.SetActive(true);
    }  

    public void activateEnemies () 
    {
        deactivateAll();
        enemies.SetActive(true);
    }  

    public void activateBiomes () 
    {
        deactivateAll();
        biomes.SetActive(true);
    }  

    public void activateEssence () 
    {
        deactivateAll();
        essence.SetActive(true);
    }  
}
