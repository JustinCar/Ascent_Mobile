using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfJustStarted : MonoBehaviour
{
    public Michsky.UI.Dark.SplashScreenManager splashScreen;
    public GameJustStarted script;
    // Start is called before the first frame update
    void Start()
    {

        script = GameObject.FindGameObjectWithTag("gamejuststarted").GetComponent<GameJustStarted>();
        if (!script.justStarted) 
        {
            splashScreen.disableSplashScreen = true;
        }
    }
}
