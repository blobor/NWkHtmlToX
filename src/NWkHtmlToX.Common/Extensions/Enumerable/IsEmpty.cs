using System.Collections.Generic;
using System.Linq;

namespace NWkHtmlToX.Common.Extensions.Enumerable {
    internal static partial class EnumerableExtensions {

        internal static bool IsEmpty<T>(this IEnumerable<T> source) {
            return !source.Any();
        }
    }
}