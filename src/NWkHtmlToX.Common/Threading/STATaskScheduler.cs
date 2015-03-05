using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NWkHtmlToX.Common.Utilities;

namespace NWkHtmlToX.Common.Threading {
    internal sealed class STATaskScheduler : TaskScheduler {

        private readonly IThreadFactory _threadFactory;

        public STATaskScheduler(IThreadFactory factory) {
            Guard.ArgumentNotNull(factory, nameof(factory));

            _threadFactory = factory;
        }

        protected override void QueueTask(Task task) {
            throw new NotImplementedException();
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued) {
            throw new NotImplementedException();
        }

        protected override IEnumerable<Task> GetScheduledTasks() {
            throw new NotImplementedException();
        }
    }
}