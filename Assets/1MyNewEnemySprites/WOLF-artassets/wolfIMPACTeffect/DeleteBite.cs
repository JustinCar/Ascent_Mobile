using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBite : MonoBehaviour
{
    public void destroySelf() 
    {
        Destroy(gameObject);
    }
}
