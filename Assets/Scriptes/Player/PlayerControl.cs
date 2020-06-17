using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float Speed;
    public float TurnSpeed;
    // Start is called before the first frame update
    private Joystick _moveJoystick;
    private Joystick _turnJoystick;
    private GameObject _playerLegs;
    private Rigidbody2D _playerPhisyc;
    private float _angle;

    private void Awake()
    {
        _moveJoystick = GameObject.Find("Fixed Joystick(Move)").GetComponent<Joystick>();
        _turnJoystick = GameObject.Find("Fixed Joystick(Turn)").GetComponent<Joystick>();
        _playerLegs = GameObject.Find("Legs");
    }
    void Start()
    {
    
        _playerPhisyc = _playerLegs.GetComponent<Rigidbody2D>();
        _angle = Mathf.Atan2(_turnJoystick.Horizontal, _turnJoystick.Vertical) * Mathf.Rad2Deg;
      
    }

    private void Update()
    {
        _playerPhisyc.velocity = GetVelocity();
        transform.rotation = GetTurn();
    }
    private Vector2 GetVelocity()
    {
       // return new Vector2(-_moveJoystick.Horizontal * 5, -_moveJoystick.Vertical * 5 );
        return new Vector2(Input.GetAxis("Horizontal")* Speed, Input.GetAxis("Vertical") * Speed);
    }
    private Quaternion GetTurn()
    {
        if (_turnJoystick.Horizontal != 0 && _turnJoystick.Vertical != 0)
        {
            _angle = Mathf.Atan2(_turnJoystick.Horizontal, -_turnJoystick.Vertical) * Mathf.Rad2Deg;
        }

        // _angle = Mathf.Atan2(_playerLegs.transform.position.x - Input.mousePosition.x,  _playerLegs.transform.position.y - Input.mousePosition.y ) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
        return Quaternion.Slerp(transform.rotation, rotation, TurnSpeed * Time.deltaTime);
    }
}
