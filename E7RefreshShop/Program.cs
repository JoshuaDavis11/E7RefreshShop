using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace E7RefreshShop
{
    class Program
    {
        [DllImport("user32.dll")]
        static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

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

        static void Main(string[] args)
        {
            //This only resizes the the game if you're running it through Google games beta
            string windowName = "Epic Seven : Origin";

            IntPtr hWnd = FindWindow(null, windowName);

            if (hWnd == IntPtr.Zero)
            {
                Console.WriteLine($"Window '{windowName}' not found.");
                return;
            }

            bool moved = MoveWindow(hWnd, 100, 100, 1650, 739, true);
            Console.WriteLine(moved ? "Window moved" : "Failed to move window.");

            Rectangle captureArea = new Rectangle(100, 100, 1650, 739);
            Bitmap screenshot = GetScreenshot(captureArea);
            screenshot.Save("sceenshot.png");
            Console.WriteLine("Screenshot saved as screenshot.png");

            // Get the window size and position, this is useful for debugging
            //if (GetWindowRect(hWnd, out RECT rect))
            //{
            //    Console.WriteLine($"Window position: ({rect.Left}, {rect.Top})");
            //    Console.WriteLine($"Window size: {rect.Width}x{rect.Height}");
            //}
            //else
            //{
            //    Console.WriteLine("Failed to get window rect.");
            //}
        }

        static Bitmap GetScreenshot(Rectangle area)
        {
            Bitmap bmp = new Bitmap(area.Width, area.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(area.Location, Point.Empty, area.Size);
            }

            return bmp;
        }

       
    }
}
