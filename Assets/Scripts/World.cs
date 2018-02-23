﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class World : Singleton<World>
{
	enum eObjective { ESCAPE, DEFEND }
    
    [System.Serializable]
	public struct Round
	{
		public float m_health;
		public float m_coins;
		//public float m_delay;
		public int m_maxPopulation;
	}

	[SerializeField] eObjective m_task;
	[SerializeField] Round[] m_rounds;

    public GameObject projectileContainer;
    public GameObject towerContainer;

	[Header("Player Stats")]
	[SerializeField] TextMeshProUGUI m_TxtLife = null;
	[SerializeField] TextMeshProUGUI m_TxtMoney = null;

	bool m_gettingReady = true;
	bool m_paused = false;
	bool m_populationMaxed = false;
	int m_deadPopulation = 0;
	float m_timer = 0.0f;
	int m_roundIndex = 0;
	float m_health;
	float m_coins;
	int m_maxPopulation;


	void Start()
	{
		m_TxtMoney.text = "$" + m_coins.ToString();
		m_TxtLife.text = m_health.ToString();


		if (m_roundIndex < m_rounds.Length)
		{
			NewLevel(m_rounds[m_roundIndex]);
		}
	}

	public void NewLevel(Round round)
	{
		print("New Level");
		m_health = round.m_health;
		m_maxPopulation = round.m_maxPopulation;
		m_coins += round.m_coins;
	}

	void Update()
	{
        if (Input.GetKeyDown(KeyCode.Escape))
        {
			Paused();
        }

		UpdateSpawners();

		if (m_gettingReady)
		{
			m_timer += Time.deltaTime;
		}

		if(!m_gettingReady && m_deadPopulation == m_maxPopulation && m_populationMaxed)
		{
			m_gettingReady = true;
			m_roundIndex++;
			if (m_roundIndex < m_rounds.Length)
			{
				NewLevel(m_rounds[m_roundIndex]);
			}
		}

        print("Wave " + m_roundIndex + ", " + m_health + " health, " + m_coins + " coins, " + m_deadPopulation + " dead");
	}

	public void Clicked()
	{
		print("Clicked");
	}

	public void NextRound()
	{
		m_gettingReady = false;

		var spawners = FindObjectsOfType<Spawner>();
		if (spawners != null)
		{
			if (m_gettingReady)
			{
				m_populationMaxed = false;
				foreach (Spawner s in spawners)
				{
					s.SpawnerOn = false;
				}
			}
			else
			{
				foreach (Spawner s in spawners)
				{
					foreach (int i in s.OnRounds)
					{
						if (i == m_roundIndex)
						{
							s.SpawnerOn = true;
							print(s.name + " is on");
						}
					}
				}

			}
		}
	}

	void UpdateSpawners()
	{
		var spawners = FindObjectsOfType<Spawner>();

		if (spawners != null)
		{
			int currentPop = 0;

			foreach (Spawner s in spawners)
			{
				currentPop += s.Population;
			}

			if (currentPop >= m_maxPopulation)
			{
				m_populationMaxed = true;
				foreach (Spawner s in spawners)
				{
					s.SpawnerOn = false;
				}
			}
		}
	}

	public void Paused()
	{
		m_paused = !m_paused;

		if (m_paused)
		{
			print("Paused");
			Time.timeScale = 0.0f;
		}
		else
		{
			print("Unpaused");
			Time.timeScale = 1.0f;
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
