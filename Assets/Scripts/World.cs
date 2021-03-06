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

    public GameObject m_projectileContainer;
    public GameObject m_towerContainer;
    public GameObject m_AIContainer;


	[Header("Player Stats")]
	[SerializeField] TextMeshProUGUI m_TxtLife = null;
	[SerializeField] TextMeshProUGUI m_TxtMoney = null;

    [Header("Dialogue Stuff")]
    [SerializeField] GameObject m_endRound1;
    [SerializeField] GameObject m_endRound2;
    [SerializeField] GameObject m_endRound3;
    [SerializeField] GameObject m_endRound7;
    [SerializeField] GameObject m_endRound9;
    [SerializeField] GameObject m_win;
    [SerializeField] GameObject m_lose;
	[SerializeField] GameObject m_musicPlayer;

	Spawner[] m_spawners;
    bool m_gettingReady = true;
	bool m_paused = false;
	bool m_populationMaxed = false;
	int m_deadPopulation = 0;
	float m_timer = 0.0f;
	int m_roundIndex = 0;

    public int RoundIndex { get { return m_roundIndex; } }
	float m_health;
	float m_coins;
	int m_maxPopulation;


	void Start()
	{
		if (m_roundIndex < m_rounds.Length)
		{
			NewLevel(m_rounds[m_roundIndex]);
		}

		m_spawners = FindObjectsOfType<Spawner>();
	}

	public void NewLevel(Round round)
	{
		print("New Level: " + m_roundIndex);

		if (m_spawners != null && m_gettingReady)
		{
			m_populationMaxed = false;
			foreach (Spawner s in m_spawners)
			{
				s.SpawnerOn = false;
				s.Reset();
			}
		}

		m_health = round.m_health;
		m_maxPopulation = round.m_maxPopulation;
		m_coins += round.m_coins;
		m_deadPopulation = 0;

		m_TxtMoney.text = "$" + m_coins.ToString();
		m_TxtLife.text = m_health.ToString();
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

		if(!m_gettingReady && m_deadPopulation == m_maxPopulation && m_populationMaxed && m_health > 0)
		{
			print("Round " + m_roundIndex + " won");

			m_gettingReady = true;
			m_roundIndex++;
			if (m_roundIndex < m_rounds.Length)
			{
				if (m_roundIndex == 0 && m_endRound1 != null)
				{
					m_endRound1.SetActive(true);
				}
				else if (m_roundIndex == 1 && m_endRound2 != null)
				{
					m_endRound2.SetActive(true);
				}
				else if (m_roundIndex == 2 && m_endRound3 != null)
				{
					m_endRound3.SetActive(true);
				}
				else if (m_roundIndex == 6 && m_endRound7 != null)
				{
					m_endRound7.SetActive(true);
				}
				else if (m_roundIndex == 8 && m_endRound9 != null)
				{
					m_endRound9.SetActive(true);
				}

				NewLevel(m_rounds[m_roundIndex]);
			}
			else
			{
				GameOver(true);
			}
		}

        if (m_health <= 0)
		{
			GameOver(false);
		}
	}

	void GameOver(bool winLose)
	{
		print("Game over");
		if(winLose)
		{
            m_win.SetActive(true);
			print("You won");
			Time.timeScale = 0.0f;
		}
		else
		{
			print("You lost");
			
			print(m_AIContainer.transform.childCount);
			for(int i=0;i<m_AIContainer.transform.childCount;i++)
			{
				Destroy(m_AIContainer.transform.GetChild(i).gameObject);
			}
			foreach(Transform child in m_towerContainer.transform)
			{
				Destroy(child.gameObject);
			}

			m_coins = 0;
			m_gettingReady = true;
			m_deadPopulation = 0;
			m_populationMaxed = false;
			NewLevel(m_rounds[m_roundIndex]);

			m_lose.SetActive(true);
		}
	}

	public void NextRound()
	{
		if (m_spawners != null)
		{
			foreach (Spawner s in m_spawners)
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

		m_gettingReady = false;
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
