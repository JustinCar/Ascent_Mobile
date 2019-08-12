using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowAttack : MonoBehaviour
{
    public Transform atkPos;
    public int damageLowerBound;
    public int damageUpperBound;

    public PlayerController plyerCtrl;

    public GameObject arrow;

    public void attack (int multiplier) 
    {

        Vector3 pos = transform.position;
		//pos.y -= 0.11f;

		GameObject abilityInstance = Instantiate(arrow, atkPos.transform.position, arrow.transform.rotation) as GameObject;
		PlayerArrow arrowScript = abilityInstance.GetComponent<PlayerArrow>();
        arrowScript.damageLowerBound = damageLowerBound * multiplier;
        arrowScript.damageUpperBound = damageUpperBound * multiplier;

		if (plyerCtrl.facingLeft) 
        {
			arrowScript.travelingLeft = true;
            Quaternion target = Quaternion.Euler(0, 0, 45);
			abilityInstance.transform.rotation = Quaternion.Slerp(target, target, Time.deltaTime);
        }
    }
}
