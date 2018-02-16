using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPiece : MonoBehaviour
{
	enum eDirection { UP, DOWN, LEFT, RIGHT }
	enum eFunction { PATH, SPAWN, FINISH }

	[SerializeField] eDirection m_contribute;
	[SerializeField] eFunction m_purpose;

	void Start()
	{

	}

	void Update()
	{

	}
}
