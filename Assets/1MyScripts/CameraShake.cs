using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class CameraShake : MonoBehaviour
{
    public float frequency = 2.0f;

    public CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    // Start is called before the first frame update
    void Start()
    {
        if (virtualCamera != null) 
        {
            virtualCameraNoise = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        }
    }

    public IEnumerator Shake (float duration, float amplitude) 
    {
        float elapsedTime = 0.0f;
        Debug.Log("SHAKE");

        while (elapsedTime < duration) 
        {
            if (virtualCamera != null || virtualCameraNoise != null) 
            {
                virtualCameraNoise.m_AmplitudeGain = amplitude;
                virtualCameraNoise.m_FrequencyGain = frequency;

                elapsedTime += Time.deltaTime;

                yield return null;
            }
        }

        virtualCameraNoise.m_AmplitudeGain = 0f;
    }
}
