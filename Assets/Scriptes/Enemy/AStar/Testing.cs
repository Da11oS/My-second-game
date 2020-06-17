using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public int xScale;
    public int yScale;
    public float CellSize;
    public int FontSize;

    private int startX;
    private int startY;
    private int endX;
    private int endY;
    public Pathfinding Pathfinding;
    List<PathNode> path;
    void Start()
    {
        //Grid grid = new Grid(20, 10, 5f, transform.position);
         Pathfinding = new Pathfinding(xScale, yScale, CellSize,transform.position);
         Pathfinding.grid.SetTextMeshFontSize(FontSize);
         startX = 0;
         startY = 0;
         endX = 0;
         endY = 0;
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            path[0].grid.TextMeshArray[startX, startY].color = Color.white;
            Pathfinding.GetGrid().GetXY(mousePosition, out startX, out startY);

        }
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            path[0].grid.TextMeshArray[endX, endY].color = Color.white;
            Pathfinding.GetGrid().GetXY(mousePosition, out endX, out endY);
        }
        if (Input.GetMouseButtonDown(2))
        {
            SetIsUnwalkable();
        }
        path = Pathfinding.FindPath((int)startX, (int)startY, (int)endX , (int)endY);
            DrawWay(path);
            path[0].grid.TextMeshArray[startX, startY].color = Color.green;
            path[0].grid.TextMeshArray[endX, endY].color = Color.red;

    }

    private void SetIsUnwalkable()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Pathfinding.GetGrid().GetXY(mousePosition, out int x, out int y);
        Pathfinding.GetNode(x, y).SetIsWalkable(false);
       path[0].grid.TextMeshArray[x, y].color = Color.black;
    }
    private void DrawWay(List<PathNode> path)
    {
        if (path != null)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {
                
                Debug.DrawLine(new Vector3(path[i].x, path[i].y) * CellSize + transform.position + Vector3.one * 5f /2, new Vector3(path[i + 1].x, path[i + 1].y) * CellSize + transform.position + Vector3.one * 5f / 2, Color.green, 5f);
            }
        }
    }
}
