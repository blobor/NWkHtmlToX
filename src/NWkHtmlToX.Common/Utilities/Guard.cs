using System;

namespace NWkHtmlToX.Common.Utilities {
    internal static class Guard {
        internal static void ArgumentNotNull<T>(T value, string argumentName) {
            if(value == null) throw new ArgumentNullException(argumentName);
        }

        internal static void ArgumentNotNullOrEmpty(string value, string argumentName) {
            if (value == null) throw new ArgumentNullException(argumentName);
            if (value.Length == 0) throw new ArgumentException(String.Format("{0} cannot be empty.", argumentName), argumentName);
        }
    }
}