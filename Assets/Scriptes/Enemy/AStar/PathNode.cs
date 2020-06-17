using System;
using System.Reflection;

public class PathNode 
{
    public int GCost;
    public int HCost;
    public int FCost;

    public PathNode CameFromeNode;

    public int x;
    public int y;
    public Grid<PathNode> grid;
    public bool isWalkable;
    public PathNode(Grid<PathNode> grid, int x, int y)
    {
        this.x = x;
        this.y = y;
        this.grid = grid;
        isWalkable = true;
    }

    public void SetIsWalkable(bool isWalkable)
    {
        this.isWalkable = isWalkable;
       //grid.TriggerGridObjectChanged(x, y);
    }

    public int GetFCost()
    {     
        return GCost + HCost;
    }
    public void CalculateFCost()
    {
       FCost = GCost + HCost;
    }
    public int GetX()
    {
        return x;
    }
    public int GetY()
    {
        return y;
    }
    public override string ToString()
    {
        return x + "," + y;
    }
}
