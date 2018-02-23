using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] TowerData m_data;

    [SerializeField] GameObject m_emitter;

    public float value { get { return m_data.value; } }
    public float attackRadius { get { return m_data.attackRadius; } }
    public float damage { get { return m_data.damage; } }
    public float attackRate { get { return m_data.attackRate; } }

    public bool fullyUpgraded = false;

    SpriteRenderer m_spriteRenderer;
    GameObject m_target;
    List<GameObject> m_possibleTargets = new List<GameObject>();
    AI m_enemyInfo;

    int m_towerIndex = 0;
    float m_attackTimer;

    void Start()
    { 
        m_attackTimer = m_data.attackRate;

        m_spriteRenderer = GetComponent<SpriteRenderer>();

		CircleCollider2D circle = gameObject.GetComponent<CircleCollider2D>();
		if(circle)
		{
			circle.radius = m_data.attackRadius;
		}
		else
		{
			SphereCollider ball = gameObject.GetComponent<SphereCollider>();
			if(ball)
			{
				ball.radius = m_data.attackRadius;
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
                Projectile bullet = Instantiate(m_data.projectile, m_emitter.transform.position, Quaternion.identity, World.Instance.m_projectileContainer.transform);
                bullet.SetTarget(m_target);
                bullet.SetDamage_Status(m_data.damage, m_data.towerStatus);

                m_attackTimer = m_data.attackRate;
				Destroy(bullet, 10.0f);
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

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Enemy")
		{
            m_possibleTargets.Add(collision.gameObject);

            if (!m_target)
			{
				m_target = collision.gameObject;
				m_enemyInfo = collision.gameObject.GetComponent<AI>();
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
        m_possibleTargets.Remove(other.gameObject);

        if (m_possibleTargets.Count > 0)
        {
            m_target = m_possibleTargets[0];
        }
        else
        {
            m_target = null;
        }
    }

	private void OnTriggerExit2D(Collider2D collision)
	{
        m_possibleTargets.Remove(collision.gameObject);

        if (m_possibleTargets.Count > 0)
        {
            m_target = m_possibleTargets[0];
        }
        else
        {
            m_target = null;
        }
    }

	public void UpgradeTower()
    {
        if (!fullyUpgraded && World.Instance.RemoveCoins(m_data.upgradeCosts[m_towerIndex]))
        {
            if (m_towerIndex < m_data.towers.Length || m_towerIndex < m_data.upgradeModifiers.Length)
            {
                m_spriteRenderer.sprite = m_data.towers[m_towerIndex];
                m_data.damage *= m_data.upgradeModifiers[m_towerIndex];
                m_data.attackRadius *= m_data.upgradeModifiers[m_towerIndex];
                m_data.attackRate /= (m_data.upgradeModifiers[m_towerIndex] - .3f);
                m_data.value = m_data.upgradeCosts[m_towerIndex];
                m_towerIndex++;

                fullyUpgraded = (m_towerIndex == m_data.towers.Length);
            }
        }                
    }

    public void SellTower()
    {
        World.Instance.AddToCoins(m_data.value * .75f);
        Destroy(gameObject);
    }
}
