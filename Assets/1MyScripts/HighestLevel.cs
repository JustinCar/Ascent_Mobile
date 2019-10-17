using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighestLevel : MonoBehaviour
{
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("TEST");
        Debug.Log("HIGHEST FLOOR: " + SaveLoadManager.getHighestFloor());
        text.SetText("" + SaveLoadManager.getHighestFloor());
    }
}
