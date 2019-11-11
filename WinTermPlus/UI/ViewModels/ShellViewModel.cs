using Caliburn.Micro;
using NHotkey;
using NHotkey.Wpf;
using System.Windows.Controls;
using System.Windows.Input;
using WinTermPlus.Interop;

namespace WinTermPlus.UI.ViewModels
{
    public class ShellViewModel
    {
        private readonly SimpleContainer _container;
        private INavigationService _navigationService;
        private readonly Config _settings;

        public ShellViewModel(SimpleContainer container, Config settings)
        {
            HotkeyManager.Current.Remove("Increment");
            HotkeyManager.Current.AddOrReplace("Increment", Key.OemTilde, ModifierKeys.Control, true, OnQuakeModeKey);

            _container = container;
            _settings = settings;
        }
        
        public void RegisterFrame(Frame frame)
        {
            _navigationService = new FrameAdapter(frame);
            _container.Instance(_navigationService);
            _navigationService.NavigateToViewModel(typeof(ConfigViewModel));
        }

        private void OnQuakeModeKey(object sender, HotkeyEventArgs e)
        {
            if (!_settings.QuakeMode)
            {
                return;
            }

            var windowsTerminalProcess = WindowsTerminalProcess.Get();
            if(windowsTerminalProcess == null)
            {
                windowsTerminalProcess = WindowsTerminalProcess.Launch();
                windowsTerminalProcess.Show(_settings.Size);
            }
            else
            {
                windowsTerminalProcess.ToggleVisibility(_settings.Size);
            }
        }
    }
}
