namespace WinTermPlus.Infrastructure
{
    public class WindowPosition
    {
        private WindowPosition() { }
        public WindowPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }
    }
}