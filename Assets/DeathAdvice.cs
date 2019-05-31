using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DeathAdvice : MonoBehaviour
{
    public List<string> advice;
    Text text;
    // Start is called before the first frame update
    void Awake()
    {
        text = gameObject.GetComponent<Text>();
        text.text = advice[Random.Range(0, advice.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
