using System.Reflection.PortableExecutable;

namespace InfinitiRun
{
    internal class Program
    {
        static void Main()
        {
            Settings.Setup();

            (int Width, int Height) position = (Console.WindowWidth / 2 - 5, Console.WindowHeight - 1);

            for (int i = 0; i < 5; i++)
            {
                WayPrinter.DrawLines(WayPrinter.Direction.Up, ref position);
            }

            WayPrinter.DrawLines(WayPrinter.Direction.UpToRight, ref position);
            WayPrinter.DrawLines(WayPrinter.Direction.Right, ref position);
            WayPrinter.DrawLines(WayPrinter.Direction.RightToUp, ref position);
            WayPrinter.DrawLines(WayPrinter.Direction.Up, ref position);
            WayPrinter.DrawLines(WayPrinter.Direction.UpToLeft, ref position);
            WayPrinter.DrawLines(WayPrinter.Direction.Left, ref position);
            WayPrinter.DrawLines(WayPrinter.Direction.LeftToUp, ref position);
            WayPrinter.DrawLines(WayPrinter.Direction.Up, ref position);

            WayPrinter.SetCursor(position);

            Console.ReadKey(true);
        }
    }
}