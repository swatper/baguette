using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowingCroissan : MonoBehaviour, IBaseWeapon
{
    [SerializeField] Rigidbody rigid;
    [Header("무기 정보")]
    [Tooltip("플레이어와의 거리(회전 반경)")]
    [SerializeField] float radius;
    [Tooltip("회전 속도")]
    [SerializeField] float speed;
    [Tooltip("회전 중심 좌표")]
    [SerializeField] Vector3 center;
    [SerializeField] float curAngle;
    [Tooltip("시작/도착 지점")]
    [SerializeField] Vector3 startPos;

    void OnEnable()
    {
        rigid.isKinematic = true;
    }

    public void ThrowCroassian()
    {
        //투척 위치 저장
        startPos = transform.position;
        transform.position += new Vector3(0.0004f, 0.0f, 0.00f);
        //회전 운동(?)할 중심 좌표 계산
        center = startPos + transform.right * radius;
        //회전 운동 시작
        //StartCoroutine(FlyCroassian());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyController>().EnemyHit(3);
        }
    }
    IEnumerator FlyCroassian()
    {
        float curAngle = 0.0f;
        while (curAngle < 360f)
        {
            yield return null;
        }
        //보정
        transform.position = startPos;
        transform.Rotate(Vector3.zero);
    }
}