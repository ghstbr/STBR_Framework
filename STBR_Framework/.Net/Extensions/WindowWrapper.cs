﻿using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System;

namespace STBR_Framework
{
    [DebuggerStepThrough]
    internal class WindowWrapper : IWin32Window
    {
        public WindowWrapper()
        {
            this.Handle = GetForegroundWindow();
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        public IntPtr Handle { get; private set; }
    }
}