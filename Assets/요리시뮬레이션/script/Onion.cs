using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onion : MonoBehaviour
{
    public GameObject ramenPrefab; // 라면 재료 3개로 나눠진 프리팹
    public Transform hand; // 손 위치 (Oculus Hand Tracking에서 자동으로 갱신)
    public Transform pot; // Pot(냄비) 위치


    private bool isHolding = false; // 파를 잡고 있는지 여부를 나타내는 플래그

    // 버튼을 누르면 파가 나눠지도록 하는 메소드
    private void Start()
    {
        //potInteraction = FindObjectOfType<PotInteraction>();
    }
    
    public void OnButtonPress()
    {
        if (isHolding)
        {
            // 파를 나누는 로직
            Vector3 spawnPosition = transform.position + new Vector3(0.1f, 0.0f, 0.0f); // 파를 잘라낸 후 나오는 위치 조정
            Instantiate(ramenPrefab, spawnPosition, Quaternion.identity);
            Destroy(gameObject); // 원래 파 오브젝트 삭제

             // 파 조각을 Pot(냄비) 근처로 이동시킴
            //ramenPiece.GetComponent<Rigidbody>().useGravity = false;
            //ramenPiece.GetComponent<Rigidbody>().isKinematic = true;
            //ramenPiece.transform.position = hand.position; // 손 위치로 이동

            //if (potInteraction.IsPotOnInduction())
            {
                // 파 조각을 인덕션 위에서 냄비로 들어가도록 처리
             //   StartCoroutine(MoveToPot(ramenPiece.transform));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 손이 파에 닿으면 잡기 시작
        if (other.CompareTag("Hand"))
        {
            isHolding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 손이 파에서 떨어지면 잡기 종료
        if (other.CompareTag("Hand"))
        {
            isHolding = false;
        }
    }
     // 냄비 안으로 파 조각을 이동시키는 Coroutine
    private IEnumerator MoveToPot(Transform ramenPiece)
    {
        float duration = 2.0f; // 이동에 걸리는 시간
        float elapsedTime = 0.0f;
        Vector3 startPosition = ramenPiece.position;
        Vector3 targetPosition = pot.position;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            ramenPiece.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        // 파 조각을 냄비 안으로 들어가게 함
        Destroy(ramenPiece.gameObject);
        // 냄비에 파 조각이 들어간 후의 추가 작업 (예를 들어, 라면이 완성되는 등의 처리)
    }
}
