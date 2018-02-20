using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obsticles : MonoBehaviour
{
	public enum eEffect { NULL, DAMAGE, SLOW, BLOCK, FREEZE, BURN }

	[SerializeField] float m_cost = 1.0f;
	[SerializeField] float m_size = 1.0f;
	[SerializeField] [Range(0.1f, 100.0f)] float m_rating = 1.0f;
	[SerializeField] float m_extraDataSlot = 0.0f;
	[SerializeField] float m_charge = 10;
	[SerializeField] eEffect m_effect;

	public float Cost { get { return m_cost; } }

	int m_blocking = 0;
	bool m_isDOT = false;

	private void Start()
	{
		transform.localScale = transform.localScale * m_size;

		switch(m_effect)
		{
			case eEffect.FREEZE:
				m_isDOT = true;
				break;
			case eEffect.BURN:
				m_isDOT = true;
				break;
			default:
				m_isDOT = false;
				break;
		}
	}

	private void Update()
	{
		if(m_charge <= 0)
		{
			Destroy(gameObject);
		}

		if(m_blocking > 0 && m_effect == eEffect.BLOCK)
		{
			m_charge -= (Time.deltaTime / m_rating) * m_blocking ;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (m_effect != eEffect.NULL)
		{
			AI bot = other.gameObject.GetComponent<AI>();
			if (bot)
			{
				if (m_effect != eEffect.BLOCK)
				{
					m_charge -= 1;
				}
				else if (m_effect == eEffect.BLOCK)
				{
					m_blocking++;
				}

				if(m_isDOT)
				{
					bot.StatusChanged((int)m_effect, m_rating);
					bot.DOT(m_extraDataSlot);
				}
			}
		}

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (m_effect != eEffect.NULL)
		{
			AI bot = collision.gameObject.GetComponent<AI>();
			if (bot)
			{
				if (m_effect != eEffect.BLOCK)
				{
					m_charge -= 1;
				}
				else if (m_effect == eEffect.BLOCK)
				{
					m_blocking++;
				}

				if (m_isDOT)
				{
					bot.StatusChanged((int)m_effect, m_rating);
					bot.DOT(m_extraDataSlot);
				}
			}
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (m_effect != eEffect.NULL || !m_isDOT)
		{
			AI bot = other.gameObject.GetComponent<AI>();
			if (bot)
			{
				bot.StatusChanged((int)m_effect, m_rating);
			}
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (m_effect != eEffect.NULL || !m_isDOT)
		{
			AI bot = collision.gameObject.GetComponent<AI>();
			if (bot)
			{
				bot.StatusChanged((int)m_effect, m_rating);
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		AI bot = other.gameObject.GetComponent<AI>();
		if (bot)
		{
			if (m_effect == eEffect.BLOCK)
			{
				m_blocking--;
			}

			bot.StatusChanged((int)eEffect.NULL, m_rating);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		AI bot = collision.gameObject.GetComponent<AI>();
		if (bot)
		{
			if (m_effect == eEffect.BLOCK)
			{
				m_blocking--;
			}

			if (!m_isDOT)
			{
				bot.StatusChanged((int)eEffect.NULL, m_rating);
			}
		}
	}
}
