using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCameraShake : MonoBehaviour
{
    public CameraShake cameraShake;
    public float duration;
    public float amplitude;
    // Start is called before the first frame update
    void Start()
    {
        cameraShake = GameObject.Find("Manager").GetComponent<CameraShake>();
        StartCoroutine(cameraShake.Shake(duration, amplitude));
    }
}
