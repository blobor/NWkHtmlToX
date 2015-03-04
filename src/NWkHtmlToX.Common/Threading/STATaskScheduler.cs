using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NWkHtmlToX.Common.Threading {
    internal sealed class STATaskScheduler : TaskScheduler {

        private readonly IThreadFactory _threadFactory;

        public STATaskScheduler(IThreadFactory factory) {
            if(factory == null) throw new ArgumentNullException(nameof(factory));

            _threadFactory = factory;
        }

        protected override void QueueTask(Task task) {
            throw new System.NotImplementedException();
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued) {
            throw new System.NotImplementedException();
        }

        protected override IEnumerable<Task> GetScheduledTasks() {
            throw new System.NotImplementedException();
        }
    }
}