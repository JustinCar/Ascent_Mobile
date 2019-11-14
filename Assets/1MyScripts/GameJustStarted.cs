using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameJustStarted : MonoBehaviour
{
    public bool justStarted = true;

    void Start() 
    {
        /* Mandatory - set your AppsFlyer’s Developer key. */
        AppsFlyer.setAppsFlyerKey ("zcKrZYJWnrWWctCxcLNnyT");
        /* For detailed logging */
        /* AppsFlyer.setIsDebug (true); */
        #if UNITY_IOS
            /* Mandatory - set your apple app ID
            NOTE: You should enter the number only and not the "ID" prefix */
            AppsFlyer.setAppID ("1484939499");
            AppsFlyer.trackAppLaunch();
        #elif UNITY_ANDROID
            /* Mandatory - set your Android package name */
            AppsFlyer.setAppID ("YOUR_ANDROID_PACKAGE_NAME_HERE");
            AppsFlyer.init("YOUR_APPSFLYER_DEV_KEY");
        #endif
    }
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
