using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Stat
{
    [Tooltip("가격")]
    public float price;
    [Tooltip("증가량")]
    public float amount;
}

[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObject/Upgrade", order = 1)]
public class StoreStatUpgrade : ScriptableObject
{
    public string upgradeName;
    [Header("레벨 테이블")]
    public List<Stat> upgradeTable;
}
