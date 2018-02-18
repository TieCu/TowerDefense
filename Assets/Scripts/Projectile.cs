using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] [Range(.1f, 50.0f)] float m_speed = 1.0f;
    GameObject m_target;
        
    void Update()
    {
        Vector3 direction = m_target.transform.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction, Vector3.up), Time.deltaTime);

        Vector3 velocity = transform.rotation * (Vector3.forward * m_speed);
        transform.position = transform.position + (velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(GameObject newTarget)
    {
        m_target = newTarget;
    }
}
