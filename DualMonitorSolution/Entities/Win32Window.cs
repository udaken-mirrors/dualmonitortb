using System;
using System.Windows.Forms;
using System.Drawing;
using DualMonitor.Win32;

namespace DualMonitor.Entities
{
    public class Win32Window : IWin32Window
    {
        protected Win32Window(IntPtr handle)
        {
            _handle = handle;
        }

        public static Win32Window FromHandle(IntPtr handle)
        {
            return new Win32Window(handle);
        }

        public static Win32Window FromClassName(string className)
        {
            var hwnd = Native.FindWindow(className, null);
            return FromHandle(hwnd);
        }

        protected IntPtr _handle;
        public string _title;
        protected string _path;
        protected string _arguments;
        protected Icon _icon;
        protected Icon _smallIcon;
        protected string _className;

        public string Title => _title ?? (_title = Native.GetWindowTextWithTimeout(Handle, 500));

        public string Path => _path ?? (_path = ProcessUtil.GetProcessPathByWindowHandle(Handle));

        public string Arguments => _arguments ?? (_arguments = ProcessUtil.GetCommandLineArguments(Handle));

        public Icon Icon
        {
            get
            {
                if (_icon == null)
                {
                    try
                    {
                        _icon = Native.GetIconWithTimeout(Handle, 500);
                    }
                    catch
                    {
                        _icon = null;
                    }
                }
                return _icon;
            }
        }

        public Icon SmallIcon => _smallIcon ?? (_smallIcon = Native.GetSmallIconWithTimeout(Handle, 500) ?? Icon);

        public string ClassName => _className ?? (_className = Native.GetClassName(Handle));

        public IntPtr Handle => _handle;

        public bool IsMinimized => Native.IsIconic(Handle);

        public Screen Screen => Screen.FromHandle(Handle);

        public Rectangle Bounds
        {
            get
            {
                Native.RECT r;
                Native.GetWindowRect(Handle, out r);

                return (Rectangle)r;
            }
        }

        public void ActivateWindow(bool toggle)
        {
            if (IsMinimized)
            {
                Native.ShowWindowAsync(Handle, Native.ShowWinCmdShow);
                Native.ShowWindowAsync(Handle, Native.ShowWinCmdRestore);
                Native.SetForegroundWindow(Handle);
            }
            else
            {
                if (toggle)
                {
                    Native.ShowWindowAsync(Handle, Native.ShowWinCmdMinimized);
                }
                else
                {
                    Native.SetForegroundWindow(Handle);
                }
            }
        }
     
        public void MoveWindowTo(Screen screen)
        {
            if (!Screen.Equals(screen))
            {
                Native.RECT r;
                Native.GetWindowRect(Handle, out r);

                var width = (screen.Bounds.Right + screen.Bounds.Left) / 2;
                var height = (screen.Bounds.Bottom + screen.Bounds.Top) / 2;

                var windowWidth = r.right - r.left;
                var windowHeight = r.bottom - r.top;

                Native.MoveWindow(Handle, width - windowWidth/2, height - windowHeight/2, windowWidth, windowHeight, true);
            }
        }

        public Win32Window FindWindow(string className)
        {
            if (Handle == IntPtr.Zero)
            {
                return FromHandle(IntPtr.Zero);
            }

            IntPtr hwnd = Native.FindWindowEx(Handle, IntPtr.Zero, className, null);
            return FromHandle(hwnd);
        }

        public void Minimize()
        {
            Native.ShowWindowAsync(Handle, Native.ShowWinCmdMinimized);
        }

        public void Restore()
        {
            Native.ShowWindowAsync(Handle, Native.ShowWinCmdRestore);
        }
    }
}
