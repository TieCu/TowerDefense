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

    [SerializeField] [Range(10.0f, 50000.0f)] float m_value;
    [SerializeField] [Range(1.0f, 50.0f)] float m_attackRadius = 15.0f;
    [SerializeField] [Range(1.0f, 1000.0f)] float m_damage = 10.0f;
    [SerializeField] [Range(.1f, 3.0f)] float m_attackRate = 1.0f;
    [SerializeField] eDamageType m_damageType;
    [SerializeField] float[] m_upgradeModifiers;
    [SerializeField] float[] m_upgradeCosts;
    [SerializeField] Sprite[] m_towers;

    public float value { get { return m_value; } }
    public float attackRadius { get { return m_attackRadius; } }
    public float damage { get { return m_damage; } }
    public float attackRate { get { return m_attackRate; } }

    SpriteRenderer m_spriteRenderer;
    GameObject m_target;
    List<GameObject> m_possibleTargets;
    AI m_enemyInfo;

    int m_towerIndex = 0;
    float m_attackTimer;

    void Start()
    {
        m_attackTimer = m_attackRate;

		CircleCollider2D circle = gameObject.GetComponent<CircleCollider2D>();
		if(circle)
		{
			circle.radius = m_attackRadius;
		}
		else
		{
			SphereCollider ball = gameObject.GetComponent<SphereCollider>();
			if(ball)
			{
				ball.radius = m_attackRadius;
			}
		}
    }
    
    void Update()
    {
        m_attackTimer -= Time.deltaTime;

        if(m_attackTimer <= 0.0f)
        {
            if (m_target)
            {
                m_enemyInfo.Attacked(m_damage, "Was Hit");

                m_attackTimer = m_attackRate;
            }            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            m_possibleTargets.Add(other.gameObject);

            if (!m_target)
            {
                m_target = other.gameObject;
                m_enemyInfo = other.gameObject.GetComponent<AI>();
            }
        }
    }

<<<<<<< HEAD
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Enemy")
		{
			if (!m_target)
			{
				m_target = collision.gameObject;
				m_enemyInfo = collision.gameObject.GetComponent<AI>();
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		
	}

	private void UpgradeTower()
    {       
        if(m_towerIndex < m_towers.Length || m_towerIndex < m_upgradeModifiers.Length)
=======
    private void OnTriggerExit(Collider other)
    {
        m_possibleTargets.Remove(other.gameObject);

        if(m_possibleTargets.Count > 0)
        {
            m_target = m_possibleTargets[0];
        }
        else
        {
            m_target = null;
        }
    }

    private void UpgradeTower()
    {
        if (World.Instance.RemoveCoins(m_upgradeCosts[m_towerIndex]))
>>>>>>> d5fa3b653269e99f6a40184c64c2df61a106bf33
        {
            if (m_towerIndex < m_towers.Length || m_towerIndex < m_upgradeModifiers.Length)
            {
                m_spriteRenderer.sprite = m_towers[m_towerIndex];
                m_damage *= m_upgradeModifiers[m_towerIndex];
                m_attackRadius *= m_upgradeModifiers[m_towerIndex];
                m_attackRate /= m_upgradeModifiers[m_towerIndex];

                m_towerIndex++;
            }
        }                
    }

    private void SellTower()
    {
        World.Instance.AddToCoins(m_value * .75f);
        Destroy(gameObject);
    }
}
