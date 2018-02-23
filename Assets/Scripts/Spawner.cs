using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] Transform m_bin;
	[SerializeField] int[] m_rounds;
	[SerializeField] bool m_spawnerOn;

	int m_population = 0;

	public int Population { get { return m_population; } }
	public int[] OnRounds{ get { return m_rounds; } }
	public Transform Bin { get { return m_bin; } }
	public bool SpawnerOn { get { return m_spawnerOn; } set { m_spawnerOn = value; } }

	public void PopulationIncrease()
	{
		m_population++;
	}

	public GameObject Spawn(GameObject go)
	{
		GameObject copy = null;
		if (Bin)
		{
			copy = Instantiate(go, transform.position + (Vector3.back / 2), Quaternion.identity, Bin);
		}
		else
		{
			copy = Instantiate(go, transform.position + (Vector3.back / 2), Quaternion.identity);
		}

		PopulationIncrease();

		return copy;
	}
}
