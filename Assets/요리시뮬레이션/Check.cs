using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotInteraction : MonoBehaviour
{
    private bool isPotOnInduction = false; // 냄비가 인덕션 위에 올려져 있는지 여부를 나타내는 플래그

    private void OnTriggerEnter(Collider other)
    {
        // 냄비가 인덕션 위에 올라갔을 때 호출되는 메서드
        if (other.CompareTag("Pot"))
        {
            isPotOnInduction = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 냄비가 인덕션 위에서 내려왔을 때 호출되는 메서드
        if (other.CompareTag("Pot"))
        {
            isPotOnInduction = false;
        }
    }

    // 냄비가 인덕션 위에 올려져 있는지 여부를 반환하는 메서드
    public bool IsPotOnInduction()
    {
        return isPotOnInduction;
    }
}
