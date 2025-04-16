using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace E7RefreshShop
{
    public class GetWindowSizeHelper
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public int Width => Right - Left;
            public int Height => Bottom - Top;
        }

        [DllImport("user32.dll")]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        public static void GetTheWindowSize(IntPtr hWnd)
        {
            // Get the window size and position, this is useful for debugging  
            if (GetWindowRect(hWnd, out RECT rect))
            {
                Console.WriteLine($"Window position: ({rect.Left}, {rect.Top})");
                Console.WriteLine($"Window size: {rect.Width}x{rect.Height}");
            }
            else
            {
                Console.WriteLine("Failed to get window rect.");
            }
        }

    }
}
