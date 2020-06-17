using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    private Rigidbody2D _physic;

    private float _forceValume = 15;
    private Vector2 _direction;

    private void Start()
    {

        if(GetComponent<Rigidbody2D>()!=null)
        _physic = GetComponent<Rigidbody2D>();
        _physic.AddForce(_forceValume * transform.up, ForceMode2D.Impulse);
        
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
