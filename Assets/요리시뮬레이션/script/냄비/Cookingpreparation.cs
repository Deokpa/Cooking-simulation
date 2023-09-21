using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cookingpreparation : MonoBehaviour
{
    private ButtonInteraction inductionButton;
    private float maxTime = 10f;
    private float currentTime = 0f;

    private bool pot = false;
    private bool water = false;
    private bool ingredients = false;  //�����

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
            if (water != true)
            {
                if (inductionButton.isButtonPressed == true)
                {
                    //�ٽ�
                }
            }
            else if (water == true) 
            {
                if (currentTime >= 4 && currentTime <= 6) //�� �ð� �ȿ� Ŭ���� �ߴ°�?
                {
                    if (ingredients == true) //yes ��Ḧ �ִ´�
                    {
                        //�ϼ� �˾Ƽ� ���ۺ��� �� �� �ϼ�
                        //5�� ���(ui�� �δ��� ����� �޽��� ����)
                        if(inductionButton.isButtonPressed == false)
                        {
                            //��� �ű��� �̵�
                        }
                        else if(inductionButton.isButtonPressed == true)
                        {
                            //�ҳ�
                        }
                    }
                }
                else
                {
                    //no ������ ���;����� ui�� ��� �� �����
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
