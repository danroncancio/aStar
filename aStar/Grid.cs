using System;
using System.Numerics;
using System.Collections.Generic;
using static Raylib_cs.Raylib;

namespace aStar
{
    public class Grid
    {
        public Vector2 gridWorldSize = new Vector2(500, 500);
        public float nodeRadius = 20f;
        Node[,] grid;

        public Node nStart;
        public Node nFinish;

        float nodeDiameter;
        int gridSizeX, gridSizeY;

        Vector2 vRight = new Vector2(1, 0);
        Vector2 vForwa = new Vector2(0, 1);
        Vector2 vCenter = new Vector2(GetScreenHeight() / 2, GetScreenWidth() / 2);


        public Node[,] Start(int scrHeight, int scrWidth)
        {
            nodeDiameter = nodeRadius*2;
            gridSizeX = (int)gridWorldSize.X / (int)nodeDiameter;
            gridSizeY = (int)gridWorldSize.Y / (int)nodeDiameter;
            Node[,] gridboi = CreateGrid(scrHeight, scrWidth);
            return gridboi;
        }

        Node[,] CreateGrid(int scrHeight, int scrWidth)
        {
            grid = new Node[gridSizeX, gridSizeY];
            Vector2 worldBottomLeft = vCenter - vRight * gridWorldSize.X/2 - vForwa * gridWorldSize.Y/2;

            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    Vector2 worldPoint = worldBottomLeft + vRight * (x * nodeDiameter + nodeRadius) + vForwa * (y * nodeDiameter + nodeRadius);
                    grid[x, y] = new Node(true, worldPoint, false, x, y);
                }
            }

            nStart = grid[2, 1];
            nFinish = grid[9, 10];

            return grid;
        }

        public List<Node> GetNeighbours(Node node, int gridSizeX, int gridSizeY, Node[,] grid)
        {
            List<Node> neighbours = new List<Node>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                    {
                        continue;
                    }

                    int checkX = node.gridX + x;
                    int checkY = node.gridY + y;

                    if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                    {
                        neighbours.Add(grid[checkX, checkY]);
                    }
                }
            }

            return neighbours;
        }

        public Node GetNodePos(Vector2 pos)
        {
            float percentX = ((pos.X - 200)) / gridWorldSize.X;
            float percentY = ((pos.Y - 200)) / gridWorldSize.Y;

            if (percentX < 0)
            {
                percentX = 0;
            }
            else if (percentX > 1)
            {
                percentX = 1;
            }

            if (percentY < 0)
            {
                percentY = 0;
            }
            else if (percentY > 1)
            {
                percentY = 1;
            }

            Console.WriteLine(percentX);

            int x = (int)((gridSizeX - 1) * percentX);
            int y = (int)((gridSizeY - 1) * percentY);

            Console.WriteLine(x);
            Console.WriteLine(y);

            return grid[x, y];
        }
    }
}
