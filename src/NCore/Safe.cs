using System;

namespace NCore
{
	public static class Safe
	{
		public static Exception Execute(Action action)
		{
			try
			{
				action();
				return null;
			}
			catch (Exception ex)
			{
				return ex;
			}
		}
	}
}
