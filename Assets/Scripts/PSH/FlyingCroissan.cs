using Unity.VisualScripting;
using UnityEngine;

public class FlyingCroissan : MonoBehaviour
{

    [SerializeField] Rigidbody rigid;
    [Header("플레이어")]
    [SerializeField] Transform playerTrans;
    [Header("비행 정보")]
    [Tooltip("플레이어와의 거리(회전 반경)")]
    [SerializeField] float radius;
    [Tooltip("회전 속도")]
    [SerializeField] float speed;
    [Tooltip("높이")]
    [SerializeField] float height;
    private float currentAngle = 0f;
    private bool isShootDown = false;
    void Start()
    {
        playerTrans = Managers.Player.transform;
    }

    void Update()
    {
        if (isShootDown)
            return;
        currentAngle += speed * Time.deltaTime;
        //도 -> 라디안 변환
        float rad = currentAngle * Mathf.Deg2Rad;

        //회전 궤도 계산
        Vector3 orbit = new Vector3(Mathf.Cos(rad) * radius, height, Mathf.Sin(rad) * radius);

        //플레이어 위치 보정
        Vector3 newPos = playerTrans.position + orbit;

        //이동
        transform.position = newPos;

        //방향 설정
        Vector3 tangent = new Vector3(-Mathf.Sin(rad), 0f, Mathf.Cos(rad));
        if (tangent != Vector3.zero)
        {
            Quaternion lookRot = Quaternion.LookRotation(tangent);
            //날아가는 축을 꼬챙이 삼아 회전
            transform.rotation = lookRot * Quaternion.Euler(0f, 0f, 90f);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Baguette")) // 바게트 태그 체크
        {
            isShootDown = true;
            rigid.useGravity = true;
        }
    }
}
