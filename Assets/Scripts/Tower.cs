using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] TowerData m_tower1Data;
    [SerializeField] TowerData m_tower2Data;
    [SerializeField] TowerData m_tower3Data;

    [SerializeField] GameObject m_emitter;

    float m_value;
    float m_attackRadius;
    float m_damage;
    float m_attackRate;
    eTowerType m_towerType;
    Status m_towerStatus;
    Sprite m_sprite;
    float m_upgradeCost;
    int numUpgrades = 2;

    public Projectile projectile;

    public float value { get { return m_value; } }
    public float attackRadius { get { return m_attackRadius; } }
    public float damage { get { return m_damage; } }
    public float attackRate { get { return m_attackRate; } }

    public bool fullyUpgraded = false;

    SpriteRenderer m_spriteRenderer;
    GameObject m_target;
    List<GameObject> m_possibleTargets = new List<GameObject>();
    AI m_enemyInfo;

    int m_towerIndex = 0;
    float m_attackTimer;

    void Start()
    {
        m_value = m_tower1Data.value;
        m_attackRadius = m_tower1Data.attackRadius;
        m_damage = m_tower1Data.damage;
        m_attackRate = m_tower1Data.attackRate;
        m_towerType = m_tower1Data.towerType;
        m_towerStatus = m_tower1Data.towerStatus;
        m_sprite = m_tower1Data.sprite;
        m_upgradeCost = m_tower1Data.upgradeCost;

        m_attackTimer = m_attackRate;

        m_spriteRenderer = GetComponent<SpriteRenderer>();

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
                Projectile bullet = Instantiate(projectile, m_emitter.transform.position, Quaternion.identity, World.Instance.m_projectileContainer.transform);
                bullet.SetTarget(m_target);
                bullet.SetDamage_Status(m_damage, m_towerStatus);

                m_attackTimer = m_attackRate;
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

        if (!fullyUpgraded && World.Instance.RemoveCoins(m_upgradeCost))
        {
            if(m_towerIndex == 0)
            {
                m_spriteRenderer.sprite = m_tower2Data.sprite;
                m_damage *= m_tower2Data.damage;
                m_attackRadius *= m_tower2Data.attackRadius;
                m_attackRate /= m_tower2Data.attackRate;
                m_value = m_tower2Data.value;
                m_towerIndex++;
            }
            else if (m_towerIndex == 1)
            {
                m_spriteRenderer.sprite = m_tower3Data.sprite;
                m_damage *= m_tower3Data.damage;
                m_attackRadius *= m_tower3Data.attackRadius;
                m_attackRate /= m_tower3Data.attackRate;
                m_value = m_tower3Data.value;
                m_towerIndex++;
            }
        }
    }

    public void SellTower()
    {
        World.Instance.AddToCoins(m_value * .75f);
        Destroy(gameObject);
    }
}
