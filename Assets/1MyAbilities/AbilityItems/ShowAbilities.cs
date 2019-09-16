using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAbilities : MonoBehaviour
{

    public GameObject abilityInfo;
    public GameObject instance;
    public bool showing = false;
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showInfo() 
    {
        if (!showing) 
        {
            instance = Instantiate(abilityInfo) as GameObject;
            instance.transform.SetParent(canvas.transform, false);
            showing = true;
        } else 
        {
            Destroy(instance);
            showing = false;
        }
    }
}
