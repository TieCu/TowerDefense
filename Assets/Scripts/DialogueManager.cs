using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
	[SerializeField] GameObject m_dialogBox;
	[SerializeField] Button m_skip;
	[SerializeField] Image m_playerPlacement;
	[SerializeField] Image m_npcPlacement;
	[SerializeField] Image m_optionalPlacement;
	[SerializeField] Text m_namePlacement;
	[SerializeField] Text m_dialogPlacement;
	[SerializeField] Sprite[] m_playerSprites;
	[SerializeField] Sprite[] m_npcSprites;
	[SerializeField] Sprite[] m_optionalSprites;
	[SerializeField] string[] m_names;
	[SerializeField] string[] m_dialogs;
	int m_currentLine = 0;
	bool m_talk = false;

	void Start()
	{
	}

	void Update()
	{
		if (m_talk && Input.GetButtonDown("Talk"))
		{
			m_dialogBox.SetActive(true);
			Time.timeScale = 0.0f;
		}

		if (m_dialogBox.activeInHierarchy && Input.GetButtonDown("Talk"))
		{
			m_currentLine++;
		}

		if (m_currentLine >= m_dialogs.Length)
		{
			Time.timeScale = 1.0f;
			m_dialogBox.SetActive(false);
			m_currentLine = 0;
		}

		m_playerPlacement.sprite = m_playerSprites[m_currentLine];
		if (m_npcPlacement != null)
		{
			m_npcPlacement.sprite = m_npcSprites[m_currentLine];
		}
		if (m_npcPlacement != null)
		{
			m_optionalPlacement.sprite = m_optionalSprites[m_currentLine];
		}
		m_namePlacement.text = m_names[m_currentLine];
		m_dialogPlacement.text = m_dialogs[m_currentLine];

		m_skip.onClick.AddListener(SkipDialog);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		m_talk = true;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		m_talk = false;
	}

	void SkipDialog()
	{
		Time.timeScale = 1.0f;
		m_dialogBox.SetActive(false);
		m_currentLine = 0;
	}
}
