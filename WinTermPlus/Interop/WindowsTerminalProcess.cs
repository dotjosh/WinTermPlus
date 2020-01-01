using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using WinTermPlus.Infrastructure;

namespace WinTermPlus.Interop
{
    public partial class WindowsTerminalProcess
    {
        private readonly Process _process;

        private WindowsTerminalProcess(Process process)
        {
            _process = process;
        }

        public bool IsRunning => _process != null;

        public bool IsFocused
        {
            get
            {
                var handles = new WindowHandles(_process);
                return handles.Handles.Any(handle => PInvoke.GetForegroundWindow() == handle);
            }
        }

        private bool RunOnHandle(Action<IntPtr> action)
        {
            if (!IsRunning)
            {
                return false;
            }

            var handles = new WindowHandles(_process);
            if (handles.Handles.Count == 0)
            {
                return false;
            }

            var handle = handles.Handles[0];
            action(handle);
            return true;
        }

        public void ToggleVisibility(WindowSize size, WindowPosition position)
        {
            if (IsFocused)
            {
                Hide();
            }
            else
            {
                Show(size, position);
            }
        }

        public void Show(WindowSize size, WindowPosition position)
        {
            RunOnHandle(handle =>
            {
                PInvoke.ShowWindow(handle, ShowWindowCommands.Restore);
                PInvoke.SetForegroundWindow(handle);
                ResizeAndPositionWindow(handle, size, position);
            });
        }

        public void Hide()
        {
            RunOnHandle(handle =>
                PInvoke.ShowWindow(handle, ShowWindowCommands.Minimize)
            );
        }

        private void ResizeAndPositionWindow(IntPtr handle, WindowSize size, WindowPosition windowPosition)
        {
            var primaryScreenBounds = Screen.PrimaryScreen.Bounds;
            var width = (int)Math.Floor(primaryScreenBounds.Width * size.Width.ToDouble());
            var height = (int)Math.Floor(primaryScreenBounds.Height * size.Height.ToDouble());

            PInvoke.MoveWindow(handle, windowPosition.X, windowPosition.Y, width, height, true);
        }

        public static WindowsTerminalProcess Get()
        {
            var process = Process.GetProcessesByName("WindowsTerminal").FirstOrDefault();
            if (process != null)
            {
                return new WindowsTerminalProcess(process);
            }
            return null;
        }

        public static WindowsTerminalProcess Launch()
        {
            var localAppDataPath = Environment.GetEnvironmentVariable("LocalAppData");
            var wtFullPath = Path.Combine(localAppDataPath, @"Microsoft\WindowsApps\wt.exe");

            var process = new Process();
            process.StartInfo.FileName = wtFullPath;
            process.Start();

            return new WindowsTerminalProcess(process);
        }

        public void ResizeAndPositionWindow(WindowSize windowSize, WindowPosition windowPosition)
        {
            RunOnHandle(handle => ResizeAndPositionWindow(handle, windowSize, windowPosition));
        }
    }
}