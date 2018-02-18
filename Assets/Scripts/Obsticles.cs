using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obsticles : MonoBehaviour
{
	public enum eEffect { SLOW, DAMAGE, BLOCK }

	[SerializeField] float m_cost = 1.0f;
	[SerializeField] float m_size = 1.0f;
	[SerializeField] float m_rating = 1.0f;
	[SerializeField] int m_charge = 10;
	[SerializeField] eEffect m_effect;

	private void Update()
	{
		if(m_charge <= 0)
		{
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		AI bot = other.gameObject.GetComponent<AI>();
		if (bot)
		{
			m_charge -= 1;
		}

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		AI bot = collision.gameObject.GetComponent<AI>();
		if (bot)
		{
			print((int)m_effect);

			if (m_effect != eEffect.DAMAGE)
			{
				bot.StatusChanged((int)m_effect, m_rating);
			}
			if (m_effect != eEffect.BLOCK)
			{
				m_charge -= 1;
			}

		}
	}

	private void OnTriggerStay(Collider other)
	{
		AI bot = other.gameObject.GetComponent<AI>();
		if (bot)
		{
			if (m_effect == eEffect.DAMAGE)
			{
				bot.Attacked(m_rating * Time.deltaTime, "Nothing yet");
				print("Damaging");
			}
		}

	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		AI bot = collision.gameObject.GetComponent<AI>();
		if (bot)
		{
			if (m_effect == eEffect.DAMAGE) {
				bot.Attacked(m_rating * Time.deltaTime, "Nothing yet");
				print("Damaging");
			}
		}

	}
}
