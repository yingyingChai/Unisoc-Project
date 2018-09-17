using System;
using System.Runtime.InteropServices;
namespace KaYi.Utilities
{
	public class WindowsAPI
	{
		public static int GWL_STYLE = -16;
		public static int WS_CHILD = 1073741824;
		public static int WS_BORDER = 8388608;
		public static int WS_DLGFRAME = 4194304;
		public static int WS_CAPTION = WindowsAPI.WS_BORDER | WindowsAPI.WS_DLGFRAME;
		public static int WM_DESTROY = 2;
		public const int SW_SHOWNORMAL = 1;
		public const int SW_SHOWMINIMIZED = 2;
		public const int SW_SHOWMAXIMIZED = 3;
		public const uint WM_CLOSE = 16u;
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
		[DllImport("USER32.DLL")]
		public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
		[DllImport("USER32.DLL")]
		public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
		[DllImport("USER32.DLL")]
		public static extern int GetWindowLong(IntPtr hWnd, int nIndex);
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);
		[DllImport("user32.dll")]
		private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
		public static void SendCloseWindowCommand(IntPtr handler)
		{
			WindowsAPI.SendMessage(handler, 16u, IntPtr.Zero, IntPtr.Zero);
		}
	}
}
