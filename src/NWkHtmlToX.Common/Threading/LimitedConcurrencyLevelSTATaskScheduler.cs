using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NWkHtmlToX.Common.Utilities;

namespace NWkHtmlToX.Common.Threading {
    internal sealed class LimitedConcurrencyLevelSTATaskScheduler : TaskScheduler {

        private readonly ConcurrentQueue<Task> _tasks;

        public LimitedConcurrencyLevelSTATaskScheduler(int maximumConcurrencyLevel) {
            if (maximumConcurrencyLevel < 1) throw new ArgumentOutOfRangeException(nameof(maximumConcurrencyLevel));

            MaximumConcurrencyLevel = maximumConcurrencyLevel;
            _tasks = new ConcurrentQueue<Task>();
        }

        public override int MaximumConcurrencyLevel { get; }

        protected override void QueueTask(Task task) {
            Guard.ArgumentNotNull(task, nameof(task));

            _tasks.Enqueue(task);
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued) {
            Guard.ArgumentNotNull(task, nameof(task));

            throw new NotImplementedException();
        }

        protected override IEnumerable<Task> GetScheduledTasks() {
            return _tasks;
        }
    }
}