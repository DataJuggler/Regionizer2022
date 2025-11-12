using System;
using System.Windows.Forms;

namespace DataJuggler.Regionizer.UI
{
    public sealed class Win32Window : IWin32Window
    {
        // private backing field (no auto-properties)
        private readonly IntPtr handle;

        public Win32Window(IntPtr handle)
        {
            this.handle = handle;
        }

        public IntPtr Handle
        {
            get { return handle; }
        }
    }
}