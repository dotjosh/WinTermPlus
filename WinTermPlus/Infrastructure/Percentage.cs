using System;

namespace WinTermPlus.Infrastructure
{
    public class Percentage
    {
        private Percentage() { }
        private int _val;

        public Percentage(int val)
        {
            _val = val;
        }

        public double ToDouble()
        {
            return _val * .01;
        }

        public int ToInt()
        {
            return _val;
        }
    }
}