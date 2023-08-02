using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class ButtonInteraction : MonoBehaviour
{
    public GameObject targetObject;

    public bool isButtonPressed = false;
    private Color originalColor;
    private Renderer targetRenderer;

    private void Start()
    {
        targetRenderer = targetObject.GetComponent<Renderer>();
        originalColor = targetRenderer.material.color;
    }

    private void PressButton()
    {
        isButtonPressed = !isButtonPressed;

        if (isButtonPressed)
        {
            targetRenderer.material.color = Color.red;     
        }
        else if (!isButtonPressed)
        {
            targetRenderer.material.color = originalColor;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                PressButton();
            }
        }
    }
}