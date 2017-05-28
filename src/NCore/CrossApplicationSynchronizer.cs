using System;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using JetBrains.Annotations;

namespace NCore
{
	/// <summary>
	/// Provides an ability to capture cross-process lock.
	/// </summary>
	public class CrossApplicationSynchronizer : IDisposable
	{
		private static readonly IntPtr s_broadCast = (IntPtr)0xFFFF;
		public static readonly int ShowFirstInstanceMessage = NativeMethods.RegisterWindowMessage("NFE_WM_SHOWFIRSTINSTANCE");

		private readonly Mutex m_mutex;

		/// <summary>
		/// Initializes a new instance of the <see cref="CrossApplicationSynchronizer"/> class.
		/// </summary>
		public CrossApplicationSynchronizer([NotNull] string identifier)
		{
			if (string.IsNullOrEmpty(identifier)) throw new ArgumentNullException("identifier");

			m_mutex = new Mutex(false, string.Format("Global\\{0}", identifier));
			var securitySettings = new MutexSecurity();
			{
				securitySettings.AddAccessRule(new MutexAccessRule
				(
					new SecurityIdentifier(WellKnownSidType.WorldSid, null),
					MutexRights.FullControl, AccessControlType.Allow
				));
			}
			m_mutex.SetAccessControl(securitySettings);

			try
			{
				IsLockObtained = m_mutex.WaitOne(100, false);
			}
			catch (AbandonedMutexException)
			{
				IsLockObtained = true;
			}
		}

		/// <summary>
		/// Indicates whether lock obtainer or not.
		/// </summary>
		public bool IsLockObtained { get; private set; }

		public void ShowFirstInstance()
		{
			NativeMethods.SendMessage(s_broadCast, ShowFirstInstanceMessage, IntPtr.Zero, IntPtr.Zero);
		}

		public void Dispose()
		{
			if (m_mutex == null) return;
			if (IsLockObtained) m_mutex.ReleaseMutex();
			m_mutex.Dispose();
		}
	}
}
