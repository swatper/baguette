using UnityEngine;

/// <summary>
/// 항상 카메라를 바라보는 스크립트
/// </summary>
public class Billboard : MonoBehaviour
{
    [Tooltip("보여줄 문자")]
    [SerializeField] Transform text;
    [Tooltip("따라갈 대상")]
    [SerializeField] Transform target;

    void LateUpdate()
    {
        if (target == null)
            return;
        text.transform.rotation = target.rotation;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void ResetTarget()
    {
        target = null;
    }
}
