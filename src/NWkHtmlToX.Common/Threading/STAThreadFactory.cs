using System;
using System.Threading;
using NWkHtmlToX.Common.Utilities;

namespace NWkHtmlToX.Common.Threading {
    internal sealed class STAThreadFactory : IThreadFactory {
        public Thread Create(Action start) {
            Guard.ArgumentNotNull(start, nameof(start));

            var thread = new Thread(start.Invoke) {
                IsBackground = true
            };

            thread.SetApartmentState(ApartmentState.STA);

            return thread;
        }

        public Thread Create(Action<object> start) {
            Guard.ArgumentNotNull(start, nameof(start));

            var thread = new Thread(start.Invoke) {
                IsBackground = true
            };

            thread.SetApartmentState(ApartmentState.STA);

            return thread;
        }
    }
}