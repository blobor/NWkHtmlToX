using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NWkHtmlToX.Common.Utilities;

namespace NWkHtmlToX.Common.Threading {
    internal sealed class STAThreadPerTaskScheduler : TaskScheduler {

        private readonly IThreadFactory _threadFactory;

        internal STAThreadPerTaskScheduler() {
            _threadFactory = new STAThreadFactory();
        }

        protected override void QueueTask(Task task) {
            Guard.ArgumentNotNull(task, nameof(task));

            _threadFactory.Create((Task queuedTask) => TryExecuteTask(queuedTask)).Start(task);
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued) {
            Guard.ArgumentNotNull(task, nameof(task));
            if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA) return false;

            return taskWasPreviouslyQueued || TryExecuteTask(task);
        }

        protected override IEnumerable<Task> GetScheduledTasks() {
            return Enumerable.Empty<Task>();
        }
    }
}