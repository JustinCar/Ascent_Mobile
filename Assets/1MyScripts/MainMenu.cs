using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    void Start () 
    {
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("LevelGenerationTest");
    }

    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }

}
