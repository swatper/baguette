using System;
using UnityEngine;

public class KnockbackObject : MonoBehaviour
{
    [Header("맞고 밀려날 타켓")]
    [SerializeField] Rigidbody target;
    [SerializeField] float knockbackForce;

    public void ApplyKnockback(Vector3 attackerPos)
    {
        //피격 방향 계산하기
        Vector3 knockbackDir = transform.position - attackerPos;
        knockbackDir.y = 0f;
        knockbackDir.Normalize();

        target.AddForce(knockbackDir * 5.0f, ForceMode.Impulse);
    }
}
