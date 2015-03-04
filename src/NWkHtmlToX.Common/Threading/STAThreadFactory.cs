using System;
using System.Threading;

namespace NWkHtmlToX.Common.Threading {
    internal class STAThreadFactory : IThreadFactory {
        public Thread Create(Action start) {
            if (start == null) throw new ArgumentNullException(nameof(start));

            var thread = new Thread(start.Invoke) {
                IsBackground = true
            };

            thread.SetApartmentState(ApartmentState.STA);

            return thread;
        }

        public Thread Create(Action<object> start) {
            if (start == null) throw new ArgumentNullException(nameof(start));

            var thread = new Thread(start.Invoke) {
                IsBackground = true
            };

            thread.SetApartmentState(ApartmentState.STA);

            return thread;
        }
    }
}