using System;
using System.Threading;
using JetBrains.Annotations;

namespace NCore
{
	/// <summary>
	/// Provides ability to ensure that only one instance created.
	/// </summary>
	public class SingleInstanceProvider : IDisposable
	{
		private readonly Mutex m_mutex;
		private bool m_newMutedCreated;

		/// <summary>
		/// Initializes a new instance of the <see cref="SingleInstanceProvider"/> class.
		/// </summary>
		public SingleInstanceProvider([NotNull] string identifier)
		{
			if (string.IsNullOrEmpty(identifier)) throw new ArgumentNullException("identifier");
			m_mutex = new Mutex(true, identifier, out m_newMutedCreated);
		}

		/// <summary>
		/// Indicates whether an instance with the specified name created or not.
		/// </summary>
		public bool IsCreated
		{
			get { return !m_newMutedCreated; }
		}

		public void Dispose()
		{
			Release();
			GC.SuppressFinalize(this);
		}

		private void Release()
		{
			if (!m_newMutedCreated) return;

			m_mutex.ReleaseMutex();
			m_newMutedCreated = false;
		}
	}
}
