using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfinitiRun
{
    public static class WayPrinter
    {
        public enum Direction 
        { 
            Left = 1, 
            Right = 2, 
            Up = 3, 
            UpToLeft = 4, 
            UpToRight = 5, 
            LeftToUp = 6, 
            RightToUp = 7 
        }

        public static void SetCursor((int Width, int Height) pos) => Console.SetCursorPosition(pos.Width, pos.Height);

        public static void DrawLines(Direction dir, ref (int Width, int Height) pos)
        {
            SetCursor(pos);

            #region Up
            if (dir == Direction.Up)
            {
                Console.Write("│");

                pos.Width += 9;
                SetCursor(pos);

                Console.Write("│");

                pos.Height -= 1;
                pos.Width -= 9;
            }
            #endregion

            #region Left / Right
            else if (dir == Direction.Left || dir == Direction.Right)
            {
                bool single = false;
                if (pos.Width == Console.WindowWidth - 1|| pos.Width == 0) { single = true; }

                if (dir == Direction.Left && !single) { pos.Width -= 1; }
                SetCursor(pos);

                Console.Write(!single ? "──" : "─");

                pos.Height += 4;
                SetCursor(pos);

                Console.Write(!single ? "──" : "─");

                pos.Height -= 4;
            }
            #endregion

            #region Up -> Left
            else if (dir == Direction.UpToLeft)
            {
                Console.Write("┐        │");
                pos.Height -= 1;
                SetCursor(pos);

                for (int i = 0; i < 3; i++)
                {
                    Console.Write("         │");
                    pos.Height -= 1;
                    SetCursor(pos);
                }

                Console.Write("─────────┐");

                pos.Width -= 1;
            }
            #endregion

            #region Up -> Right
            else if (dir == Direction.UpToRight)
            {
                Console.Write("│        ┌");
                pos.Height -= 1;
                SetCursor(pos);

                for (int i = 0; i < 3; i++)
                {
                    Console.Write("│         ");
                    pos.Height -= 1;
                    SetCursor(pos);
                }

                Console.Write("┌─────────");

                pos.Width += 10;
            }
            #endregion

            #region Left -> Up
            else if (dir == Direction.LeftToUp)
            {
                pos.Width -= 9;
                SetCursor(pos);

                Console.Write("│        └");
                pos.Height += 1;
                SetCursor(pos);

                for (int i = 0; i < 3; i++)
                {
                    Console.Write("│         ");
                    pos.Height += 1;
                    SetCursor(pos);
                }

                Console.Write("└─────────");

                pos.Height -= 5;
            }
            #endregion

            #region Right -> Up
            else if (dir == Direction.RightToUp)
            {
                pos.Height += 4;
                SetCursor(pos);

                Console.Write("─────────┘");
                pos.Height -= 1;
                SetCursor(pos);

                for (int i = 0; i < 3; i++)
                {
                    Console.Write("         │");
                    pos.Height -= 1;
                    SetCursor(pos);
                }

                Console.Write("┘        │");

                pos.Height -= 1;
            }
            #endregion

            if (dir == Direction.Left) { pos.Width -= 1; }
            if (dir == Direction.Right) { pos.Width += 2; }
        }
    }
}
