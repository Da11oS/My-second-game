using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    public float WayRayCastDistance;
    public float Speed;
    public float TurnSpeed;
    public  float ToPlyerDistanceLimite;
    public GameObject Player;

    private RaycastHit2D _obstacleCheckRayCast;
    private RaycastHit2D _playerChekRayCast;
    private Quaternion _turn;
    private Vector2 _toPlayerDirection;
    private Rigidbody2D _rigidBody;
    private int _layerMask;
    private bool _rayForCheckObstateOnCollider;
    private float _toPlyerDistance;
    private bool _isPatrol;
    void Start()
    {
        _layerMask = ~(1 << LayerMask.NameToLayer("Enemy"));
        _rigidBody = GetComponent<Rigidbody2D>();
        _turn = transform.rotation;
        _rayForCheckObstateOnCollider = false;
        Player = GameObject.Find("Bond");
    }

    void Update()
    {
        PlayerRayCast();
        if (IsSeePlayer())
        {
            ObstacleCheck();
            GetComponent<PathfindingMoving>().enabled = true;
            _isPatrol = false;
        }
        else if (_isPatrol)
        {
            ObstacleCheck();
            Patrol();
        }
        if (!IsSeePlayer())
        {
            GetComponent<PathfindingMoving>().enabled = false;
            _isPatrol = true;
        }
    }
    public void Walk()
    {
        Ray2D ray = new Ray2D(transform.position, transform.up);
        _rigidBody.velocity = ray.direction * Speed  ;
    }
    private void ObstacleCheck()
    {
        Ray2D ray = new Ray2D(transform.position, transform.up + new Vector3(-0.3f+Mathf.PingPong(Time.time*TurnSpeed, 1f), -0.3f + Mathf.PingPong(Time.time * TurnSpeed, 1f), 0)); 
        _obstacleCheckRayCast = Physics2D.Raycast(ray.origin, ray.direction, WayRayCastDistance, _layerMask);
        Debug.DrawRay(transform.position, ray.direction* WayRayCastDistance, Color.blue);

    }
    private void Patrol()
    {
        
        if (IsObstate())
        {
            _rayForCheckObstateOnCollider = true;
                transform.Rotate(0, 0, 90);
                //Debug.Log(_obstacleCheckRayCast.transform.name + "Луч задет!(Opstate)");
        }
        else
            _rayForCheckObstateOnCollider = false;
        Walk();
    }

    private bool IsObstate()
    {
        return (_obstacleCheckRayCast.collider != null && !_rayForCheckObstateOnCollider) &&
                (IsNeedLayer(_obstacleCheckRayCast, "Opstate") ||
                 IsNeedLayer(_obstacleCheckRayCast, "Door")  ||
                 IsNeedLayer(_obstacleCheckRayCast, "Glass")
                );
    }
    private bool IsNeedLayer(RaycastHit2D raycast, string layerName)
    {
        return raycast.collider.gameObject.layer == LayerMask.NameToLayer(layerName);
    }
    private Quaternion GetTurn(float angle)
    {
        _turn = Quaternion.AngleAxis(angle, Vector3.forward);
        return Quaternion.Slerp(transform.rotation, _turn, TurnSpeed * Time.deltaTime);
    }

    private void PlayerRayCast()
    {
       
        var heading = Player.transform.position -  transform.position;
        _toPlyerDistance = heading.magnitude;
        _toPlayerDirection = heading / heading.magnitude;
        _playerChekRayCast = Physics2D.Raycast(transform.position, Player.transform.position,
        _toPlyerDistance, _layerMask);
        Debug.DrawRay(transform.position, _toPlyerDistance* _toPlayerDirection, Color.yellow);
        Debug.DrawRay(transform.position, ToPlyerDistanceLimite * _toPlayerDirection, Color.red);
    }
    private void FollowThePlayer()
    {
        float angle = Mathf.Atan2(_toPlayerDirection.y, _toPlayerDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, TurnSpeed*Time.deltaTime);
    }
    private bool IsSeePlayer()
    {
        return (_playerChekRayCast.collider == null ||
            _playerChekRayCast.collider.gameObject.layer == LayerMask.NameToLayer("Glass"))
            && _toPlyerDistance < ToPlyerDistanceLimite;
    }
    private void SearchWayToPlayer()
    {
        if(IsObstate())
        {

        }

    }
}
