﻿using System.Collections;
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
	[Header("Player Stats")]
	[SerializeField] TextMeshProUGUI m_TxtLife = null;
	[SerializeField] TextMeshProUGUI m_TxtMoney = null;

	float m_timer = 0.0f;
	int m_roundIndex = 0;
	float m_health;
	float m_coins;
	int m_maxPopulation;
	bool m_isPaused = true;


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
	
}
