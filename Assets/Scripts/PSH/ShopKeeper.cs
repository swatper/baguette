using System;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    [Tooltip("씬에서 직접 할당 필요")]
    public OnOffManager shopOnOFF;
    [Header("상점 관련 UI")]
    [Tooltip("상점 UI")]
    public GameObject storeUI;
    [Tooltip("상호작용 키 UI (키 힌트 UI)")]
    public GameObject keyhintUI;

    [Tooltip("키 힌트가 바라볼 대상(카메라)")]
    [SerializeField] Transform target;
    [SerializeField] PlayerController player;

    void Awake()
    {
        keyhintUI.SetActive(false);
    }

    /// <summary>
    /// 키 힌트 표시는 "카메라"를 바라봄 
    /// </summary>
    void LateUpdate()
    {
        if (target == null)
            return;

        /*
        //플레이어 방향에 따라 키 힌트 회전
        Vector3 direction = target.position - keyhintUI.transform.position;

        if (direction.sqrMagnitude > 0.001f)
        {
            //텍스트 앞면이 플레이어를 향하도록 회전값 계산 및 회전
            keyhintUI.transform.rotation = Quaternion.LookRotation(-direction);
        }
        */

        keyhintUI.transform.rotation = target.rotation;
    }

    //키 힌트 보여주기
    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        target = Camera.main.transform;
        keyhintUI.SetActive(true);
        player = other.GetComponent<PlayerController>();
        player.SetShopInteration(this);
    }

    //키 힌트 숨기기
    void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;
        target = null;
        keyhintUI.SetActive(false);
        player.RemoveShopInteration();
        player = null;
    }

    public void ShowStore()
    {
        shopOnOFF.StateChange();
        storeUI.SetActive(true);
        player.SetPlayerReadMode(true);
    }
}
