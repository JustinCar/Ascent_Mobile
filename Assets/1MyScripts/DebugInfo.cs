using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugInfo : MonoBehaviour
{

    GameObject debugInfo;
    bool active;
    // Start is called before the first frame update
    void Start()
    {
        debugInfo = GameObject.Find("[Graphy]");
        deactivate();
    }

    public void setDebugInfo() 
    {
        if (active)
        {
            debugInfo.SetActive(false); 
            active = false;
        } else 
        {
            debugInfo.SetActive(true); 
            active = true;           
        }
    }

    void deactivate() 
    {
        debugInfo.SetActive(false); 
        active = false;
    }

    public void activate() 
    {
        debugInfo.SetActive(true); 
        active = true;
    }

}
