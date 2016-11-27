using System;
using System.Runtime.InteropServices;

namespace NCore
{
	public static class NativeMethods
	{
		[DllImport("user32")]
		public static extern bool SetForegroundWindow(IntPtr hWnd);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

		[return: MarshalAs(UnmanagedType.Bool)]
		[DllImport("user32", SetLastError = true)]
		public static extern bool PostMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

		[DllImport("user32")]
		public static extern int RegisterWindowMessage(string message);
	}
}
