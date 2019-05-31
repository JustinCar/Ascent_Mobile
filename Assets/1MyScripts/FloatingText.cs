using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour {

    public float destroyTime = 0.3f;
    public float offset;
    PlayerController plyerCtrl;
    public Vector3 randomizeIntensity = new Vector3(0.1f, 0.1f, 0.1f);
    bool facingLeft = false;

    public bool damageOverTime;
	// Use this for initialization
	void Start () {
        plyerCtrl = GameObject.Find("Player").GetComponent<PlayerController>();
        Destroy(gameObject, destroyTime);

        if (damageOverTime) 
        {
            transform.localPosition += new Vector3(0, offset / 2, 0);
        }
        else if (plyerCtrl.facingLeft) 
        {
            facingLeft = true;
            transform.localPosition += new Vector3(-offset, 0.1f, 0);
        }
        else if (!plyerCtrl.facingLeft) 
        {
            facingLeft = false;
            transform.localPosition += new Vector3(offset, 0.1f, 0);
        }
        
        transform.localPosition += new Vector3(Random.Range (-randomizeIntensity.x, randomizeIntensity.x),
            Random.Range(-randomizeIntensity.y, randomizeIntensity.y),
            Random.Range(-randomizeIntensity.z, randomizeIntensity.z));

    }
	
	// Update is called once per frame
	void Update () {
        if (damageOverTime) 
        {
            transform.localPosition += new Vector3(0, offset * Time.deltaTime, 0);
        }
		else if (facingLeft) 
        {
            transform.localPosition += new Vector3(-offset * Time.deltaTime, 0, 0);
        }
        else if (!facingLeft) 
        {
            transform.localPosition += new Vector3(offset * Time.deltaTime, 0, 0);
        }
	}
}
