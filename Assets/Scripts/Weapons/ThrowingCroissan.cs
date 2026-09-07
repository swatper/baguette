using System.Collections;
using UnityEngine;

public class ThrowingCroissan : MonoBehaviour, IBaseWeapon
{
    [SerializeField] WeaponHandler wHandler;
    [SerializeField] Rigidbody rigid;
    [SerializeField] Animator wAni;
    [Header("무기 정보")]
    [Tooltip("회전 중심 좌표")]
    [SerializeField] Vector3 center;
    [Tooltip("플레이어와의 거리(회전 반경)")]
    [SerializeField] float radius;
    [Tooltip("회전 속도")]
    [SerializeField] float speed;
    [SerializeField] float curAngle;
    bool isAttack = false;

    void OnEnable()
    {
        rigid.isKinematic = true;
    }

    public void ThrowCroassian()
    {
        wAni.Play("Rotate");
        rigid.isKinematic = false;
        isAttack = true;
        curAngle = 0.0f;
        //회전 시작
        StartCoroutine(ThrowCroissant());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && isAttack)
            other.gameObject.GetComponent<EnemyController>().EnemyHit(3, transform.position);
    }

    IEnumerator ThrowCroissant()
    {
        //회전 시작 전 상태 저장(위치, 방향)
        Vector3 startLocalPos = transform.localPosition;
        Quaternion startLocalRot = transform.localRotation;

        Vector3 orbitCenter = startLocalPos + Vector3.forward * radius;
        //회전 시간 계산
        float elapsed = 0f;
        float duration = 360f / speed;

        while (curAngle < 360f)
        {
            elapsed += Time.deltaTime;
            curAngle = Mathf.Clamp(elapsed / duration * 360f, 0f, 360f);
            float rad = curAngle * Mathf.Deg2Rad;
            Vector3 offset = new Vector3(Mathf.Sin(rad) * radius, 0f, -Mathf.Cos(rad) * radius);
            transform.localPosition = orbitCenter + offset;

            Vector3 tangent = new Vector3(Mathf.Cos(rad), 0f, Mathf.Sin(rad));
            if (tangent != Vector3.zero)
            {
                Quaternion lookRot = Quaternion.LookRotation(tangent, Vector3.up);
                transform.localRotation = lookRot * Quaternion.Euler(0f, 0f, 90f);
            }
            yield return null;
        }
        //보정
        transform.localPosition = startLocalPos;
        transform.localRotation = startLocalRot;

        rigid.isKinematic = true;
        isAttack = false;
        wAni.Play("Idle");
        curAngle = 360f;
        wHandler.ResetState();
    }
}
