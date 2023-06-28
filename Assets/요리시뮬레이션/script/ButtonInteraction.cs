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
                // 버튼을 눌렀을 때 실행할 동작
                PressButton();
            }
        }
    }

    private void PressButton()
    {
        isButtonPressed = !isButtonPressed;
        // 버튼이 눌렸을 때의 동작을 구현

        if (isButtonPressed && pot) // 냄비가 위에 있다
        {
            // 오브젝트 위에 오브젝트가 존재할 때 실행할 동작을 구현
            targetRenderer.material.color = Color.red;
            // 온도가 올라가는 코드 만들어야 함

            // 추가적인 코드 작성
            if (objectOnTop2) // 물이 존재한다
            {
                // 오브젝트 위에 추가 오브젝트가 존재할 때 실행할 동작을 구현
                // 물 끓는 애니메이션 추가
                // 추가적인 코드 작성
            }
            else if (!objectOnTop2) // 물이 존재하지 않는다
            {
                // 오브젝트 위에 추가 오브젝트가 존재하지 않을 때 실행할 동작을 구현
                // 일정 시간 지나면 냄비가 탐
                // 추가적인 코드 작성
            }
        }
        else if (isButtonPressed && !pot) // 냄비가 위에 없다
        {
            // 오브젝트 위에 오브젝트가 존재하지 않을 때 실행할 동작을 구현
            targetRenderer.material.color = Color.red;
            // 추가적인 코드 작성
        }
        else if (!isButtonPressed)
        {
            ReleaseButton();
        }

    }

    private void ReleaseButton()
    {
        isButtonPressed = false;
        // 버튼이 떼어졌을 때의 동작을 구현
        targetRenderer.material.color = originalColor;
        // 온도가 0으로 바뀐다
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 오브젝트와 충돌한 다른 오브젝트의 개수가 1개 이상인 경우,
        // 즉, 오브젝트 위에 다른 오브젝트가 존재하는 경우로 판단합니다.
        if (collision.contacts.Length > 0)
        {
            pot = true;
            if (collision.gameObject.CompareTag("Water"))
            {
                // 오브젝트 위에 추가 오브젝트가 존재할 때 실행할 동작을 구현
                objectOnTop2 = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // 충돌한 다른 오브젝트가 없으면 오브젝트 위에 오브젝트가 존재하지 않는 것으로 판단합니다.
        pot = false;
    }
}