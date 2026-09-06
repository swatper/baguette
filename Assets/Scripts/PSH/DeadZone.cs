using UnityEngine;

public class DeadZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.CompareTag("Enemy"))
            obj.GetComponent<EnemyController>().EnemyHit(5);
        else if (obj.CompareTag("Player"))
            obj.GetComponent<PlayerController>().TakeDamage(200);
        else
            Destroy(other.gameObject);
    }
}
