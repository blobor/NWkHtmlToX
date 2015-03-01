using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using NWkHtmlToX.Common.SafeHandles;

namespace NWkHtmlToX.Common.Interop.Windows {
    internal class Interlop {
        internal class Kernel32 {

            [DllImport(Libraries.Kernel32, EntryPoint = "LoadLibrary", SetLastError = true, CharSet = CharSet.Ansi)]
            internal static extern SafeWindowsLibraryHandle LoadLibrary([MarshalAs(UnmanagedType.LPStr)]string dllToLoad);

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            [DllImport(Libraries.Kernel32, EntryPoint = "FreeLibrary", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool FreeLibrary(IntPtr hModule);

            [DllImport(Libraries.Kernel32, EntryPoint = "GetProcAddress", ExactSpelling = true, SetLastError = true, CharSet = CharSet.Ansi)]
            internal static extern IntPtr GetProcAddress(SafeLibraryHandle hModule, string procedureName);
        }
    }
}