using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] [Range(.1f, 50.0f)] float m_speed = 1.0f;
   
    GameObject m_targetObject;
    Vector3 m_targetVec;
    Status m_status;
    float m_damage;
    float m_maxDistance = 5.0f;
    string m_enemyTag = "Enemy";
        
    void Update()
    {
        //if (!m_targetObject)
        //{
        //    m_targetObject = World.Instance.GetNearestGameObject(gameObject, m_enemyTag, m_maxDistance);
        //    m_targetVec = m_targetObject.transform.position;
        //}
        if (m_targetObject)
        {
            m_targetVec = m_targetObject.transform.position;
        }
        else
        {
            m_targetObject = World.Instance.GetNearestGameObject(gameObject, m_enemyTag, m_maxDistance);
            m_targetVec = m_targetObject.transform.position;
        }

        Vector3 direction = m_targetVec - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction, Vector3.up), 10.0f * Time.deltaTime);

        Vector3 velocity = transform.rotation * (Vector3.forward * m_speed);
        transform.position = transform.position + (velocity * Time.deltaTime);

        if (direction.magnitude <= .15f)
        {
            if (m_targetObject)
            {
                AI ai = m_targetObject.GetComponent<AI>();
                ai.Attacked(m_damage);

                if (m_targetObject)
                {
                    ai.StatusChanged((int)m_status.status, m_status.statusDamage, m_status.time, true);
                }
            }
            
            Destroy(gameObject);
        }
    }

    public void SetTarget(GameObject newTarget)
    {
        m_targetObject = newTarget;
    }

    public void SetDamage_Status(float damage, Status status)
    {
        m_damage = damage;
        m_status = status;
    }
}
