using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Pathfinding
{
    private const int MOVE_STRAIGHT = 10;
    private const int MOVE_DIAGONAL = 14;
    public  Grid<PathNode> grid;

    private List<PathNode> openList;
    private List<PathNode> closedList;
    public Pathfinding(int width, int height, float CellSize, Vector3 originPosition)
    {
        grid = new Grid<PathNode>(width, height, CellSize, originPosition, (Grid<PathNode> g, int x, int y) => new PathNode (g, x, y));
    }
    public List<Vector3> FindPath(Vector3 startWorldPosition, Vector3 endWorldPosition)
    {

        grid.GetXY(startWorldPosition, out int startX, out int startY);
        grid.GetXY(endWorldPosition, out int endX, out int endY);

        List<PathNode> path = FindPath(startX, startY, endX, endY);
        if (path == null)
        {
            return null;
        }
        else
        {
            List<Vector3> vectorPath = new List<Vector3>();
            foreach (PathNode pathNode in path)
            {
                vectorPath.Add((new Vector3(pathNode.x, pathNode.y)) * grid.GetCellSize() + Vector3.one * grid.GetCellSize() * .5f);
            }
            return vectorPath;
        }
    }
    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {

        PathNode startNode = grid.GetGridObjet(startX, startY);
        PathNode endNode = grid.GetGridObjet(endX, endY);

        if (isInvalidePath(startNode,endNode))
        {
            // Invalid Path
            return null;
        }

        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                PathNode pathNode = grid.GetGridObjet(x, y);
                pathNode.GCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.CameFromeNode = null;
            }
        }

        startNode.GCost = 0;
        startNode.HCost = CalculateDistance(startNode, endNode);
        startNode.CalculateFCost();


        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);
            if (currentNode == endNode)
            {
               // Reached finalnode
                //PathfindingDebugStepVisual.Instance.TakeSnapshot(grid, currentNode, openList, closedList);
                //PathfindingDebugStepVisual.Instance.TakeSnapshotFinalPath(grid, CalculatePath(endNode));
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathNode neighbourNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neighbourNode)) continue;
                if (!neighbourNode.isWalkable)
                {
                    closedList.Add(neighbourNode);
                    continue;
                }

                int tentativeGCost = currentNode.GCost + CalculateDistance(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.GCost)
                {
                    neighbourNode.CameFromeNode = currentNode;
                    neighbourNode.GCost = tentativeGCost;
                    neighbourNode.HCost = CalculateDistance(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }

        return null;
    }
    private bool isInvalidePath(PathNode start, PathNode end)
    {
        return start == null || end == null;
    }
    private void CheckOnNull(PathNode node, string name)
    {
        if (node == null)
            Debug.Log(name + " null");
    }
   
    public Grid<PathNode > GetGrid()
    {
        return grid;
    }
 
    private List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();
        bool isLeft = currentNode.x-1 >= 0;
        bool isRight = currentNode.x + 1 < grid.GetWidth();
        bool isDown = currentNode.y - 1 >= 0;
        bool isUp = currentNode.y + 1 < grid.GetHeight();

        if (isLeft)
        {
            // Left
            neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));
            // Left Down
            if (isDown) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y - 1));
            // Left Up
            if (isUp) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y + 1));
        }
        if (isRight)
        {
            // Right
            neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));
            // Right Down
            if (isDown) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
            // Right Up
            if (isUp) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
        }
        // Down
        if (isDown) neighbourList.Add(GetNode(currentNode.x, currentNode.y - 1));
        // Up
        if (isUp) neighbourList.Add(GetNode(currentNode.x, currentNode.y + 1));

        return neighbourList;
    }

    private void NeighbourListAdd( List<PathNode> neighbourList, PathNode currentNode, int x)
    {
        bool isDown = currentNode.y - 1 >= 0;
        bool isUp = currentNode.y < grid.GetHeight();
        if (isDown)
             neighbourList.Add(GetNode(currentNode.x + x, currentNode.y - 1));
        if (isUp)
             neighbourList.Add(GetNode(currentNode.x + x, currentNode.y + 1));
    }
    public PathNode GetNode(int x, int y)
    {
        return grid.GetGridObjet(x, y);
    }
    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;
        while (currentNode.CameFromeNode != null)
        {
            path.Add(currentNode.CameFromeNode);
            currentNode = currentNode.CameFromeNode;
        }
        path.Reverse();
        return path;
    }


    private int CalculateDistance(PathNode a, PathNode b)
    {
        int yDistance = (int)Mathf.Abs(a.x - b.x);
        int xDistance = (int)Mathf.Abs(a.x - b.x);
        int remaining = (int)Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT * remaining;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostNode = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].FCost < lowestFCostNode.FCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }
}

