using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePiece : MonoBehaviour
{
	[SerializeField] Sprite m_texture = null;

	private void Start()
	{
		MeshRenderer rend = gameObject.GetComponent<MeshRenderer>();
		if(rend)
		{
			rend.material.SetTexture("_MainTex", m_texture.texture);
		}
	}
}
