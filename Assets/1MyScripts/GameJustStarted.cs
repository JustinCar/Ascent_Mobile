using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameJustStarted : MonoBehaviour
{
    public bool justStarted = true;
    // Start is called before the first frame update
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("gamejuststarted");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
