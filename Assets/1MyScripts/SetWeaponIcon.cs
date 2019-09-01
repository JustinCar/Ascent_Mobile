using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetWeaponIcon : MonoBehaviour
{
    public Image weaponIcon;
    public Sprite bow;
    public Sprite martial;
    public Sprite sword;

    // Start is called before the first frame update
    void Start()
    {
        if (SaveLoadManager.getFightingStyle() == 0) 
        {
            weaponIcon.sprite = bow;
        } else if (SaveLoadManager.getFightingStyle() == 1) 
        {
            weaponIcon.sprite = martial;
        } else if (SaveLoadManager.getFightingStyle() == 2) 
        {
            weaponIcon.sprite = sword;
        }
    }
}
