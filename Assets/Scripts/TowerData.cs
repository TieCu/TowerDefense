using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/Tower", order = 1)]
public class TowerData : ScriptableObject
{
    public enum eTowerType
    {
        POISON,
        CANNON,
        ICE,
        LAVA,
        BALISTA,
        MAGIC
    }

    public float value;
    public float attackRadius;
    public float damage;
    public float attackRate;
    public eTowerType towerType;
    public float[] upgradeModifiers;
    public float[] upgradeCosts;
    public Sprite[] towers;

    public GameObject emitter;
    public Projectile projectile;
    public Projectile projectileContainer;
}
