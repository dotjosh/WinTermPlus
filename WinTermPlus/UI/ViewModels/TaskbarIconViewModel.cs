using System.Windows;
using System.Windows.Input;
using WinTermPlus.Infrastructure;

namespace WinTermPlus.UI.ViewModels
{
    public class TaskbarIconViewModel
    {
        public ICommand ExitApplicationCommand
        {
            get
            {
                return new DelegateCommand(() => Application.Current.Shutdown());
            }
        }
        public ICommand ConfigCommand
        {
            get
            {
                return new DelegateCommand(() => Application.Current.MainWindow.Show());
            }
        }
    }

}