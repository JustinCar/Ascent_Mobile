using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EssenceManager : MonoBehaviour {

    public int essence;        // The player's essence.
    public Text text;          // Reference to the Text component.


    void Awake()
    {
        // Set up the reference.
        text = GetComponentInChildren<Text>();

        // Set the amount of essence
        essence = SaveLoadManager.getEssence();
        
    }

    void Update()
    {
        // Set the displayed text to be the word "Score" followed by the score value.
        text.text = "" + essence;
    }
}
