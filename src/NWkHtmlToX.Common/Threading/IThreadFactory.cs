using System;
using System.Threading;

namespace NWkHtmlToX.Common.Threading {
    internal interface IThreadFactory {
        Thread Create(Action start);

        Thread Create(Action<object> start);

        Thread Create<T>(Action<T> start);
    }
}