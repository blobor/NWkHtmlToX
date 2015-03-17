﻿using System;
using System.Threading;
using NWkHtmlToX.Common.Utilities;

namespace NWkHtmlToX.Common.Threading {
    internal sealed class STAThreadFactory : IThreadFactory {
        public Thread Create(Action start) {
            ThrowIf.Argument.IsNull(start, nameof(start));

            var thread = new Thread(start.Invoke) {
                IsBackground = true
            };

            thread.SetApartmentState(ApartmentState.STA);

            return thread;
        }

        public Thread Create<T>(Action<T> start) {
            return Create(obj => start((T) obj));
        }

        public Thread Create(Action<object> start) {
            ThrowIf.Argument.IsNull(start, nameof(start));

            var thread = new Thread(start.Invoke) {
                IsBackground = true
            };

            thread.SetApartmentState(ApartmentState.STA);

            return thread;
        }
    }
}