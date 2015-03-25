﻿using System;
using System.Runtime.InteropServices;

namespace NWkHtmlToX.Common.Utilities {
    internal static partial class ThrowIf {
        internal static class Argument {
            internal static void IsNull<T>(T argument, string argumentName) {
                if (argument == null) throw new ArgumentNullException(argumentName);
            }

            internal static void IsNullOrEmpty(string argument, string argumentName) {
                if (argument == null) throw new ArgumentNullException(argumentName);
                if (argument.Length == 0) throw new ArgumentException(String.Format("{0} cannot be empty.", argumentName), argumentName);
            }

            internal static void IsOutOfRange<T>(T argument, string argumentName, T minValue, T maxValue) where T : IComparable<T> {
                if (argument.CompareTo(minValue) < 0 || argument.CompareTo(maxValue) > 0) {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            internal static void IsInvalidHandle(SafeHandle handle, string argumentName) {
                if(handle.IsInvalid) throw new ArgumentException("Invalid handle.", argumentName);
            }

            internal static void IsInvalidOrClosedHandle(SafeHandle handle, string argumentName) {
                IsInvalidHandle(handle, argumentName);
                if(handle.IsClosed) throw new ArgumentException("Handle is already closed.", argumentName);
            }
        }
    }
}