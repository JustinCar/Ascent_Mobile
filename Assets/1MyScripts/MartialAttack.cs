using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MartialAttack : MonoBehaviour
{
    public Transform atkPos;
    public LayerMask enemyLayer;
    public float atkRange;
    public int damageLowerBound;
    public int damageUpperBound;

    public PlayerController plyerCtrl;

    public PlayerAudioManager audioManager;

    public void attack (int multiplier) 
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(atkPos.position, atkRange, enemyLayer);
        if (enemies.Length > 0) 
        {   
            audioManager.martialAttackAudio();
            foreach (Collider2D c in enemies) 
            {

                c.gameObject.GetComponent<EnemyHealth>().TakeDamage(Random.Range(damageLowerBound * multiplier, damageUpperBound * multiplier), plyerCtrl.facingLeft, true, 0);
            }
        } else 
        {
            audioManager.missedMartialAttackAudio();
        }
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(atkPos.position, atkRange);
    }

}
