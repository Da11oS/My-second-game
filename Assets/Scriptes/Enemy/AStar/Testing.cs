using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public int xScale;
    public int yScale;
    public int FontSize;
    public float CellSize;
    public static GameObject Instance;
    public Pathfinding Pathfinding;
    public GameObject Obstacle;


    private void Awake()
    {
        
         Pathfinding = new Pathfinding(xScale, yScale, CellSize, transform.position);
        Instance = this.gameObject;
    }
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            SetIsUnwalkable(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        
    }

    public void SetIsUnwalkable(Vector3 position)
    {
        Pathfinding.GetGrid().GetXY(position, out int x, out int y);
        Pathfinding.GetNode(x, y).SetIsWalkable(false);
        Debug.Log(x + ", " + y + "black");
        Pathfinding.Grid.TextMeshArray[x, y].color = Color.black;
    }
    public  void DrawWay(List<PathNode> path)
    {
        if (path != null)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {

                Debug.DrawLine(new Vector3(path[i].x, path[i].y) * CellSize  + Vector3.one * 5f / 2, new Vector3(path[i + 1].x, path[i + 1].y) * CellSize  + Vector3.one * 5f / 2, Color.green, 5f);
            }
        }
    }
    public void DrawWay(List<Vector3> path)
    {
        if (path != null)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {

                Debug.DrawLine(path[i] * CellSize - transform.position + Vector3.one * 5f / 2, path[i+1] * CellSize - transform.position + Vector3.one * 5f / 2, Color.green, 5f);
            }
        }
    }
}
