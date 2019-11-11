using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace WinTermPlus.Interop
{
    public class WindowHandles
    {
        private readonly Process _process;

        public WindowHandles(Process process)
        {
            _process = process;
        }

        public List<IntPtr> Handles
        {
            get
            {
                var handles = new List<IntPtr>();

                var threads = _process.Threads;
                foreach (ProcessThread thread in threads)
                    PInvoke.EnumThreadWindows(thread.Id,
                        (hWnd, lParam) =>
                        {
                            handles.Add(hWnd);
                            return true;
                        }, IntPtr.Zero);

                return handles;                
            }
        }

        public void ApplyToAll(Action<IntPtr> action)
        {
            foreach (var handle in Handles)
            {
                action(handle);
            }
        }
    }
}