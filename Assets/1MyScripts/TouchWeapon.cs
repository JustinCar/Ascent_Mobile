using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchWeapon : MonoBehaviour
{

    public PlayerController playerController;
    // Start is called before the first frame update

    public void activateWeapon() 
    {
        playerController.weaponButtonPressed = true;
    }
}
