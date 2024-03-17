using System.Data;
using System.Reflection.PortableExecutable;
using System.Transactions;

namespace InfinitiRun
{
    internal class Program
    {
        static public bool[,] Map { get; set; } //true: blocked | false: free

        static void Main()
        {
            Settings.Setup();

            Map = new bool[Console.WindowWidth, Console.WindowHeight];
            WayPrinter.EmptyMap();

            (int Width, int Height) position = (Console.WindowWidth / 2 - 5, Console.WindowHeight - 1);
            WayPrinter.Direction lastPosition = WayPrinter.Direction.Up;

            for (int i = 0; i < 5; i++)
            {
                WayPrinter.DrawLines(WayPrinter.Direction.Up, ref position);
            }

            while (position.Height != -1 && position.Width != -1 && position.Width != Console.WindowWidth + 1)
            {
                GenerateWay(ref position, ref lastPosition);
            }

            Player player = new Player();

            Console.SetCursorPosition(0, Console.WindowHeight);
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int k = 0; k < Map.GetLength(1); k++)
                {
                    Console.SetCursorPosition(i, Console.WindowHeight + k);
                    Console.Write(Map[i, k] ? "X" : " ");
                }
            }

            while (true)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                player.Move(key);
            }

        }

        static void GenerateWay(ref (int Width, int Height) pos, ref WayPrinter.Direction lastDirection)
        {
            // 1: Left
            // 2: Right
            // 3: Up
            // 4: Up -> Left
            // 5: Up -> Right
            // 6: Left -> Up
            // 7: Right -> Up

            // Left     -> Left, LeftToUp
            // Right    -> Right, RightToUp
            // Up       -> Up, UpToLeft, UpToRight
            // Up2Left  -> Left, LeftToUp
            // Up2Right -> Right, RightToUp
            // Left2Up  -> Up, UpToLeft, UpToRight
            // Right2Up -> Up, UpToLeft, UpToRight

            Random random = new Random();
            bool canDraw = false;
            int rndmNumber = 0;

            while (!canDraw && pos.Width != Console.WindowWidth + 1 && pos.Width != -1 && pos.Height != Console.WindowHeight + 1 && pos.Height != -1) //Already OutOfBounds
            {
                canDraw = true;

                rndmNumber = random.Next(1, 8);

                bool isValid =
                    ((int)lastDirection == 1 && (rndmNumber == 1 || rndmNumber == 6)) ||
                    ((int)lastDirection == 2 && (rndmNumber == 2 || rndmNumber == 7)) ||
                    ((int)lastDirection == 3 && (rndmNumber == 3 || rndmNumber == 4 || rndmNumber == 5)) ||
                    ((int)lastDirection == 4 && (rndmNumber == 1 || rndmNumber == 6)) ||
                    ((int)lastDirection == 5 && (rndmNumber == 2 || rndmNumber == 7)) ||
                    ((int)lastDirection == 6 && (rndmNumber == 3 || rndmNumber == 4 || rndmNumber == 5)) ||
                    ((int)lastDirection == 7 && (rndmNumber == 3 || rndmNumber == 4 || rndmNumber == 5));

                if (!isValid) { canDraw = false; }

                if  ((rndmNumber == 4 && (pos.Height < 4 || pos.Width < 9)) || 
                    (rndmNumber == 5 && (pos.Height < 4 || pos.Width > Console.WindowWidth - 9)) ||
                    (rndmNumber == 6 && (pos.Height < 4 || pos.Width > Console.WindowWidth - 9)) ||
                    (rndmNumber == 7 && (pos.Height < 4 || pos.Width < 9))) { canDraw = false; } //Would go OutOfBounds
            }

            if (canDraw)
            {
                WayPrinter.DrawLines((WayPrinter.Direction) rndmNumber, ref pos);
                lastDirection = (WayPrinter.Direction) rndmNumber;
            }
        }
    }
}