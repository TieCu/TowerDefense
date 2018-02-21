using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSelection : MonoBehaviour {

	[SerializeField] Tower m_tower = null;
	[SerializeField] LayerMask m_placeable;
	private TilePiece m_currentTile = null;
	private TilePiece m_priorTile = null;
	private Color m_currTileColor;
	private Color m_actualColor;

	void Start () {
		
	}
	
	void Update () {
	//	if(m_tower != null)
		{
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 10, m_placeable);
			if(hit.collider != null)
			{

				if (hit.collider.gameObject.GetComponent<TilePiece>().Occupied == false)
				{
					m_currentTile = hit.collider.gameObject.GetComponent<TilePiece>();
					if (m_priorTile == m_currentTile)
					{

					} else
					{
						if(m_priorTile == null) m_priorTile = m_currentTile;
						Renderer temp = hit.transform.gameObject.GetComponent<Renderer>();
						m_actualColor = temp.material.color;
						m_priorTile.gameObject.GetComponent<Renderer>().material.color = m_actualColor;
						m_priorTile = m_currentTile;
						m_currTileColor = Color.Lerp(m_actualColor, Color.green, 0.5f);
						temp.material.color = m_currTileColor;
						//Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
					}
				}
			}
		
		}

	}

	public void SelectTower(Tower tower)
	{
		m_tower = tower;
	}
}
