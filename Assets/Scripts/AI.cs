using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
	//Type

	[SerializeField] float m_health = 10.0f;
	[SerializeField] float m_speed = 1.0f;
	[SerializeField] float m_value = 1.0f;

	private Vector2 m_direction;

	public float Value { get { return m_value; } }

	private void Start()
	{
		m_direction = Vector2.right;
	}

	void Update()
	{
		print(m_direction.x + " " + m_direction.y);

		Vector3 velocity = Vector3.zero;
		velocity.x = m_direction.x;
		velocity.y = m_direction.y;

		transform.position = transform.position + (velocity * Time.deltaTime * m_speed);
	}

	public void NewTile(PathPiece tile)
	{
		//possible other tile types

		PathPiece.eDirection current = tile.Contribution;

		switch (current)
		{
			case PathPiece.eDirection.UP:
				m_direction = Vector2.up;
				break;
			case PathPiece.eDirection.DOWN:
				m_direction = Vector2.down;
				break;
			case PathPiece.eDirection.LEFT:
				m_direction = Vector2.left;
				break;
			case PathPiece.eDirection.RIGHT:
				m_direction = Vector2.right;
				break;
			default:
				m_direction = Vector2.zero;
				break;
		}

		print(m_direction.x + " " + m_direction.y);
	}

	public void Attacked(float damage, string effect)
	{
		//future effects
		if (damage > 0) {
			m_health -= damage;
		}
	}
}
