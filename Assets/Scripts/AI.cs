using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
	//Type
	struct Status
	{
		int m_status;
		float m_effectiveness;
	}

	[SerializeField] float m_health = 10.0f;
	[SerializeField] float m_speed = 1.0f;
	[SerializeField] float m_value = 1.0f;
	[SerializeField] float m_damage = 1.0f;

	Status m_status;
	Vector2 m_direction;
	int m_channel = 0;

	public float Value { get { return m_damage; } }
	public int Channel { get { return m_channel; } }

	private void Start()
	{
		m_direction = Vector2.right;
	}

	void Update()
	{
		if(m_health <= 0.0f)
		{
			Destroy(gameObject);
		}

		Vector3 velocity = Vector3.zero;
		velocity.x = m_direction.x;
		velocity.y = m_direction.y;

		transform.position = transform.position + (velocity * Time.deltaTime * m_speed);
	}

	private void OnDestroy()
	{
		World world = FindObjectOfType<World>();
		if (world)
		{
			world.AddToCoins(m_value);
		}
	}

	public void NewTile(PathPiece tile, int channel)
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

		m_channel = channel;
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
	}

	public void StatusChanged(int type, float damage)
	{

	}

	public void Attacked(float damage, string effect)
	{
		//future effects
		if (damage > 0) {
			m_health -= damage;
		}
	}
}
