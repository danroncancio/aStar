using System;
using System.Collections.Generic;

namespace aStar
{
    class Pathfinding
    {
        public void FindPath(Node startNode, Node finishNode, int gridX, int gridY, Node[,] grid)
        {
            Grid gr = new Grid();
            List<Node> openSet = new List<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet[0];
                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                    {
                        currentNode = openSet[i];
                    }
                }

                openSet.Remove(currentNode);
                currentNode.visited = true;

                if (currentNode == finishNode)
                {
                    RetracePath(startNode, finishNode);
                    return;
                }

                foreach (Node neighbour in gr.GetNeighbours(currentNode, gridX, gridY, grid))
                {
                    if (!neighbour.walkable || neighbour.visited)
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, finishNode);
                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                    }
                }
            }

            void RetracePath(Node startNode, Node finishNode)
            {
                List<Node> path = new List<Node>();
                Node currentNode = finishNode;

                while (currentNode != startNode)
                {
                    //path.Add(currentNode);
                    currentNode.isPath = true;
                    currentNode = currentNode.parent;

                }
                //path.Reverse();
            }
        }

        int GetDistance(Node nodeA, Node nodeB)
        {
            int dstX = (int)MathF.Abs(nodeA.gridX - nodeB.gridX);
            int dstY = (int)MathF.Abs(nodeA.gridY - nodeB.gridY);

            if (dstX > dstY)
            {
                return 14 * dstY + 10 * (dstX - dstY);
            }
            else
            {
                return 14 * dstX + 10 * (dstY - dstX);
            }
        }
    }
}
