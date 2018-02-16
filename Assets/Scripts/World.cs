using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
	enum eObjective { ESCAPE, DEFEND }

	[SerializeField] eObjective m_task;
	[SerializeField] float m_health;

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
}
