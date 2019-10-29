using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class RobotBomb : MonoBehaviour
{
    public Light2D light;
    public float timeBeforeExplode;
    float timer;
    bool exploded = false;

    public int damageLowerBound;
    public int damageUpperBound;
    public GameObject explosionPrefab;
    public PlayerAudioManager audioManager;
    public float explosionRange;
    GameObject player;
    public float explosionOffset;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioManager = player.GetComponent<PlayerAudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!exploded)
        {
            timer += Time.deltaTime;
            light.intensity = timer;
        }
        
        if (timer >= timeBeforeExplode)
            explode();
    }

    void explode() 
    {
        exploded = true;
        GameObject explosion = Instantiate(explosionPrefab, gameObject.transform.position,  Quaternion.identity);
        Vector3 pos = explosion.transform.position;
		pos.y += explosionOffset;
		explosion.transform.position = pos;

		audioManager.explosionAudio();

		if (Vector2.Distance(gameObject.transform.position, player.transform.position) <= explosionRange)
        {
            player.GetComponent<PlayerHealth>().takeDamage(Random.Range(damageLowerBound, damageUpperBound), false);
        }
		Destroy(gameObject);
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
