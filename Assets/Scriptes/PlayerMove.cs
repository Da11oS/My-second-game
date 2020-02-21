using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    private Joystick moveJoystick,turnJoystick;
    private GameObject playerLegs;
    private Rigidbody2D RB;
    private float angle;

    private void Awake()
    {
        moveJoystick = GameObject.Find("Fixed Joystick(Move)").GetComponent<Joystick>();
        turnJoystick = GameObject.Find("Fixed Joystick(Turn)").GetComponent<Joystick>();
        playerLegs = GameObject.Find("Legs");
    }
    void Start()
    {
    
        RB = playerLegs.GetComponent<Rigidbody2D>();
        angle = Mathf.Atan2(turnJoystick.Horizontal, turnJoystick.Vertical) * Mathf.Rad2Deg;
      
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RB.velocity = Run();
        transform.rotation = Turn();
    }
    private Vector2 Run()
    {
        //return new Vector2(-moveJoystick.Horizontal * 5, -moveJoystick.Vertical * 5 );
        return new Vector2(Input.GetAxis("Horizontal")* 5, Input.GetAxis("Vertical") * 5);
    }
    private Quaternion Turn()
    {
        if (turnJoystick.Horizontal != 0 && turnJoystick.Vertical != 0 )
        {
            angle = Mathf.Atan2(turnJoystick.Horizontal, -turnJoystick.Vertical) * Mathf.Rad2Deg;
        }
        Quaternion rotation = Quaternion.AngleAxis(angle , Vector3.forward);
        return Quaternion.Slerp(transform.rotation, rotation, 5 * Time.deltaTime);
    }
}
