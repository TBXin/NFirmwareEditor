using System;
using System.ComponentModel;
using JetBrains.Annotations;

namespace NCore
{
	public class AsyncProcessWrapper
	{
		public AsyncProcessWrapper([NotNull] Action<BackgroundWorker> processor)
		{
			if (processor == null) throw new ArgumentNullException("processor");
			Processor = processor;
		}

		[NotNull]
		public Action<BackgroundWorker> Processor { get; private set; }
	}
}
