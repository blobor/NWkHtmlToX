using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NWkHtmlToX.Common.Extensions.Enumerable;
using NWkHtmlToX.Common.Utilities;

namespace NWkHtmlToX.Common.Threading {
    internal sealed class LimitedConcurrencyLevelSTATaskScheduler : TaskScheduler {

        private int _numberOfCurrentlyRunningThreads;
        private readonly ConcurrentQueue<Task> _tasks;
        private readonly IThreadFactory _threadFactory;
        private readonly ThreadLocal<bool> _isCurrentThreadProcessingTask = new ThreadLocal<bool>(() => false);

        public LimitedConcurrencyLevelSTATaskScheduler(int maximumConcurrencyLevel) {
            ThrowIf.Argument.IsOutOfRange(maximumConcurrencyLevel, nameof(maximumConcurrencyLevel), 1, Int32.MinValue);

            _numberOfCurrentlyRunningThreads = 0;
            _tasks = new ConcurrentQueue<Task>();
            _threadFactory = new STAThreadFactory();
            MaximumConcurrencyLevel = maximumConcurrencyLevel;
        }

        public override int MaximumConcurrencyLevel { get; }

        protected override void QueueTask(Task task) {
            ThrowIf.Argument.IsNull(task, nameof(task));

            _tasks.Enqueue(task);

            if (_numberOfCurrentlyRunningThreads < MaximumConcurrencyLevel) {
                Interlocked.Increment(ref _numberOfCurrentlyRunningThreads);
                _threadFactory.Create(ExecuteWork).Start();
            }
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued) {
            ThrowIf.Argument.IsNull(task, nameof(task));

            // If this thread isn't already processing a task, we don't support inlining
            if (_isCurrentThreadProcessingTask.Value) return false;

            // check if CurrentThread have appropriate ApartmentState
            if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA) return false;

            // Try to run the task.
            return taskWasPreviouslyQueued ? TryDequeue(task) && TryExecuteTask(task) : TryExecuteTask(task);
        }

        protected override IEnumerable<Task> GetScheduledTasks() {
            return _tasks.ToArray(_tasks.Count);
        }

        private void ExecuteWork() {
            _isCurrentThreadProcessingTask.Value = true;
            try {
                while (!_tasks.IsEmpty) {
                    Task queuedTask;
                    if (_tasks.TryDequeue(out queuedTask)) {
                        TryExecuteTask(queuedTask);
                    }
                }
            } finally {
                _isCurrentThreadProcessingTask.Value = false;
            }
        }
    }
}