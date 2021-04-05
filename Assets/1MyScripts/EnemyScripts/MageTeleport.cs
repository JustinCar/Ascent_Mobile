using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTeleport : MonoBehaviour
{
    PlayerAudioManager audioManager;
    public Vector3 TeleportLocation;
    Animator Animator;
    Transform ParentTransform;
    EnemyHealth Health;
    void Awake()
    {
        audioManager = GameObject.Find("Player").GetComponent<PlayerAudioManager>();
        TeleportLocation = Vector3.zero;
        Animator = gameObject.GetComponent<Animator>();
        ParentTransform = gameObject.transform.parent.transform;
        Health = gameObject.GetComponentInParent<EnemyHealth>();
    }

    void Teleport()
    {
        if (TeleportLocation != Vector3.zero)
        {
            ParentTransform.position = TeleportLocation;
            TeleportLocation = Vector3.zero;
            Health.FlipIfNeeded(); 
            Animator.SetBool("isTeleporting", false); 
        }
    }
}
