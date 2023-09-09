using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading;

public class Program
{

  #region Native Stuff
  [DllImport("user32.dll", SetLastError = true)]
  static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

  [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
  public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

  [DllImport("user32.dll")]
  public static extern bool GetWindowRect(IntPtr hwnd, ref Rectangle rectangle);

  const short SWP_NOSIZE = 1;
  const short SWP_NOZORDER = 0X4;
  const int SWP_SHOWWINDOW = 0x0040;

  #endregion

  public static IntPtr FindNotificationWindow()
  {
    return FindWindow("Windows.UI.Core.CoreWindow", "New notification");
  }

  public static IntPtr getNotificationWindow()
  {
    IntPtr hwnd = FindNotificationWindow();
    while (hwnd == IntPtr.Zero)
    {
      hwnd = FindNotificationWindow();
      Thread.Sleep(10);
    }
    return hwnd;
  }

  public static void Main(string[] args)
  {
    IntPtr hwnd = getNotificationWindow();
    Rectangle NotifyRect = new Rectangle();
    while (true)
    {
      GetWindowRect(hwnd, ref NotifyRect);
      if (NotifyRect.Height != NotifyRect.Y && NotifyRect.Y != -50)
        SetWindowPos(hwnd, 0, Screen.PrimaryScreen.Bounds.Width - NotifyRect.Width + NotifyRect.X, -50, 0, 0, SWP_NOSIZE | SWP_NOZORDER | SWP_SHOWWINDOW);
      Thread.Sleep(10);
    }
  }
}
