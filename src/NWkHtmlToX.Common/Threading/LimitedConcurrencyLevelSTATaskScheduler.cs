using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NWkHtmlToX.Common.Extensions.Enumerable;
using NWkHtmlToX.Common.Utilities;

namespace NWkHtmlToX.Common.Threading {
    internal sealed class LimitedConcurrencyLevelSTATaskScheduler : TaskScheduler {

        private int _numberOfCurrentlyRunningThreads;
        private readonly object _lockObject = new object();
        private readonly LinkedList<Task> _tasks;
        private readonly IThreadFactory _threadFactory;
        private readonly ThreadLocal<bool> _isCurrentThreadProcessingTask = new ThreadLocal<bool>(() => false);

        private static readonly Lazy<TaskScheduler> _singleSTAThreadTaskSchedulerInstance = new Lazy<TaskScheduler>(() => new LimitedConcurrencyLevelSTATaskScheduler(1), true);
        internal static TaskScheduler SingleSTAThreadTaskScheduler => _singleSTAThreadTaskSchedulerInstance.Value;

        public LimitedConcurrencyLevelSTATaskScheduler(int maximumConcurrencyLevel) {
            ThrowIf.Argument.IsOutOfRange(maximumConcurrencyLevel, nameof(maximumConcurrencyLevel), 1, Int32.MaxValue);

            _numberOfCurrentlyRunningThreads = 0;
            _tasks = new LinkedList<Task>();
            _threadFactory = new STAThreadFactory();
            MaximumConcurrencyLevel = maximumConcurrencyLevel;
        }

        public override int MaximumConcurrencyLevel { get; }

        protected override void QueueTask(Task task) {
            ThrowIf.Argument.IsNull(task, nameof(task));

            lock (_lockObject) {
                _tasks.AddLast(task);

                if (_numberOfCurrentlyRunningThreads < MaximumConcurrencyLevel) {
                    ++_numberOfCurrentlyRunningThreads;
                    _threadFactory.Create(ExecuteWork).Start();
                }
            }
        }

        protected override bool TryDequeue(Task task) {
            if (Monitor.TryEnter(_lockObject)) {
                try {
                    return _tasks.Remove(task);
                }
                finally {
                    Monitor.Exit(_lockObject);
                }
            }
            return false;
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
            lock (_lockObject) {
                return _tasks.ToArray(_tasks.Count);
            }
        }

        private void ExecuteWork() {
            _isCurrentThreadProcessingTask.Value = true;
            try {
                while (true) {

                    Task queuedTask;
                    lock (_lockObject) {

                        if (_tasks.Count < 1) {
                            --_numberOfCurrentlyRunningThreads;
                            break;
                        }

                        queuedTask = _tasks.First.Value;
                        _tasks.RemoveFirst();
                    }

                    TryExecuteTask(queuedTask);
                }
            } finally {
                _isCurrentThreadProcessingTask.Value = false;
            }
        }
    }
}