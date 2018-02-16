using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
	//Type

	[SerializeField] float m_health = 10.0f;
	[SerializeField] float m_speed = 1.0f;
	[SerializeField] float m_value = 1.0f;

	PathPiece.eDirection m_direction;

	public float Value { get { return m_value; } }

	void Update()
	{
		Vector2 direction = Vector2.zero;

		switch(m_direction)
		{
			case PathPiece.eDirection.UP:
				direction = Vector2.up * m_speed;
				break;
			case PathPiece.eDirection.DOWN:
				direction = Vector2.down * m_speed;
				break;
			case PathPiece.eDirection.LEFT:
				direction = Vector2.left * m_speed;
				break;
			case PathPiece.eDirection.RIGHT:
				direction = Vector2.right * m_speed;
				break;
			default:
				direction = Vector2.zero;
				break;
		}

		Vector3 velocity = Vector3.zero;
		velocity.x = direction.x;
		velocity.y = direction.y;

		transform.position = transform.position + velocity * Time.deltaTime;
	}

	public void NewTile(PathPiece tile)
	{
		//possible other tile types

		PathPiece.eDirection current = tile.Contribution;

		if(current != m_direction)
		{
			m_direction = current;
		}
	}
}
