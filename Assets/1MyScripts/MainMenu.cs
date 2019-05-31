using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject guide;

    DebugInfo debugInfo;   

    void Start () 
    {
        debugInfo = GameObject.Find("DebugInfoToggle").GetComponent<DebugInfo>();
    }
    public void PlayGame()
    {
        debugInfo.activate();
        SceneManager.LoadScene("LevelGenerationTest");
    }

    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }

    public void ActivateGuide() 
    {
        guide.SetActive(true);
        gameObject.SetActive(false);
    }
}
