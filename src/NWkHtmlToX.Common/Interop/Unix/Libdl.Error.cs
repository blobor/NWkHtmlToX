using System;
using System.Runtime.InteropServices;

namespace NWkHtmlToX.Common.Interop.Unix {
    internal partial class Interlop {
        internal partial class Libdl {

            [DllImport(Libraries.Libdl, EntryPoint = "dlerror", ExactSpelling = true, SetLastError = true)]
            internal static extern IntPtr GetLastError();
        }
    }
}