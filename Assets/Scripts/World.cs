﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class World : Singleton<World>
{
	enum eObjective { ESCAPE, DEFEND }

	[SerializeField] eObjective m_task;
	[SerializeField] float m_health;
    [SerializeField] float m_coins;
	[SerializeField] int m_maxPopulation;

	
    public GameObject projectileContainer;
    public GameObject towerContainer;
	[Header("Player Stats")]
	[SerializeField] TextMeshProUGUI m_TxtLife = null;
	[SerializeField] TextMeshProUGUI m_TxtMoney = null;

	float m_timer = 0.0f;

   

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
		m_timer += Time.deltaTime;

		var spawners = FindObjectsOfType<Spawner>();

		if(spawners != null)
		{
			int currentPop = 0;

			foreach (Spawner s in spawners)
			{
				currentPop += s.Population;
			}

			if(currentPop >= m_maxPopulation)
			{
				foreach (Spawner s in spawners)
				{
					s.SpawnerOn = false;
				}
			}
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
