using System;
using System.Threading.Tasks;

namespace NWkHtmlToX.Common.Threading {
    internal class SingleSTAThreadTaskScheduler {
        private static readonly Lazy<TaskScheduler> _instance = new Lazy<TaskScheduler>(() => new LimitedConcurrencyLevelSTATaskScheduler(1), true);
        public static TaskScheduler Instance => _instance.Value;
    }
}