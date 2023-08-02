using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cookingpreparation : MonoBehaviour
{
    public ButtonInteraction inductionButton;
    private float maxTime = 10f;
    private float currentTime = 0f;

    private bool pot = false;
    private bool water = false;

    void Start()
    {
        inductionButton = GameObject.Find("inductionButton").GetComponent<ButtonInteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inductionButton.isButtonPressed && pot) 
        {
            currentTime += Time.deltaTime;
            if (water)
            {
                //�� ���� �ִϸ��̼� �߰�
            }
            else if (!water) 
            {
                if (currentTime > maxTime)
                {
                    //���� Ÿ�� ��ƼŬ �߰�
                    currentTime = 0;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pot")) 
        {
            pot = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pot"))
        {
            pot = false;
        }
    }
}
