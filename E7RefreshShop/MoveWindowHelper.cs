using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace E7RefreshShop
{
    public class MoveWindowHelper
    {
        //import the dlls and windows api methods we will use
        [DllImport("user32.dll")]
        static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        static readonly IntPtr HWND_TOP = IntPtr.Zero;
        const uint SWP_SHOWWINDOW = 0x0040;

        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_RESTORE = 9;
        public static void BringWindowToFront(IntPtr hWnd)
        {
            ShowWindow(hWnd, SW_RESTORE);         // Restore if minimized
            SetForegroundWindow(hWnd);            // Bring to front
        }
        public static void MoveWindowWithSetWindowPos(IntPtr hWnd, int x, int y, int width, int height)
        {
            SetWindowPos(hWnd, HWND_TOP, x, y, width, height, SWP_SHOWWINDOW);
        }
        public static void MoveGoogleGamesWindowHelper()
        {
            
            string windowName = "LDPlayer";
            IntPtr hWnd = FindWindow(null, windowName);
            
            //Bring the application to the front
            BringWindowToFront(hWnd);

            //To get the window size, uncomment the line below
            //GetWindowSizeHelper.GetTheWindowSize(hWnd);


            if (hWnd == IntPtr.Zero)
            {
                Console.WriteLine($"Window '{windowName}' not found.");
                return;
            }
            //bool moved = MoveWindow(hWnd, 100, 100, 1650, 739, true);
            MoveWindowWithSetWindowPos(hWnd, 100, 100, 1650, 739);
            //Console.WriteLine(moved ? "Window moved" : "Failed to move window.");
        }
    }
}
