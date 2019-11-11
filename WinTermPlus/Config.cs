using WinTermPlus.Infrastructure;

namespace WinTermPlus
{
    public class Config
    {
        public WindowSize Size => new WindowSize(new Percentage(Width), new Percentage(Height));

        public bool QuakeMode
        {
            get => Properties.Settings.Default.QuakeMode;
            set
            {
                Properties.Settings.Default.QuakeMode = value;
                Save();
            }
        }

        public int Height
        {
            get => Properties.Settings.Default.Height;
            set
            {
                Properties.Settings.Default.Height = value;
                Save();
            }
        }

        public int Width
        {
            get => Properties.Settings.Default.Width;
            set
            {
                Properties.Settings.Default.Width = value; 
                Save();
            }
        }

        public bool StartWithWindows
        {
            get => Properties.Settings.Default.StartWithWindows;
            set
            {
                Properties.Settings.Default.StartWithWindows = value;
                Save();
            }
        }

        public void Save()
        {
            Properties.Settings.Default.Save();
        }
    }
}