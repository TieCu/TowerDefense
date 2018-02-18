using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T m_instance;

    public static  T Instance
    {
        get
        {
            m_instance = FindObjectOfType<T>();
            if (m_instance == null)
            {
                GameObject go = new GameObject();
                m_instance = go.AddComponent<T>();
                go.name = "Singleton " + typeof(T).ToString();
            }
            return m_instance;
        }
    }
}
