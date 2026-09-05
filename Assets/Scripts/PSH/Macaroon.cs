using System;
using UnityEngine;

public class Macaroon : KnockbackObject
{
    [SerializeField] ShopManager shop;
    [SerializeField] BreadCounter breadUI;
    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        PlayerController player = other.GetComponent<PlayerController>();
        //개초딩 게임 ON
        player.SetMaxHealth(20);
        player.UpgradeBreadBag(100);
        player.AddCurBread(100);
        player.SetPlayerSpeed(20);
        Managers.Money.Money = 5599.9f; //에어컨 못 사지롱~

        shop.SetBreadValueText();
        breadUI.ReserBreadCountForce();

        Destroy(gameObject);
    }
}
