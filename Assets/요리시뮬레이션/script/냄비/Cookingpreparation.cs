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
    private bool ingredients = false;  //식재료

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
                    //다시
                }
            }
            else if (water == true) 
            {
                if (currentTime >= 4 && currentTime <= 6) //제 시간 안에 클릭을 했는가?
                {
                    if (ingredients == true) //yes 재료를 넣는다
                    {
                        //완성 알아서 보글보글 한 후 완성
                        //5초 경과(ui로 인덕션 끄라는 메시지 띄우기)
                        if(inductionButton.isButtonPressed == false)
                        {
                            //라면 옮기기로 이동
                        }
                        else if(inductionButton.isButtonPressed == true)
                        {
                            //불남
                        }
                    }
                }
                else
                {
                    //no 탔는지 덜익었는지 ui로 띄운 후 재시작
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
