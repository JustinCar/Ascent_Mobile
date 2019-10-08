using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject loadScreen;
    public void PlayGame()
    {
        //SceneManager.LoadScene("LevelGenerationTest");

        StartCoroutine(LoadAsynchronously());
    }

    IEnumerator LoadAsynchronously() 
    {
        loadScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync("LevelGenerationTest");
        

        yield return null;
    }

    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }

}
