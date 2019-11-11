namespace WinTermPlus.Infrastructure
{
    public class WindowSize
    {
        private WindowSize() { }
        public WindowSize(Percentage width, Percentage height)
        {
            Width = width;
            Height = height;
        }

        public Percentage Width { get; }
        public Percentage Height { get; }
    }
}