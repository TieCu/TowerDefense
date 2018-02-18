using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : Singleton<World>
{
	enum eObjective { ESCAPE, DEFEND }

	[SerializeField] eObjective m_task;
	[SerializeField] float m_health;
    [SerializeField] float m_coins;

	float m_timer = 0.0f;

	void Start()
	{

	}

	void Update()
	{
		m_timer += Time.deltaTime;
	}

	public void Invaded(float value)
	{
		m_health -= value;
	}

    public void AddToCoins(float newCoins)
    {
        m_coins += newCoins;
    }

    public bool RemoveCoins(float cost)
    {
        bool wasSuccessful = false;

        if(cost <= m_coins)
        {
            m_coins -= cost;
            wasSuccessful = true;
        }

        return wasSuccessful;       
    }
}
