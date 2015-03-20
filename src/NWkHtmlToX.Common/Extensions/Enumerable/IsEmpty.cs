using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NWkHtmlToX.Common.Extensions.Enumerable {
    internal static partial class EnumerableExtensions {

        internal static bool IsEmpty<T>(this IEnumerable<T> source) {
            return !source.Any();
        }

        internal static bool IsEmpty(this ICollection source) {
            return source.Count < 1;
        }

        internal static bool IsEmpty<T>(this ICollection<T> source) {
            return source.Count < 1;
        }
    }
}