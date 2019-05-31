using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    public LevelManager manager;
    public float doorOpenDistance;
    public Animator animator;
    GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<LevelManager>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, gameObject.transform.position) <= doorOpenDistance) 
        {
            animator.SetBool("playerClose", true);
        } else 
        {
            animator.SetBool("playerClose", false);
        }
    }

    void OnTriggerEnter2D (Collider2D collider) 
	{
		if (collider.tag == "Player") 
		{
			manager.levelCompleted();
		}
	}
}
