using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eStatus
{
    BURN = 5,
    FREEZE = 4,
    NONE = 1,
    POISON = 6,
    SLOW = 2
}

[System.Serializable]
public struct Status
{
    public eStatus status;
    public float statusDamage;
    public float time;
}

public enum eTowerType
{
    BALISTA,
    CANNON,
    ICE,
    LAVA,
    MAGIC,
    POISON
}

[CreateAssetMenu(fileName = "Data", menuName = "Data/Tower", order = 1)]

public class TowerData : ScriptableObject
{   
    public float value;
    public float attackRadius;
    public float damage;
    public float attackRate;
    public eTowerType towerType;
    public Status towerStatus;
    //public float[] upgradeModifiers;
    //public float[] upgradeCosts;
    //public Sprite[] towers;
    public Sprite sprite;
    public float upgradeCost;
    
    public Projectile projectile;
}
