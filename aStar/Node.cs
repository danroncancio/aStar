using System.Numerics;

namespace aStar
{
    public class Node
    {
        public bool walkable;
        public Vector2 worldPosition;
        public int gridX;
        public int gridY;

        public bool visited;
        public int gCost;
        public int hCost;
        public Node parent;

        public bool isPath;

        public Node(bool _walkable, Vector2 _worldPosition, bool _visited, int _gridX, int _gridY)
        {
            walkable = _walkable;
            worldPosition = _worldPosition;
            visited = _visited;
            gridX = _gridX;
            gridY = _gridY;
        }

        public int fCost
        {
            get
            {
                return gCost + hCost;
            }
        }
    }
}
