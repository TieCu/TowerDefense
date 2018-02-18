using System.Collections;
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
	[SerializeField] int m_channel = 0;

	float m_spawnTimer = 0.0f;

	public eDirection Contribution { get { return m_contribute; } }
	public eFunction Purpose { get { return m_purpose; } }

	void Update()
	{
		if (m_spawnTimer >= m_spawnTime)
		{
			if (Purpose == eFunction.SPAWN && m_spawns)
			{
				Spawner spawn = gameObject.GetComponent<Spawner>();
				if (spawn)
				{
					if (spawn.SpawnerOn)
					{
						GameObject go = spawn.Spawn(m_spawns.gameObject);
						print(m_contribute);
						go.GetComponent<AI>().NewTile(this, m_channel);
					}
				}
				else
				{
					AI bot  = Instantiate<AI>(m_spawns, transform.position + (Vector3.back / 2), Quaternion.identity);
					bot.NewTile(this, m_channel);
				}
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
			if (ai.Channel == m_channel || m_channel == 0)
			{
				if (m_purpose == eFunction.FINISH)
				{
					World world = FindObjectOfType<World>();
					if (world)
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

	private void OnTriggerEnter(Collider other)
	{
		AI ai = other.GetComponent<AI>();
		if (ai)
		{
			if (ai.Channel == m_channel || m_channel == 0)
			{
				if (m_purpose == eFunction.FINISH)
				{
					World world = FindObjectOfType<World>();
					if (world)
					{
						world.Invaded(ai.Value);
						Destroy(other.gameObject);
					}
				}
				else
				{
					ai.NewTile(this);
				}
			}
		}

	}
}
