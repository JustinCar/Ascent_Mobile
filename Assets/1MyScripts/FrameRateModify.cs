using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateModify : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Run at highest possible frame rate
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 300;
    }
}
