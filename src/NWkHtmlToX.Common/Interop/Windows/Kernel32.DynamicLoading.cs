using System;
using System.Runtime.InteropServices;

namespace NWkHtmlToX {
    internal partial class Interlop {
        internal partial class Kernel32 {

            [DllImport(Libraries.Kernel32, EntryPoint = "LoadLibrary", SetLastError = true, CharSet = CharSet.Ansi)]
            internal static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)]string dllToLoad);

            [DllImport(Libraries.Kernel32, EntryPoint = "FreeLibrary", SetLastError = true)]
            internal static extern bool FreeLibrary(IntPtr hModule);

            [DllImport(Libraries.Kernel32, EntryPoint = "GetProcAddress", SetLastError = true, CharSet = CharSet.Ansi)]
            internal static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);
        }
    }
}