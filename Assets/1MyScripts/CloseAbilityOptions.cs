using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAbilityOptions : MonoBehaviour
{

    public ItemPickup itemPickupScript;
    public void close () 
    {
        itemPickupScript.showAbilityOptions = false;
    }
}
