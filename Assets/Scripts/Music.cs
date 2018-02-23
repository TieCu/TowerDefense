using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

	[SerializeField] AudioClip m_regularMusic;
	[SerializeField] AudioClip m_bossBattleMusic;
	private AudioSource m_source;
	// Use this for initialization
	void Start () {
		m_source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void CueBossBattle()
	{
		m_source.clip = m_bossBattleMusic;
		m_source.Play();
	}
	public void EndBossBattle()
	{
		m_source.clip = m_regularMusic;
		m_source.Play();
	}
}
