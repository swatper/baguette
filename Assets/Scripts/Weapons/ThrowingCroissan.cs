using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowingCroissan : MonoBehaviour, IBaseWeapon
{
    [SerializeField] Rigidbody rigid;
    [SerializeField] Animator wAni;
    [Header("무기 정보")]
    [Tooltip("시작/도착 지점")]
    [SerializeField] Vector3 startPos;
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
        transform.position += new Vector3(0.0004f, 0.0f, 0.00f);
        //회전 운동(?)할 중심 좌표 계산
        center = startPos + transform.right * radius;
        //회전 시작
        StartCoroutine(ThrowCroissant());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && isAttack)
            other.gameObject.GetComponent<EnemyController>().EnemyHit(3);
    }

    IEnumerator ThrowCroissant()
    {
        while (curAngle < 360f)
        {
            startPos = transform.position;
            curAngle += speed * Time.deltaTime;
            //도 -> 라디안 변환
            float rad = curAngle * Mathf.Deg2Rad;

            //회전 궤도 계산
            Vector3 orbit = new Vector3(Mathf.Cos(rad) * radius, 0f, Mathf.Sin(rad) * radius);

            //플레이어 위치 보정
            Vector3 newPos = center + orbit;

            //이동
            transform.position = newPos;

            //방향 설정
            Vector3 tangent = new Vector3(-Mathf.Sin(rad), 0f, Mathf.Cos(rad));
            if (tangent != Vector3.zero)
            {
                Quaternion lookRot = Quaternion.LookRotation(tangent);
                //날아가는 축을 중심으로 회전
                transform.rotation = lookRot * Quaternion.Euler(0f, 0f, 90f);
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        rigid.isKinematic = true;
        isAttack = false;
        wAni.Play("Idle");
        curAngle = 360f;
    }
}
