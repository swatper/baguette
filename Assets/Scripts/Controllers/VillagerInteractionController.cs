using Unity.VisualScripting;
using UnityEngine;

public class VillagerInteractionController : MonoBehaviour
{
    [Tooltip("PlayerController에서 WeaponHandler를 가져오는곳")]
    [SerializeField] private WeaponHandler weaponHandler;

    public WeaponHandler GetWeaponHandler()
    {
        return weaponHandler;
    }

    GameObject _roof;
    public GameObject Roof { get { return _roof; } set { _roof = value; } }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerController player = other.GetOrAddComponent<PlayerController>();
        Managers.Deliver.CompleteDelivery(player, this);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type">0: 대면 배달, 1: 비대면 배달</param>
    public void TakeBaguette()
    {
        Managers.Deliver.CheckDelivery(this, 1);
    }
}
