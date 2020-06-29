using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropingGun : MonoBehaviour
{
    private void Update()
    {
        if ( GetComponent<Rigidbody2D>().velocity.x < 0.5f && GetComponent<Rigidbody2D>().velocity.y < 0.5f)
        {
            GetComponent<Rigidbody2D>().velocity *= 0;
            GetComponent<CircleCollider2D>().isTrigger = true;
        }
    }

}
