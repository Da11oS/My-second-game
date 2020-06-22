using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PathfindingMoving : MonoBehaviour
{
    // Start is called before the first frame update
    private delegate void Move();

    private List<Vector3> _pathVector;
    private int _vectorIndex;
    private int startX;
    private int startY;
    private int endX;
    private int endY;
    private Pathfinding _pathFinding;
    private List<PathNode> path;
    private Move Walk;
    private Grid<PathNode> _grid;
    private List<PathNode> _path;
    void Start()
    {

        _pathFinding = GameObject.Find("PathfindGrid").GetComponent<Testing>().Pathfinding;
        _pathFinding.GetGrid().GetXY(transform.position, out startX, out startY);
        _grid = _pathFinding.GetGrid();
        _pathFinding.GetGrid().GetXY(GetComponent<EnemyMove>().Player.transform.position, out endX, out endY);
        _vectorIndex = 0;
        _path = _pathFinding.FindPath((int)startX, (int)startY, (int)endX, (int)endY);
        SetVector(_path);
        LookAt();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_vectorIndex);
        if(_pathVector!=null && _vectorIndex<_path.Count)
        if (IsReachedUnit(_pathVector[_vectorIndex]))
            LookAt();
       

    }

    private void SetVector(List<PathNode> path)
    {
        if (path != null)
        {
            Debug.Log("path.Count " + path.Count);
            for (int i = 0; i < path.Count - 1; i++)
            {
                Debug.Log(new Vector3(path[i + 1].x, path[i + 1].y) + " ind =" + i);
                _pathVector[i] = new Vector3(path[i + 1].x, path[i + 1].y) + GameObject.Find("PathfindGrid").transform.position;
            }
        }
    }
    private void LookAt()
    {
        _vectorIndex++;
        float angle = Mathf.Atan2(_pathVector[_vectorIndex].y, _pathVector[_vectorIndex].y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, gameObject.GetComponent<EnemyMove>().TurnSpeed * Time.deltaTime); 
    }
    private bool IsReachedUnit(Vector3 vector)
    {
        _pathFinding.GetGrid().GetXY(transform.position, out int enemyX, out int enemyY);
        _pathFinding.GetGrid().GetXY(vector, out int gridX, out int gridY);
        return (enemyX == gridX) && (enemyY == gridY);
    }
    public void DrawWay(List<PathNode> path)
    {
        if (path != null)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {

                Debug.DrawLine(new Vector3(path[i].x, path[i].y) * _grid.GetCellSize() + transform.position + Vector3.one * 5f / 2, new Vector3(path[i + 1].x, path[i + 1].y) * _grid.GetCellSize() + transform.position + Vector3.one * 5f / 2, Color.green, 5f);
            }
        }
    }
}

