using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.UI;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] GameObject scope;
    [SerializeField] private CamController camController;
    [SerializeField] private Animator weaponHandlerAni;
    [Header("에이밍 UI")]
    [SerializeField] private Slider aimingProgressbar;
    [SerializeField] private Image fillColor;

    [Header("무기 설정")]
    [Tooltip("무기 프리팹(바게트 빵)")]
    public Baguette BreadPrefs;
    public Baguette onHandBread;
    [Tooltip("최대 빵 보유 갯수")]
    [SerializeField] private int MaxBread = 5;
    [Tooltip("현재 빵 보유 횟수, 자동으로 초기화")]
    [SerializeField] public int curBread;
    [Header("원거리 공격 정보")]
    [Tooltip("발사 각도 측정용")]
    [SerializeField] private Transform fireAngleTransform;
    [Tooltip("원거리 공격 경로(Raycast)")]
    [SerializeField] private BreadThrowPathRaycast throwPathRaycast;
    [Tooltip("줌 유지 시간")]
    Coroutine aimingKeepTimeCoroutine;
    [SerializeField] private float aimingKeepTime;
    [SerializeField] private float curKeepTime;
    [Tooltip("쿨타임")]
    [SerializeField] private float throwCooldownTime;
    [Tooltip("쿨타임 여부")]
    [SerializeField] private bool isCooldown = false;
    [Tooltip("빵 재장전 시간")]
    [SerializeField] private float reloadTime;
    // UI에 전달할 이벤트
    [Tooltip("빵 개수 변경 이벤트")]
    public UnityEvent<int> OnBreadCountChanged = new UnityEvent<int>();

    void Awake()
    {
        ResetSetBread(MaxBread);
    }

    //시작 시점에 빵 갯수 초기화  
    void Start()
    {
        CreateBread(1);
        //UI 표시 빵 갯수 초기화
        CountEventInvoke();
    }

    #region 빵 사용 관련

    /// <summary>
    /// 빵 충전/보급
    /// </summary>
    public void SupplyBread()
    {
        curBread = MaxBread;
        CountEventInvoke();
    }

    /// <summary>
    /// 빵 최대 보유 갯수 증가
    /// </summary>
    /// <param name="amount">증가량</param>
    public void UpgradeMaxBread(int amount)
    {
        MaxBread += amount;
        CountEventInvoke();
    }

    public void ResetSetBread(int count)
    {
        curBread = count;
        if (curBread == 0)
            camController.CameraAim(false);
        CountEventInvoke();
    }

    public void AddCurBread(int amount)
    {
        curBread += amount;
        CountEventInvoke();
    }

    /// <summary>
    /// 빵 생성 (초기화 및 애니메이션에서 호출)
    /// </summary>
    /// <param name="type"></param>
    public void CreateBread(int type)
    {
        if (curBread <= 0)
        {
            return;
        }

        if (type == 0)
            curBread--;

        CountEventInvoke();

        //빵 프리팹 생성
        onHandBread = Instantiate(BreadPrefs, transform);
        //빵 위치 조정
        onHandBread.transform.localPosition = new Vector3(0.62f, 0.2f, 0.7f);
        onHandBread.transform.localRotation = Quaternion.identity;
        onHandBread.transform.localScale = Vector3.one;
    }

    #endregion

    #region 빵 공격 관련
    /// <summary>
    /// 애니메이션에서 호출할 근접 시작 알림
    /// </summary>
    public void StartMeleeAttack() => onHandBread.StartSwingBaguette();

    /// <summary>
    /// 애니메이션에서 호출할 근접 공격 종료 알림
    /// </summary>
    public void EndMeleeAttack() => onHandBread.EndSwingBaguette();

    /// <summary>
    /// 빵 던지기
    /// </summary>
    public void ThrowBread()
    {
        if (aimingKeepTimeCoroutine != null)
        {
            StopCoroutine(aimingKeepTimeCoroutine);
            aimingKeepTimeCoroutine = null;
        }

        if (isCooldown || (curKeepTime < aimingKeepTime))
        {
            camController.CameraAim(false);
            EndThrowReady();
            return;
        }
        else if (curBread < 1)
        {
            //던지기 직전에 빵이 없으면 줌 해제
            camController.CameraAim(false);
            EndThrowReady();
            return;
        }
        else
        {
            isCooldown = true;
            CountEventInvoke();
            //발사 각도 전달하기
            onHandBread.SetFireAngle(fireAngleTransform.forward);
            //빵 던지기
            onHandBread.ThrowBaguette();
            onHandBread = null;
            //빵 재장전
            weaponHandlerAni.Play("ReloadBaguette");
            //시간 측정
            StartCoroutine(ThrowCooldown());
            StartCoroutine(ThrowBreadCoroutine());
        }
    }

    /// <summary>
    /// 던지기 쿨타임 여부 반환
    /// </summary>
    /// <returns></returns>
    public bool IsCooldown()
    {
        return isCooldown;
    }

    public void StartThrowReady()
    {
        aimingProgressbar.gameObject.SetActive(true);
        //경로 표시
        throwPathRaycast.DrowThrowPath();
    }

    /// <summary>
    /// 내부에서 사용할 경로 제거
    /// </summary>
    void EndThrowReady()
    {
        curKeepTime = 0.0f;
        fillColor.color = Color.white;
        aimingProgressbar.gameObject.SetActive(false);
        throwPathRaycast.HideThrowPath();
    }


    #endregion

    #region 빵 개수 관련 by.Jaehoon
    /// <summary>
    /// 현재 빵 개수를 반환합니다.
    /// </summary>
    /// <returns>현재 빵 개수</returns>
    public int GetCurrentBread()
    {
        return curBread;
    }
    /// <summary>
    /// 최대 빵 개수를 반환합니다.
    /// </summary>
    /// <returns>최대 빵 개수</returns>
    public int GetMaxBread()
    {
        return MaxBread;
    }
    /// <summary>
    /// 현재 빵 개수가 변경되었음을 알리는 이벤트를 Invoke합니다.
    /// </summary>
    private void CountEventInvoke()
    {
        OnBreadCountChanged.Invoke(curBread);
    }
    #endregion

    public void StartAimingTime()
    {
        if (aimingKeepTimeCoroutine != null)
            return;

        aimingKeepTimeCoroutine = StartCoroutine(CheckAimingTime());
    }

    #region 코루틴 (시간 측정)
    /// <summary>
    /// 던지기 쿨타임 코루틴
    /// </summary>
    IEnumerator ThrowCooldown()
    {
        float curCoolTime = 0;
        while (curCoolTime < throwCooldownTime)
        {
            curCoolTime++;
            yield return new WaitForSeconds(1.2f);
        }
        isCooldown = false;
    }

    /// <summary>
    /// 조준 상태 유지 코루틴(재장전 시간 연동)
    /// </summary>
    IEnumerator ThrowBreadCoroutine()
    {
        //재장전 시간 동안 조준 상태 유지
        yield return new WaitForSeconds(reloadTime);

        //카메라 줌 아웃(3인칭으로 변경)
        camController.CameraAim(false);
        yield return null;
        EndThrowReady();
    }

    IEnumerator CheckAimingTime()
    {
        //재장전 시간 동안 조준 상태 유지
        while (curKeepTime < aimingKeepTime)
        {
            curKeepTime += Time.deltaTime;
            aimingProgressbar.value = Mathf.Clamp01(curKeepTime / aimingKeepTime);
            //카메라 위치 보정
            if (curKeepTime > 0.15)
                camController.TranslateCametaFoce(scope.transform.position);
            yield return null;
        }
        curKeepTime = aimingKeepTime;
        aimingProgressbar.value = 1.0f;
        fillColor.color = Color.green;
    }
    #endregion
}
