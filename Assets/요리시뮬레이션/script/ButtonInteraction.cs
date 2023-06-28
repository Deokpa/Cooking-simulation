using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class ButtonInteraction : MonoBehaviour
{
    public ButtonController buttonController;
    public GameObject targetObject;

    private bool pot = false;
    private bool objectOnTop2 = false;
    private bool isButtonPressed = false;
    private Color originalColor;
    private Renderer targetRenderer;

    private void Start()
    {
        buttonController = GetComponent<ButtonController>();
        targetRenderer = targetObject.GetComponent<Renderer>();
        originalColor = targetRenderer.material.color;

        buttonController.InteractableStateChanged.AddListener(OnButtonStateChanged);
    }

    private void OnButtonStateChanged(InteractableStateArgs state)
    {
        if (state.NewInteractableState == InteractableState.ProximityState)
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                // ��ư�� ������ �� ������ ����
                PressButton();
            }
        }
    }

    private void PressButton()
    {
        isButtonPressed = !isButtonPressed;
        // ��ư�� ������ ���� ������ ����

        if (isButtonPressed && pot) // ���� ���� �ִ�
        {
            // ������Ʈ ���� ������Ʈ�� ������ �� ������ ������ ����
            targetRenderer.material.color = Color.red;
            // �µ��� �ö󰡴� �ڵ� ������ ��

            // �߰����� �ڵ� �ۼ�
            if (objectOnTop2) // ���� �����Ѵ�
            {
                // ������Ʈ ���� �߰� ������Ʈ�� ������ �� ������ ������ ����
                // �� ���� �ִϸ��̼� �߰�
                // �߰����� �ڵ� �ۼ�
            }
            else if (!objectOnTop2) // ���� �������� �ʴ´�
            {
                // ������Ʈ ���� �߰� ������Ʈ�� �������� ���� �� ������ ������ ����
                // ���� �ð� ������ ���� Ž
                // �߰����� �ڵ� �ۼ�
            }
        }
        else if (isButtonPressed && !pot) // ���� ���� ����
        {
            // ������Ʈ ���� ������Ʈ�� �������� ���� �� ������ ������ ����
            targetRenderer.material.color = Color.red;
            // �߰����� �ڵ� �ۼ�
        }
        else if (!isButtonPressed)
        {
            ReleaseButton();
        }

    }

    private void ReleaseButton()
    {
        isButtonPressed = false;
        // ��ư�� �������� ���� ������ ����
        targetRenderer.material.color = originalColor;
        // �µ��� 0���� �ٲ��
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ������Ʈ�� �浹�� �ٸ� ������Ʈ�� ������ 1�� �̻��� ���,
        // ��, ������Ʈ ���� �ٸ� ������Ʈ�� �����ϴ� ���� �Ǵ��մϴ�.
        if (collision.contacts.Length > 0)
        {
            pot = true;
            if (collision.gameObject.CompareTag("Water"))
            {
                // ������Ʈ ���� �߰� ������Ʈ�� ������ �� ������ ������ ����
                objectOnTop2 = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // �浹�� �ٸ� ������Ʈ�� ������ ������Ʈ ���� ������Ʈ�� �������� �ʴ� ������ �Ǵ��մϴ�.
        pot = false;
    }
}