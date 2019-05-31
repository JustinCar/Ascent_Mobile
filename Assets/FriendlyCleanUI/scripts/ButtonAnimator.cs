using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Animator buttonAnimator;
    public Button button;

    // Use this for initialization
    void Start()
    {
        buttonAnimator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonAnimator.SetTrigger("Highlighted");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonAnimator.SetTrigger("Normal");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonAnimator.SetTrigger("Pressed");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Checks if current button is under mouse cursor while you release the pointer
        bool isButtonHovered = eventData.hovered.Contains(this.gameObject);

        if (isButtonHovered)
        {
            buttonAnimator.SetTrigger("Highlighted");
        }
        else {
            buttonAnimator.SetTrigger("Normal");
        }

    }
}