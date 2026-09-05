using UnityEngine;

public class Patissier : MonoBehaviour
{
    [Header("빵집 관련 UI")]
    [Tooltip("상호작용 키 UI (키 힌트 UI)")]
    public GameObject keyhintUI;
    [Header("카메라 추적 스크립트")]
    [SerializeField] Billboard board;

    void Awake()
    {
        keyhintUI.SetActive(false);
    }

    //키 힌트 보여주기
    void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();
        if (player == null || !other.transform.root.CompareTag("Player"))
            return;
        keyhintUI.SetActive(true);
        board.SetTarget(Camera.main.transform);
        player.SetBreadShopInteration(this);
    }

    //키 힌트 숨기기
    void OnTriggerExit(Collider other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();
        if (player == null || !other.transform.root.CompareTag("Player"))
            return;

        board.ResetTarget();
        keyhintUI.SetActive(false);
        player.RemoveBreadShopInteration();
    }
}
