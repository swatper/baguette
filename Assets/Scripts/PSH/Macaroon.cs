using UnityEngine;

public class Macaroon : KnockbackObject
{
    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        PlayerController player = other.GetComponent<PlayerController>();


        Destroy(gameObject);
    }
}
