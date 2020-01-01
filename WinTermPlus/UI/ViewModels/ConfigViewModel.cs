using Caliburn.Micro;
using System.Windows;
using WinTermPlus.Infrastructure;
using WinTermPlus.Interop;

namespace WinTermPlus.UI.ViewModels
{
    public class ConfigViewModel : Screen
    {
        private readonly Config _config;

        public ConfigViewModel(Config config)
        {
            _config = config;
        }

        public bool QuakeMode
        {
            get => _config.QuakeMode;
            set
            {
                _config.QuakeMode = value;
                NotifyOfPropertyChange(nameof(QuakeMode));
            }
        }

        public Percentage Width
        {
            get => _config.Size.Width;
            set
            {
                _config.Width = value.ToInt();
                NotifyOfPropertyChange(nameof(Width));
            }
        }

        public Percentage Height
        {
            get => _config.Size.Height;
            set
            {
                _config.Height = value.ToInt();
                NotifyOfPropertyChange(nameof(Height));
            }
        }

        public int PositionX
        {
            get => _config.PositionX;
            set
            {
                _config.PositionX = value;
                NotifyOfPropertyChange(nameof(PositionX));
            }
        }

        public int PositionY
        {
            get => _config.PositionY;
            set
            {
                _config.PositionY = value;
                NotifyOfPropertyChange(nameof(PositionY));
            }
        }

        public bool StartWithWindows
        {
            get => _config.StartWithWindows;
            set
            {
                _config.StartWithWindows = value;
                WindowsStartup.UpdateStartupKey(value);
                NotifyOfPropertyChange(nameof(Height));
            }
        }

        public override void NotifyOfPropertyChange(string propertyName = null)
        {
            base.NotifyOfPropertyChange(propertyName);

            var process = WindowsTerminalProcess.Get();
            process?.ResizeAndPositionWindow(_config.Size, _config.Position);
        }

        public void Done()
        {
            Application.Current.MainWindow.Hide();
        }
    }
}
