using System;
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace aStar
{
    static class Program
    {
        
        public static void Main()
        {
            Vector2 vOne = new Vector2(1, 1);
            Vector2 vBlock = new Vector2(130, 210);

            Color[] rColor = { Color.BLACK, Color.GREEN, Color.BROWN, Color.GOLD }; 

            InitWindow(900, 900, "A*");

            Grid gr = new Grid();
            Pathfinding pf = new Pathfinding();
            Node[,] grid = gr.Start(GetScreenHeight(), GetScreenWidth());

            while (!WindowShouldClose())
            {
                Vector2 mousePos = GetMousePosition();

                if (IsMouseButtonReleased(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    Node nodePos = gr.GetNodePos(mousePos);
                    nodePos.walkable = !nodePos.walkable;
                }

                BeginDrawing();

                ClearBackground(Color.BLACK);

                foreach (Node n in grid)
                {

                    DrawRectangleV(n.worldPosition, vOne * (40 - .7f), (n.walkable)?Color.GRAY:Color.RED);
                    if (n.visited)
                    {
                        DrawRectangleV(n.worldPosition, vOne * (40 - .7f), Color.LIGHTGRAY);
                    }

                    if (n.isPath)
                    {
                        DrawRectangleV(n.worldPosition, vOne * (40 - .7f), Color.YELLOW);
                    }
                    if (n.worldPosition == gr.nStart.worldPosition)
                    {
                        DrawRectangleV(n.worldPosition, vOne * (40 - .7f), Color.ORANGE);
                    }
                    else if (n.worldPosition == gr.nFinish.worldPosition)
                    {
                        DrawRectangleV(n.worldPosition, vOne * (40 - .7f), Color.VIOLET);
                    }
                    
                }

                if (IsKeyPressed(KeyboardKey.KEY_S))
                {
                    pf.FindPath(gr.nStart, gr.nFinish, grid.GetLength(0), grid.GetLength(1), grid);
                }

                EndDrawing();

                
            }

            CloseWindow();
        }

        
    }
}