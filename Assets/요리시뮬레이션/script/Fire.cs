using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public ButtonInteraction buttoninteract;

    float maxTime;
    float currentTime;

    void Start()
    {
        buttoninteract = GameObject.Find("set").GetComponent<ButtonInteraction>();
        maxTime = 5;
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > maxTime)
        {
            print(currentTime);
            print(buttoninteract.pot);
            currentTime = 0;
        }
    }
}
