using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropingGun : MonoBehaviour
{
    public int BulletCount;
    public int TakeGunCount = 0;
    private Vector3 _position;
    private void Start()
    {
       
        _position = transform.position;
        Debug.Log("Droppp");
    }
    private void Update()
    {
        if ( GetComponent<Rigidbody2D>().velocity.x < 0.5f && GetComponent<Rigidbody2D>().velocity.y < 0.5f)

        {
            GetComponent<Rigidbody2D>().velocity *= 0;
            GetComponent<CircleCollider2D>().isTrigger = true;
        }
        _position = transform.position;
    }

}
