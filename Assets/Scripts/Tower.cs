using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public enum eDamageType
    {
        BULLET,
        MISSLE
    }

    [SerializeField] float m_value;

    [SerializeField] [Range(1.0f, 50.0f)] float m_attackRadius = 15.0f;
    [SerializeField] [Range(1.0f, 1000.0f)] float m_damage = 10.0f;
    [SerializeField] [Range(.1f, 3.0f)] float m_attackRate = 1.0f;
    [SerializeField] eDamageType m_damageType;
    [SerializeField] float[] m_upgradeModifiers;
    [SerializeField] float[] m_upgradeCosts;
    [SerializeField] Sprite[] m_towers;

    SpriteRenderer m_spriteRenderer;
    GameObject m_target;
    AI m_enemyInfo;

    int m_towerIndex = 0;
    float m_attackTimer;

    void Start()
    {
        m_attackTimer = m_attackRate;
    }
    
    void Update()
    {
        m_attackTimer -= Time.deltaTime;

        if(m_attackTimer <= 0.0f)
        {
            if (m_target)
            {
                m_enemyInfo.TakeDamage(m_damage);

                m_attackTimer = m_attackRate;
            }            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if (!m_target)
            {
                m_target = other.gameObject;
                m_enemyInfo = other.gameObject.GetComponent<AI>();
            }
        }
    }

    private void UpgradeTower()
    {       
        if(m_towerIndex < m_towers.Length || m_towerIndex < m_upgradeModifiers.Length)
        {
            m_spriteRenderer.sprite = m_towers[m_towerIndex];
            m_damage *= m_upgradeModifiers[m_towerIndex];
            m_attackRadius *= m_upgradeModifiers[m_towerIndex];
            m_attackRate /= m_upgradeModifiers[m_towerIndex];

            m_towerIndex++;
        }        
    }

    private void SellTower()
    {
        Game.Instance.AddMoney(m_value * .75);
        Destroy(gameObject);
    }
}
