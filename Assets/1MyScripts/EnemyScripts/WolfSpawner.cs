using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSpawner : MonoBehaviour
{
    private EnemyHealth HealthScript;
    public GameObject WolfToSpawn;
    public int NumberToSpawn;
    public float MinDistanceToSpawn;
    public float MaxDistanceToSpawn;
    public int RaycastLayer;

    public float SpawnOffset;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<EnemyHealth>().OnDeath = OnWolfDeath;
    }

    void OnWolfDeath()
    {
        for(int i = 0; i < NumberToSpawn; i++)
        {
            float DistanceToSpawn = Random.Range(MinDistanceToSpawn, MaxDistanceToSpawn); 
            DistanceToSpawn *= GetDirectionToSpawn();
            Vector3 SpawnLocation = gameObject.transform.position;
            SpawnLocation.x += DistanceToSpawn;
            SpawnLocation.y += SpawnOffset;
            GameObject Wolf = Instantiate(WolfToSpawn, SpawnLocation, WolfToSpawn.transform.rotation);

            Animator Anim = Wolf.GetComponentInChildren<Animator>();
            if (Anim)  
            {
                Anim.SetBool("isChasing", true);
            }
        }
    }

    bool LeftSideClear()
    {
        RaycastHit2D 
        hit = Physics2D.Raycast(gameObject.transform.position, -Vector2.right, MaxDistanceToSpawn, RaycastLayer);
		if (hit) 
		{
			return false;
		}

        return true;
    }
    bool RightSideClear()
    {
        RaycastHit2D 
        hit = Physics2D.Raycast(gameObject.transform.position, Vector2.right, MaxDistanceToSpawn, RaycastLayer);
		if (hit) 
		{
			return false;
		}

        return true;
    }

    int GetDirectionToSpawn()
    {
            if (Random.Range(1, 3) == 1)
            {
                if(RightSideClear())
                {
                    return 1; 
                }
                else
                {
                    return -1; 
                }
            }
            else
            {
                if(LeftSideClear())
                {
                    return -1; 
                }
                else
                {
                    return 1; 
                }
            }
    }
}
