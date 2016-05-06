using System;

namespace NFirmwareEditor.Core
{
	internal static class Safe
	{
		public static void Execute(Action action)
		{
			try
			{
				action();
			}
			catch
			{
				// Ignore
			}
		}
	}
}
