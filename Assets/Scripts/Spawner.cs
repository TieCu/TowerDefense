using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] Transform m_bin;

	int m_population = 0;

	public int Population { get { return m_population; } }
	public Transform Bin { get { return m_bin; } }

	public void PopulationIncrease()
	{
		m_population++;
	}
}
