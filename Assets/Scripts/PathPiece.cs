using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPiece : TilePiece
{
	public enum eDirection { UP, DOWN, LEFT, RIGHT }
	public enum eFunction { PATH, SPAWN, FINISH }

	[SerializeField] eDirection m_contribute;
	[SerializeField] eFunction m_purpose;
	[SerializeField] GameObject m_spawns = null;

	public eDirection Contribution { get { return m_contribute; } }
	public eFunction Purpose { get { return m_purpose; } }

	void Update()
	{
		if(m_spawns)
		{
			Instantiate<GameObject>(m_spawns);
		}
	}
}
