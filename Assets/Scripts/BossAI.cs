using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : AI
{
	[SerializeField] float m_destroyRadius = 5.0f;

	override protected void UpdateMovement()
	{
		Vector3 velocity = Vector3.right;
		velocity = velocity * m_speed * Time.deltaTime;
		transform.position = transform.position + velocity;

		CheckBorder();
	}

	void CheckBorder()
	{
		
	}
}
