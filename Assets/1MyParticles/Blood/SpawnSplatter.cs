using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSplatter : MonoBehaviour
{
    ParticleSystem particleSystem;
    public List<GameObject> splatters;
    public List<ParticleCollisionEvent> collisions = new List<ParticleCollisionEvent>();
    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject collision) 
    {
        ParticlePhysicsExtensions.GetCollisionEvents(particleSystem, collision, collisions);

        int count = collisions.Count;

        for (int i = 0; i < count; i++) 
        {
            Instantiate(splatters[Random.Range(0, splatters.Count)], collisions[i].intersection, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        }
    }
}
