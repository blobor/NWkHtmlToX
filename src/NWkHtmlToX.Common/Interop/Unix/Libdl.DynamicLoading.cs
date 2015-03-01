using System;
using System.Runtime.InteropServices;

namespace NWkHtmlToX.Common.Interop.Unix {
    internal partial class Interlop {
        internal partial class Libdl {

            [DllImport(Libraries.Libdl, EntryPoint = "dlopen", ExactSpelling = true, SetLastError = true, CharSet = CharSet.Ansi)]
            internal static extern IntPtr LoadLibrary(string fileName, int flags);

            [DllImport(Libraries.Libdl, EntryPoint = "dlclose", ExactSpelling = true, SetLastError = true)]
            internal static extern int FreeLibrary(IntPtr handle);

            [DllImport(Libraries.Libdl, EntryPoint = "dlsym", ExactSpelling = true, SetLastError = true, CharSet = CharSet.Ansi)]
            internal static extern IntPtr GetProcAddress(IntPtr handle, string symbol);
        }
    }
}