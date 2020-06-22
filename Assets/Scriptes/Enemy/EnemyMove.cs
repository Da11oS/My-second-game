using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EnemyMove : MonoBehaviour
{

    public float WayRayCastDistance;
    public float Speed;
    public float TurnSpeed;
    public float ToPlyerDistanceLimite;
    public GameObject Player;
    public GameObject FinishPoint;

    private RaycastHit2D _obstacleCheckRayCast;
    private RaycastHit2D _playerChekRayCast;
    private Quaternion _turn;
    private Vector2 _toPlayerDirection;
    private Rigidbody2D _rigidBody;
    private int _layerMask;
    private bool _rayForCheckObstateOnCollider;
    private float _toPlyerDistance;
    private bool _isPatrol;

    //Поиск пути к игроку
    private Pathfinding _pathFinding;
    [SerializeField]
    private List<Vector3> _pathVector;
    private List<PathNode> _path;
    private bool _isLastFrameObstateBetweenPlayer;
    private bool _isLastFrameSeePlayer;
    [SerializeField]
    private int _indexOfVector;
    void Start()
    {
        _layerMask = ~(1 << LayerMask.NameToLayer("Enemy"));
        _rigidBody = GetComponent<Rigidbody2D>();
        _turn = transform.rotation;
        _rayForCheckObstateOnCollider = false;
        Player = GameObject.Find("Bond");
        _pathFinding = FindObjectOfType<Testing>().Pathfinding;
        _isLastFrameObstateBetweenPlayer = false;
        _isLastFrameSeePlayer = false;

    }

    void Update()
    {

        if (IsSeePlayer())
        {
            _isPatrol = false;
            //if (IsObstacleBetweenPlayer())
            //{
            //    if (!_isLastFrameObstateBetweenPlayer)
            //    {
                  //  Debug.Log(transform.name + " see player!");
                    //_pathFinding.GetGrid().GetXY(transform.position, out int startX, out int startY);
                    //_pathFinding.GetGrid().GetXY(Player.transform.position, out int endX, out int endY);
                    //_path = _pathFinding.FindPath(startX, startY, endX, endY);
                    SetTargetPosition(Player.transform.position);

            //    }

            //}
            //else
            //{
            //    GetTurn(Player.transform.position);
            //    Debug.Log("Obstacle Null " + transform.name);
            //    Walk();
            //}
        }
        else
        {
              //  Debug.Log("Don't see player " + transform.name);
            _pathVector = null;
            _isPatrol = true;
        }
        if (_isPatrol)
        {
            ObstacleCheck();
            Patrol();
        }
        MoveToPlayer();
        _isLastFrameObstateBetweenPlayer = IsObstacleBetweenPlayer();
        _isLastFrameSeePlayer = IsSeePlayer();
    }
    public void Walk()
    {
        Ray2D ray = new Ray2D(transform.position,transform.up);
        _rigidBody.velocity = ray.direction * Speed;
    }
    private void ObstacleCheck()
    {
        float turn = -0.3f;
        Ray2D ray = new Ray2D(transform.position, transform.up + new Vector3(turn + Mathf.PingPong(Time.time*TurnSpeed, 1f), turn + Mathf.PingPong(Time.time * TurnSpeed, 1f), 0)); 
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

    private Quaternion GetTurn(Vector3 target)
    {
        var angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        _turn = Quaternion.AngleAxis(angle, Vector3.forward);
        return Quaternion.Slerp(transform.rotation, _turn, TurnSpeed * Time.deltaTime);
    }
    private void PlayerRayCast()
    {
       
        var heading = Player.transform.position -  transform.position;
        _toPlyerDistance = heading.magnitude;
        _toPlayerDirection = heading / heading.magnitude;
        _playerChekRayCast = Physics2D.Raycast(transform.position, Player.transform.position, _toPlyerDistance, _layerMask);
        Debug.DrawRay(transform.position, _toPlyerDistance* _toPlayerDirection, Color.yellow);
        Debug.DrawRay(transform.position, ToPlyerDistanceLimite * _toPlayerDirection, Color.red);
    }

    private bool IsSeePlayer()
    {
        PlayerRayCast();
        return (_playerChekRayCast.collider == null ||
            _playerChekRayCast.collider.gameObject.layer == LayerMask.NameToLayer("Glass"))
            && _toPlyerDistance < ToPlyerDistanceLimite;
    }
    private bool IsObstacleBetweenPlayer()
    {
        return _playerChekRayCast.collider != null && !_playerChekRayCast.collider.GetComponent<PlayerControl>();
    }
   private void MoveToPlayer()
    {
        if (_pathVector != null)
        {
            Vector3 targetPosition = _pathVector[_indexOfVector];
               if(Vector3.Distance(targetPosition, transform.position) > 1f)
                {
                    Vector3 moveDirection = (targetPosition - transform.position).normalized;
                    GetTurn(targetPosition);
                   // Walk();
                    transform.position = transform.position + moveDirection * Speed * Time.deltaTime;
                    _rigidBody.velocity = Vector2.zero;
                  //  Debug.Log(targetPosition + "targetPosition");
                  //  Debug.Log(transform.name + "move to player!");
                }
               else
                {
                    _indexOfVector++;
                    if (_indexOfVector >= _pathVector.Count)
                    {
                        _pathVector = null;
                    }
                }
 
        }
    }
    public void SetTargetPosition(Vector3 targetPosition)
    {
        _indexOfVector = 0;
        _pathVector =Pathfinding.Instance.FindPath(transform.position, targetPosition);
       // Instantiate(FinishPoint, _pathVector[_pathVector.Count - 1], Quaternion.identity);
        if (_pathVector != null && _pathVector.Count > 1)
        {
        Testing.Instance.GetComponent<Testing>().DrawWay(_pathVector);
            _pathVector.RemoveAt(0);
        }
    }

}
