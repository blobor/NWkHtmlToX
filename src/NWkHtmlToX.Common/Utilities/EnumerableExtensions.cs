using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NWkHtmlToX.Common.Utilities {
    internal static class EnumerableExtensions {

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        internal static T[] ToArray<T>(this IEnumerable<T> source, int count) {
            ThrowIf.Argument.IsNull(source, nameof(source));
            ThrowIf.Argument.IsOutOfRange(count, nameof(count), 0, Int32.MaxValue);

            var index = 0;
            var result = new T[count];
            foreach (var item in source) {
                result[index++] = item;
            }
            return result;
        }
    }
}