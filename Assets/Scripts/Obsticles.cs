using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obsticles : MonoBehaviour
{
	enum eEffect { SLOW, DAMAGE, BLOCK }

	[SerializeField] float m_cost = 1.0f;
	[SerializeField] float m_health = 10.0f;
	[SerializeField] float m_size = 1.0f;
	[SerializeField] float m_rating = 1.0f;
	[SerializeField] eEffect m_effect;

	private void Update()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, m_size);

		if(colliders != null)
		{
			print("There's something here");
			foreach (Collider c in colliders)
			{
				print(c.name + " is here");

				AI bot = c.gameObject.GetComponent<AI>();
				if(bot)
				{
					SomeoneThere(bot);
				}
			}
		}
	}

	private void SomeoneThere(AI person)
	{
		switch(m_effect)
		{
			case eEffect.BLOCK:
				//Ai status change blocked
				break;
			case eEffect.SLOW:
				//Ai status change slowed
				break;
			case eEffect.DAMAGE:
				person.Attacked(m_rating * Time.deltaTime, "Nothing yet");
				print("Damaging");
				break;
		}
	}
}
