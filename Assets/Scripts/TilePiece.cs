using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePiece : MonoBehaviour
{
	[SerializeField] bool m_occupied = false;

	public bool Occupied { get { return m_occupied; } set { m_occupied = value; } }
}
