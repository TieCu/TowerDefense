using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class World : Singleton<World>
{
	enum eObjective { ESCAPE, DEFEND }

	[SerializeField] eObjective m_task;
	[SerializeField] float[] m_healthRound;
    [SerializeField] float[] m_coinsRound;
	[SerializeField] float[] m_roundDelay;
	[SerializeField] int[] m_maxPopulationRound;

	
    public GameObject projectileContainer;
    public GameObject towerContainer;
	int m_deadPopulation = 0;
	[Header("Player Stats")]
	[SerializeField] TextMeshProUGUI m_TxtLife = null;
	[SerializeField] TextMeshProUGUI m_TxtMoney = null;

	float m_timer = 0.0f;
	int m_roundIndex = 0;
	float m_health;
	float m_coins;
	int m_maxPopulation;
	bool m_isPaused = false;


	void Start()
	{
		m_TxtMoney.text = "$" + m_coins.ToString();
		m_TxtLife.text = m_health.ToString();
	}

	public void NewLevel(int population, float health, float startBonus)
	{
		m_health = health;
		m_maxPopulation = population;
		m_coins += startBonus;
	}

	void Update()
	{
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_isPaused = true;
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            m_isPaused = false;
        }

		if (m_roundIndex < m_healthRound.Length && m_roundIndex < m_coinsRound.Length && m_roundIndex < m_maxPopulationRound.Length)
		{
			NewLevel(m_maxPopulationRound[m_roundIndex], m_healthRound[m_roundIndex], m_coinsRound[m_roundIndex]);
		}

		if (!m_isPaused)
		{
			m_timer += Time.deltaTime;
		}

		var spawners = FindObjectsOfType<Spawner>();

		if (spawners != null)
		{
			if (m_isPaused)
			{
				foreach(Spawner s in spawners)
				{
					s.SpawnerOn = false;
				}
			}
			else
			{

				int currentPop = 0;

				foreach (Spawner s in spawners)
				{
					currentPop += s.Population;
				}

				if (currentPop >= m_maxPopulation)
				{
					foreach (Spawner s in spawners)
					{
						s.SpawnerOn = false;
					}
				}
			}
		}

		if(m_timer >= m_roundDelay[m_roundIndex])
		{
			m_isPaused = false;
			m_timer = 0.0f;
		} 
	}

	public void Invaded(float value)
	{
		m_health -= value;
		m_TxtLife.text = m_health.ToString();
	}

    public void AddToCoins(float newCoins)
    {
        m_coins += newCoins;
		m_TxtMoney.text = "$" + m_coins.ToString();
    }

	public void DeadNow()
	{
		m_deadPopulation++;
	}

    public bool RemoveCoins(float cost)
    {
        bool wasSuccessful = false;

        if(cost <= m_coins)
        {
            m_coins -= cost;
            wasSuccessful = true;
        }
		m_TxtMoney.text = "$" + m_coins.ToString();

		return wasSuccessful;     
    }

    public GameObject GetNearestGameObject(GameObject sourceGameObject, string tag, float maxDistance = float.MaxValue)
    {
        GameObject nearest = null;

        GameObject[] gameObjects;
        gameObjects = GameObject.FindGameObjectsWithTag(tag);

        if (gameObjects.Length > 0)
        {
            float nearestDistance = float.MaxValue;
            for (int i = 0; i < gameObjects.Length; i++)
            {
                if (gameObjects[i] != sourceGameObject)
                {
                    float distance = (sourceGameObject.transform.position - gameObjects[i].transform.position).magnitude;
                    if (distance < nearestDistance)
                    {
                        nearest = gameObjects[i];
                        nearestDistance = distance;
                    }
                }
            }

            if (nearestDistance > maxDistance) nearest = null;
        }

        return nearest;
    }
}
