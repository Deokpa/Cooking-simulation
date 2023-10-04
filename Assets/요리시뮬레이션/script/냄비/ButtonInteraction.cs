using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class ButtonInteraction : MonoBehaviour
{
    public GameObject targetObject;
    public Transform buttonPressedPosition;
    public bool isButtonPressed = false;

    private Vector3 originalPosition;
    private Color originalColor;
    private Renderer targetRenderer;

    private void Start()
    {
        targetRenderer = targetObject.GetComponent<Renderer>();
        originalColor = targetRenderer.material.color;
        originalPosition = transform.position;
    }

    private void PressButton()
    {
        isButtonPressed = !isButtonPressed;

        if (isButtonPressed)
        {
            targetRenderer.material.color = Color.red;
            transform.position = buttonPressedPosition.position;
        }
        else if (!isButtonPressed)
        {
            targetRenderer.material.color = originalColor;
            transform.position = originalPosition;
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 7) // 손 레이어
        {
            Debug.Log(OVRInput.GetDown(OVRInput.Button.One));
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                
                PressButton();
            }
        }
    }
}