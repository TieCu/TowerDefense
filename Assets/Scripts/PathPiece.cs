﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPiece : TilePiece
{
	public enum eDirection { UP, DOWN, LEFT, RIGHT }
	public enum eFunction { PATH, SPAWN, FINISH }

	[SerializeField] eDirection m_contribute;
	[SerializeField] eFunction m_purpose;
	[SerializeField] AI m_spawns = null;
	[SerializeField] float m_spawnTime = 1.0f;

	float m_spawnTimer = 0.0f;

	public eDirection Contribution { get { return m_contribute; } }
	public eFunction Purpose { get { return m_purpose; } }
	public bool SpawnerOn { get; set; }

	void Update()
	{
		if (m_spawnTimer >= m_spawnTime)
		{
			if (m_spawns && SpawnerOn)
			{
				Instantiate<AI>(m_spawns);
				m_spawns.NewTile(this);
			}

			m_spawnTimer = 0.0f;
		}
		else
		{
			m_spawnTimer += Time.deltaTime;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		AI ai = collision.GetComponent<AI>();
		if (ai)
		{
			if (m_purpose == eFunction.FINISH)
			{
				World world = FindObjectOfType<World>();
				if(world)
				{
					world.Invaded(ai.Value);
					Destroy(collision.gameObject);
				}
			}
			else
			{
				ai.NewTile(this);
			}
		}
	}
}