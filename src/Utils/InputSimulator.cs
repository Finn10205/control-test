using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MultiBoxBot.Utils
{
    public class InputSimulator
    {
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        private const uint KEYEVENTF_KEYUP = 2;

        public void ActivateWindow(IntPtr windowHandle)
        {
            try
            {
                SetForegroundWindow(windowHandle);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[InputSimulator] Error activating window: {ex.Message}");
            }
        }

        public void PressKey(string keyCode)
        {
            try
            {
                Keys key = (Keys)Enum.Parse(typeof(Keys), keyCode);
                byte vk = (byte)key;

                keybd_event(vk, 0, 0, UIntPtr.Zero);
                System.Threading.Thread.Sleep(50);

                keybd_event(vk, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[InputSimulator] Error pressing key '{keyCode}': {ex.Message}");
            }
        }

        public void ClickMouse()
        {
            try
            {
                keybd_event(0x01, 0, 0, UIntPtr.Zero);
                System.Threading.Thread.Sleep(50);

                keybd_event(0x01, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[InputSimulator] Error clicking mouse: {ex.Message}");
            }
        }

        public void SendText(string text)
        {
            try
            {
                foreach (char c in text)
                {
                    SendCharacter(c);
                    System.Threading.Thread.Sleep(50);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[InputSimulator] Error sending text: {ex.Message}");
            }
        }

        private void SendCharacter(char c)
        {
            if (char.IsUpper(c))
            {
                byte vk = (byte)char.ToUpper(c);
                keybd_event(0x10, 0, 0, UIntPtr.Zero);
                keybd_event(vk, 0, 0, UIntPtr.Zero);
                keybd_event(vk, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
                keybd_event(0x10, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
            }
            else
            {
                byte vk = (byte)char.ToUpper(c);
                keybd_event(vk, 0, 0, UIntPtr.Zero);
                keybd_event(vk, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
            }
        }

        public void SendCommand(string command)
        {
            try
            {
                if (command.StartsWith("key:"))
                {
                    string keyName = command.Substring(4);
                    PressKey(keyName);
                }
                else if (command.StartsWith("text:"))
                {
                    string text = command.Substring(5);
                    SendText(text);
                }
                else if (command == "click")
                {
                    ClickMouse();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[InputSimulator] Error sending command '{command}': {ex.Message}");
            }
        }
    }
}
