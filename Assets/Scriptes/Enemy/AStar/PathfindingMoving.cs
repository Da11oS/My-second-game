using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PathfindingMoving : MonoBehaviour
{
    // Start is called before the first frame update

    private List<Vector3> _pathVector;
    private int _vectorIndex;
    private int startX;
    private int startY;
    private int endX;
    private int endY;
    private Pathfinding pathFinding;
    private List<PathNode> path;
    private delegate void Move();
    private Move Walk;
    void Start()
    {
       
        pathFinding = GameObject.Find("Grid").GetComponent<Testing>().Pathfinding;
        pathFinding.GetGrid().GetXY(transform.position, out startX, out startY);
        pathFinding.GetGrid().GetXY(GetComponent<EnemyMove>().Player.transform.position, out endX, out endY);
        _vectorIndex = 0;
        LookAt(ref _vectorIndex);
        Walk = gameObject.GetComponent<EnemyMove>().Walk; 
    }

    // Update is called once per frame
    void Update()
    {
        if (IsReachedUnit(_pathVector[_vectorIndex]))
            LookAt(ref _vectorIndex);
        Walk();
    }

    private void SetVector(List<PathNode> path)
    {
        if (path != null)
            for (int i = 0; i < path.Count - 1; i++)
                _pathVector[i] = new Vector3(path[i + 1].x, path[i + 1].y) + GameObject.Find("Grid").transform.position;

    }
    private void LookAt( ref int i)
    {
        i++;
        float angle = Mathf.Atan2(_pathVector[i].y, _pathVector[i].y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, gameObject.GetComponent<EnemyMove>().TurnSpeed * Time.deltaTime); 
    }
    private bool IsReachedUnit(Vector3 vector)
    {
        return (Vector3.Distance(transform.position, vector) < 0.05f);
    }
}

