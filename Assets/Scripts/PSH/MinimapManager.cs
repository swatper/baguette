using UnityEngine;

public class MinimapManager : MonoBehaviour
{
    // 정적 싱글톤 인스턴스
    public static MinimapManager Instance { get; private set; }
    [SerializeField] private Camera minimapCamera;

    [Header("미니맵 요소")]
    [SerializeField] private GameObject minimapUI;          // 미니맵 전체 루트 오브젝트 (켜고 끄기용)
    [SerializeField] private RectTransform minimapRect;     // 렌더 텍스처를 보여주는 RawImage의 RectTransform
    [SerializeField] private RectTransform playerMark;
    [SerializeField] private RectTransform macaroonMark;

    [Header("추척 대상")]
    [Tooltip("플레이어")]
    [SerializeField] private Transform playerTransform;
    [Tooltip("마카롱")]
    [SerializeField] private Transform macaTransform;

    private void Awake()
    {
        // 싱글톤 중복 방지 및 인스턴스 초기화
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void LateUpdate()
    {
        UpdatePlayerIcon();
    }

    private void UpdatePlayerIcon()
    {
        //마크 배치
        if (macaTransform != null)
            macaroonMark.anchoredPosition = TranslateWorldPosToUI(macaTransform);
        else
            macaroonMark.gameObject.SetActive(false);

        playerMark.anchoredPosition = TranslateWorldPosToUI(playerTransform);
        //마크 회전
        playerMark.localEulerAngles = new Vector3(0f, 0f, -playerTransform.eulerAngles.y);
    }

    Vector2 TranslateWorldPosToUI(Transform target)
    {
        //월드 좌표 -> 뷰포트 좌표 변환
        Vector3 viewPos = minimapCamera.WorldToViewportPoint(target.position);
        //미니맵 Rect 사이즈 기준으로 위치 계산 (Anchor/Pivot이 0.5, 0.5 기준)
        Vector2 size = minimapRect.rect.size;

        return new Vector2(
            (viewPos.x - 0.5f) * size.x,
            (viewPos.y - 0.5f) * size.y
        );
    }

    /// <summary>
    /// 미니맵 UI 켜기
    /// </summary>
    public void HideMinimap()
    {
        if (minimapUI != null)
            minimapUI.gameObject.SetActive(false);

    }

    /// <summary>
    /// 미니맵 UI 끄기
    /// </summary>
    public void ShowMinimap()
    {
        if (minimapUI != null)
            minimapUI.gameObject.SetActive(true);
    }
}