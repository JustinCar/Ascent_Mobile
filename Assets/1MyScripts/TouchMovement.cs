using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMovement : MonoBehaviour
{
    public PlayerController playerController;

    public void leftPressed() 
    {
        if (playerController.isActiveAndEnabled)
            playerController.move = -1;
    }

    public void rightPressed() 
    {
        if (playerController.isActiveAndEnabled)
            playerController.move = 1;
    }

    public void upPressed() 
    {
        if (playerController.isActiveAndEnabled)
            playerController.upButtonPressed = true;
    }

    public void downPressed() 
    {
        if (playerController.isActiveAndEnabled)
            playerController.downButtonPressed = true;
    }

    public void leftReleased() 
    {
        if (playerController.isActiveAndEnabled)
            playerController.move = 0;
    }

    public void rightReleased() 
    {
        if (playerController.isActiveAndEnabled)
            playerController.move = 0;
    }

    public void upReleased() 
    {
        if (playerController.isActiveAndEnabled)
            playerController.upButtonPressed = false;
    }

    public void downReleased() 
    {
        if (playerController.isActiveAndEnabled)
            playerController.downButtonPressed = false;
    }
}
