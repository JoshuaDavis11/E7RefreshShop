using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace E7RefreshShop
{
    public class HotKeyListener
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public static void StartListening()
        {
            new Thread(() =>
            {
                const int HOTKEY_ID = 1;
                const uint MOD_NONE = 0x0000;
                const uint VK_ESCAPE = 0x1B;

                if (!RegisterHotKey(IntPtr.Zero, HOTKEY_ID, MOD_NONE, VK_ESCAPE))
                {
                    Console.WriteLine("Failed to register hotkey.");
                    return;
                }

                Console.WriteLine("ESC hotkey registered. Press ESC to stop the bot.");

                try
                {
                    while (true)
                    {
                        MSG msg;
                        if (GetMessage(out msg, IntPtr.Zero, 0, 0) != 0)
                        {
                            if (msg.message == WM_HOTKEY && (int)msg.wParam == HOTKEY_ID)
                            {
                                Console.WriteLine("ESC pressed.");
                                EscPressed = true;
                                break;
                            }
                        }
                    }
                }
                finally
                {
                    UnregisterHotKey(IntPtr.Zero, HOTKEY_ID);
                }

            }).Start();
        }

        public static bool EscPressed = false;

        private const int WM_HOTKEY = 0x0312;

        [StructLayout(LayoutKind.Sequential)]
        private struct MSG
        {
            public IntPtr hwnd;
            public uint message;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public System.Drawing.Point pt;
        }

        [DllImport("user32.dll")]
        private static extern int GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);
    }
}
