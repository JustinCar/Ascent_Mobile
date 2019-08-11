using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCombat : MonoBehaviour
{
    public UseAbility leftMB;
    public UseAbility rightMB;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activateLeft() 
    {
        leftMB.activated = true;
    }

    public void activateRight() 
    {
        rightMB.activated = true;
    }
}
