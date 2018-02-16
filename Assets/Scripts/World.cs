using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
	enum eObjective { ESCAPE, DEFEND }

	[SerializeField] PathPiece[] m_path;
	[SerializeField] eObjective m_task;

	float m_timer = 0.0f;

	void Start()
	{

	}

	void Update()
	{
		m_timer += Time.deltaTime;
	}
}
