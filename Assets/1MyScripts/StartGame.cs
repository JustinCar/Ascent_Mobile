using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

	DebugInfo debugInfo;   

	// Use this for initialization
	void Start () {
		debugInfo = GameObject.Find("DebugInfoToggle").GetComponent<DebugInfo>();
	}


	void OnTriggerEnter2D (Collider2D collider) 
	{
		if (collider.tag == "Player") 
		{
			debugInfo.activate();
			SceneManager.LoadScene("LevelGenerationTest");
		}
	}
}
