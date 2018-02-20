using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
	//Type
	struct Status
	{
		internal int m_status; //0 Null, 1 Damage, 2 Slow, 3 Block, 4 Freeze, 5 Fire
		internal float m_effectiveness;
		internal float m_additionalData; //extra dot
	}

	[SerializeField] float m_health = 10.0f;
	[SerializeField] float m_speed = 1.0f;
	[SerializeField] float m_value = 1.0f;
	[SerializeField] float m_damage = 1.0f;

	List<Status> m_status;
	Vector2 m_direction;
	int m_channel = 0;

	public float Value { get { return m_damage; } }
	public int Channel { get { return m_channel; } }

	private void Start()
	{
		m_direction = Vector2.zero;

		m_status = new List<Status>();

		Status start = new Status();
		start.m_status = 0;
		m_status.Add(start);

	}

	void Update()
	{
		if (m_health <= 0.0f)
		{
			Destroy(gameObject);
		}

		float speed = m_speed;
		StatusEffect(ref speed);

		Vector3 velocity = Vector3.zero;
		velocity.x = m_direction.x;
		velocity.y = m_direction.y;

		transform.position = transform.position + (velocity * Time.deltaTime * speed);
	}

	private void StatusEffect(ref float speed)
	{
		for(int i=0;i<m_status.Count;i++)
		{
			switch (m_status[i].m_status)
			{
				case 1: //Damage
					Attacked(m_status[i].m_effectiveness * Time.deltaTime);
					break;
				case 2: //Slow
					if (m_status[i].m_effectiveness != 0)
					{
						speed /= m_status[i].m_effectiveness;
					}
					break;
				case 3: //Block
					speed = 0;
					break;
				case 4: //Freeze
					speed = 0;
					Attacked(Time.deltaTime * m_status[i].m_effectiveness);
					Status cold = m_status[i];
					cold.m_additionalData -= Time.deltaTime;
					m_status[i] = cold;

					if (m_status[i].m_additionalData <= 0.0f)
					{
						if (m_status.Count != 1)
						{
							m_status.Remove(m_status[i]);
						}
					}
					break;
				case 5: //Burn
					Attacked(Time.deltaTime * m_status[i].m_effectiveness);
					Status burn = m_status[i];
					burn.m_additionalData -= Time.deltaTime;
					m_status[i] = burn;

					if (m_status[i].m_additionalData <= 0.0f)
					{
						if (m_status.Count != 1)
						{
							m_status.Remove(m_status[i]);
						}
					}
					break;
				case 6: //Posion
					//Attacked(Time.deltaTime * m_status[i].m_additionalData);
					//Status weak = m_status[i];
					//if (weak.m_effectiveness != 0)
					//{
					//	speed /= weak.m_effectiveness;
					//}
					//weak.m_effectiveness -= Time.deltaTime;
					//if (weak.m_effectiveness > 0.0f && weak.m_effectiveness < 1.0f)
					//{
					//	weak.m_effectiveness = 1.0f;
					//}
					//m_status[i] = weak;
					break;
			}

		}

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

	public void StatusChanged(int type, float primaryValue, float addedData, bool AddRemove)
	{
		bool alreadyAffect = false;
		foreach (Status s in m_status)
		{
			if(s.m_status == type && s.m_effectiveness == primaryValue && s.m_additionalData == addedData)
			{
				if (!AddRemove)
				{
					m_status.Remove(s);
				}

				alreadyAffect = true;

				break;
			}
		}

		if (AddRemove && !alreadyAffect)
		{
			Status status = new Status();
			status.m_status = type;
			status.m_effectiveness = primaryValue;
			status.m_additionalData = addedData;
			m_status.Add(status);
		}

	}

	public void Attacked(float damage)
	{
		if (damage > 0) {
			m_health -= damage;
		}
	}
}
