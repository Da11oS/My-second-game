using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject DeadEnemy;
    private Vector3 _touchPoint;
    private bool _isDead = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            GetDamage(collision.gameObject, collision.transform.position);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            GetDamage(collision.gameObject, collision.transform.position);
        }
    }
    private void GetDamage(GameObject collider, Vector3 position)
    {
        _touchPoint = position;
        if (!_isDead)
            Instantiate(DeadEnemy, transform.position, GetRotation());
        if(collider.tag == "Bullet")
        {
            Destroy(collider);
        }
        Destroy(gameObject);
        _isDead = true;
    }
    private Quaternion GetRotation()
    {
        Vector3 heading = transform.position - _touchPoint;
        Vector3 direction = heading / heading.magnitude;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }
    
}

