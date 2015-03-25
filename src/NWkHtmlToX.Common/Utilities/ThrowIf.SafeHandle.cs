using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace NWkHtmlToX.Common.Utilities {
    internal static partial class ThrowIf {
        internal static class Handle {
            internal static void IsFailedToLoad(SafeHandle safeHandle) {
                if(safeHandle.IsInvalid) throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            internal static void IsFailedToLoad(IntPtr handle) {
                if (handle == IntPtr.Zero) throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }
    }
}