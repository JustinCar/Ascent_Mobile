using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatter : MonoBehaviour
{

    LevelManager manager;
    public bool attachedToPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<LevelManager>();

		manager.levelComponents.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
