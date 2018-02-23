using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePiece : MonoBehaviour
{
	[SerializeField] bool m_occupied = false;
	[SerializeField] GameObject m_tower = null;

	public bool Occupied { get { return m_occupied; } set { m_occupied = value; } }
	public GameObject Tower
	{
		get { return m_tower; }
		set
		{
			m_tower = value;
			if (m_tower != null)
			{
				m_occupied = true;
			}
			else
			{
				m_occupied = false;
			}
		}
	}
}
