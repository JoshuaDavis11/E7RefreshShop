using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;
using AutoHotkey.Interop;

namespace E7RefreshShop
{
    public static class MouseHelper
    {
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, UIntPtr dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;

        public static void RefreshButtonClickAt(Point point)
        {
            Thread.Sleep(100); // Give it time to focus
            // Move cursor
            SetCursorPos(point.X, point.Y);

            // Nudge the cursor slightly to ensure it's registered
            SetCursorPos(point.X + 1, point.Y + 1);
            SetCursorPos(point.X, point.Y);

            // Perform click
            mouse_event(MOUSEEVENTF_LEFTDOWN, point.X, point.Y, 0, UIntPtr.Zero);
            Thread.Sleep(50); // Optional delay for realism
            mouse_event(MOUSEEVENTF_LEFTUP, point.X, point.Y, 0, UIntPtr.Zero);
        }

        public static void BuyItemClickAt(Point point)
        {
            Thread.Sleep(100); // Give it time to focus

            // Move cursor
            SetCursorPos(point.X + 550, point.Y + 25);

            // Perform click
            mouse_event(MOUSEEVENTF_LEFTDOWN, point.X + 550, point.Y + 25, 0, UIntPtr.Zero);
            Thread.Sleep(50); // Optional delay for realism
            mouse_event(MOUSEEVENTF_LEFTUP, point.X + 550, point.Y + 25, 0, UIntPtr.Zero);
        }


    }

}
