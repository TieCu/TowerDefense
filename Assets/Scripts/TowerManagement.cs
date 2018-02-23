using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerManagement : MonoBehaviour {

	private bool isSelling = false;
	private bool isUpgrading = false;

	[SerializeField] GameObject m_tower = null;
	[SerializeField] LayerMask m_placeable;
	[SerializeField] TextMeshProUGUI m_TxtCost = null;
	[SerializeField] GameObject m_costPanel = null;
	private TilePiece m_currentTile = null;
	private TilePiece m_priorTile = null;
	private Color m_currTileColor;
	private Color m_actualColor;

	[Header("Highlighting Colors")]
	[SerializeField] Color m_buyingColor;
	[SerializeField] Color m_sellingColor;
	[SerializeField] Color m_upgradingColor;


	// Use this for initialization
	void Start () {
		m_costPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		


		//Selling code
		if (isSelling)
		{
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 10, m_placeable);
			if (hit.collider != null)
			{

				if (hit.collider.gameObject.GetComponent<TilePiece>().Occupied == true)
				{
					m_costPanel.SetActive(true);
					m_currentTile = hit.collider.gameObject.GetComponent<TilePiece>();
					if (m_priorTile == m_currentTile)
					{

					}
					else
					{
						m_TxtCost.text = "Sell Value: " + ((int)hit.collider.gameObject.GetComponent<TilePiece>().Tower.GetComponent<Tower>().value * .75f).ToString();
						//selection highlighting code
						if (m_priorTile == null) m_priorTile = m_currentTile;
						Renderer temp = hit.transform.gameObject.GetComponent<Renderer>();
						m_actualColor = temp.material.color;
						m_priorTile.gameObject.GetComponent<Renderer>().material.color = m_actualColor;
						m_priorTile = m_currentTile;
						m_currTileColor = Color.Lerp(m_actualColor, m_sellingColor, 0.5f);
						temp.material.color = m_currTileColor;
					}
					//Tower Removal Code
					if (Input.GetMouseButtonDown(0))
					{
						//give money back to the player here from the tower first
						hit.collider.gameObject.GetComponent<TilePiece>().Tower.GetComponent<Tower>().SellTower();

						Destroy(hit.collider.gameObject.GetComponent<TilePiece>().Tower);
						hit.collider.gameObject.GetComponent<TilePiece>().Tower = null;
						m_currentTile.gameObject.GetComponent<Renderer>().material.color = m_actualColor;
						m_costPanel.SetActive(false);

					}


				}

			}
				else m_costPanel.SetActive(false);

		}

		//Buying code
		if (m_tower != null)
		{
			m_costPanel.SetActive(true);
			m_TxtCost.text = "Cost: " + m_tower.GetComponent<Tower>().value.ToString();
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 10, m_placeable);
			if (hit.collider != null)
			{

				if (hit.collider.gameObject.GetComponent<TilePiece>().Occupied == false)
				{
					m_currentTile = hit.collider.gameObject.GetComponent<TilePiece>();
					if (m_priorTile == m_currentTile)
					{

					}
					else
					{
						//selection highlighting code
						if (m_priorTile == null) m_priorTile = m_currentTile;
						Renderer temp = hit.transform.gameObject.GetComponent<Renderer>();
						m_actualColor = temp.material.color;
						m_priorTile.gameObject.GetComponent<Renderer>().material.color = m_actualColor;
						m_priorTile = m_currentTile;
						m_currTileColor = Color.Lerp(m_actualColor, m_buyingColor, 0.5f);
						temp.material.color = m_currTileColor;
					}


					//Tower Placement Code
					if (Input.GetMouseButtonDown(0))
					{
						//Implement Buy code here

						bool iCanBuy = World.Instance.RemoveCoins(m_tower.GetComponent<Tower>().value);

						if (iCanBuy)
						{
							Vector3 pos = hit.collider.transform.position + new Vector3(0.0f, 0.5f, 0.0f);
							GameObject tow = Instantiate(m_tower, pos, Quaternion.identity);

							hit.collider.gameObject.GetComponent<TilePiece>().Tower = tow;
							m_currentTile.gameObject.GetComponent<Renderer>().material.color = m_actualColor;
						}
					}

				}
			}

		}

		//Upgrading code
		if (isUpgrading)
		{
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 10, m_placeable);
			if (hit.collider != null)
			{
				if (hit.collider.gameObject.GetComponent<TilePiece>().Occupied == true)
				{
					m_currentTile = hit.collider.gameObject.GetComponent<TilePiece>();
					if (m_priorTile == m_currentTile)
					{

					}
					else
					{
						//selection highlighting code
						if (m_priorTile == null) m_priorTile = m_currentTile;
						Renderer temp = hit.transform.gameObject.GetComponent<Renderer>();
						m_actualColor = temp.material.color;
						m_priorTile.gameObject.GetComponent<Renderer>().material.color = m_actualColor;
						m_priorTile = m_currentTile;

						//check if this tile is fully upgraded, if it is then revert it back to its normal color
						if(!hit.collider.gameObject.GetComponent<TilePiece>().Tower.GetComponent<Tower>().fullyUpgraded)
						{
							m_costPanel.SetActive(true);
							m_TxtCost.text = "Cost: "; ((int)hit.collider.gameObject.GetComponent<TilePiece>().Tower.GetComponent<Tower>().upgradeCost).ToString();
							m_currTileColor = Color.Lerp(m_actualColor, m_upgradingColor, 0.5f);
							temp.material.color = m_currTileColor;

						}
					}
					//Tower Upgrade Code
					if (Input.GetMouseButtonDown(0))
					{
						//upgrade the tower and subtract the gold from the player here
						hit.collider.gameObject.GetComponent<TilePiece>().Tower.GetComponent<Tower>().UpgradeTower();
						if (hit.collider.gameObject.GetComponent<TilePiece>().Tower.GetComponent<Tower>().fullyUpgraded)
						{
							m_costPanel.SetActive(false);
						}
							//check if this tile is fully upgraded, if it is then revert it back to its normal color
							//m_currentTile.gameObject.GetComponent<Renderer>().material.color = m_actualColor;
						}

				}
			}

		}



	}

	public void UpgradeTower()
	{
		isUpgrading = !isUpgrading;
		isSelling = false;
		m_tower = null;
		if (isUpgrading == false)
		{
			m_costPanel.SetActive(false);
			m_currentTile.gameObject.GetComponent<Renderer>().material.color = m_actualColor;
		}

	}
	public void SellTower()
	{
		isSelling = !isSelling;
		isUpgrading = false;
		m_tower = null;
		if (isSelling == false)
		{
			m_costPanel.SetActive(false);
			m_currentTile.gameObject.GetComponent<Renderer>().material.color = m_actualColor;
		}

	}

	public void SelectTower(GameObject tower)
	{
		//decide where to check if they have enough
		if (m_tower != null)
		{
			m_tower = null;
			m_costPanel.SetActive(false);
			m_currentTile.gameObject.GetComponent<Renderer>().material.color = m_actualColor;
		}
		else
		{
			m_tower = tower;
			isUpgrading = false;
			isSelling = false;
		}
	}
}
