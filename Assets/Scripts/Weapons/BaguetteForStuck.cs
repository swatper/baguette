using UnityEngine;
using System.Collections;

public class BaguetteForStuck : MonoBehaviour
{
    [SerializeField] float stuckTime;
    void OnEnable()
    {
        StartCoroutine(RemoveStuckBread());
    }

    IEnumerator RemoveStuckBread()
    {
        yield return new WaitForSeconds(stuckTime);
        Destroy(gameObject);
    }
}
