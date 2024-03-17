using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfinitiRun
{
    public class Player
    {
        (int Width, int Height) playerPos;
        enum State { OldPos, NewPos }

        public Player()
        {
            playerPos = (Console.WindowWidth / 2, Console.WindowHeight - 1);

            Console.SetCursorPosition(playerPos.Width - 1, playerPos.Height);
            Console.Write("xx");
        }

        public void Move(ConsoleKey key)
        {
            if ((key == ConsoleKey.W || key == ConsoleKey.UpArrow) && !Program.Map[playerPos.Width, playerPos.Height - 1])
            {
                DrawPlayer(State.OldPos);
                playerPos.Height -= 1;
                DrawPlayer(State.NewPos);
            }

            if ((key == ConsoleKey.A || key == ConsoleKey.LeftArrow) && !Program.Map[playerPos.Width - 2, playerPos.Height])
            {
                DrawPlayer(State.OldPos);
                playerPos.Width -= 1;
                DrawPlayer(State.NewPos);
            }

            if ((key == ConsoleKey.D || key == ConsoleKey.RightArrow) && !Program.Map[playerPos.Width + 2, playerPos.Height])
            {
                DrawPlayer(State.OldPos);
                playerPos.Width += 1;
                DrawPlayer(State.NewPos);
            }

            if ((key == ConsoleKey.S || key == ConsoleKey.DownArrow) && !Program.Map[playerPos.Width, playerPos.Height + 1])
            {
                DrawPlayer(State.OldPos);
                playerPos.Height += 1;
                DrawPlayer(State.NewPos);
            }
        }

        void DrawPlayer(State state)
        {
            Console.SetCursorPosition(playerPos.Width - 1, playerPos.Height);

            if (state == State.OldPos)
            {
                Console.Write("  ");
            }
            else
            {
                Console.Write("xx");
            }
        }
    }
}
