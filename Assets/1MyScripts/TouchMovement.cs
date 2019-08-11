using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMovement : MonoBehaviour
{
    public PlayerController playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void leftPressed() 
    {
        playerController.move = -1;
    }

    public void rightPressed() 
    {
        playerController.move = 1;
    }

    public void upPressed() 
    {
        playerController.upButtonPressed = true;
    }

    public void downPressed() 
    {
        playerController.downButtonPressed = true;
    }

    public void leftReleased() 
    {
        playerController.move = 0;
    }

    public void rightReleased() 
    {
        playerController.move = 0;
    }

    public void upReleased() 
    {
        playerController.upButtonPressed = false;
    }

    public void downReleased() 
    {
        playerController.downButtonPressed = false;
    }
}
