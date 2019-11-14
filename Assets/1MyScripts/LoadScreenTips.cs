using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadScreenTips : MonoBehaviour
{
    public List<string> advice;
    public TextMeshProUGUI text;

    public float coolDown;
    float timer;

    int previousIndex;
    // Start is called before the first frame update
    void Awake()
    {
        previousIndex = Random.Range(0, advice.Count);
        text.text = advice[previousIndex];
        timer = coolDown;
    }

    void Update() 
    {
        timer -= Time.deltaTime;
        if (timer <= 0) 
        {
            int newIndex = Random.Range(0, advice.Count);
            while (newIndex == previousIndex) 
            {
                newIndex = Random.Range(0, advice.Count);
            }
            previousIndex = newIndex;
            text.text = advice[newIndex];
        }
    }
}
