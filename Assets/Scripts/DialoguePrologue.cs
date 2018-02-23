using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePrologue : MonoBehaviour
{
	[SerializeField] GameObject m_dialogBox;
	[SerializeField] GameObject m_nextDialog;
	[SerializeField] GameObject m_menu;
	[SerializeField] Image m_portrait;
	[SerializeField] Text m_namePlacement;
	[SerializeField] Text m_dialogPlacement;
	[SerializeField] Sprite[] m_characterSprites;
	[SerializeField] string[] m_names;
	[SerializeField] string[] m_dialogs;
	int m_currentLine = 0;
	bool m_talk = false;

	void Start()
	{
	}

	void Update()
	{
		if (m_dialogBox.activeInHierarchy)
		{
			m_menu.SetActive(false);
			Time.timeScale = 0.0f;
		}

		if (m_talk && Input.GetMouseButtonDown(0))
		{
			m_dialogBox.SetActive(true);
		}

		if (m_dialogBox.activeInHierarchy && Input.GetMouseButtonDown(0))
		{
			m_currentLine++;
		}

		if (m_currentLine >= m_dialogs.Length)
		{
			Time.timeScale = 1.0f;
			m_menu.SetActive(true);
			m_nextDialog.SetActive(true);
			m_dialogBox.SetActive(false);
			m_currentLine = 0;
		}

		m_portrait.sprite = m_characterSprites[m_currentLine];
		m_namePlacement.text = m_names[m_currentLine];
		m_dialogPlacement.text = m_dialogs[m_currentLine];
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		m_talk = true;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		m_talk = false;
	}
}
