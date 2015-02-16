using System;
using System.Runtime.InteropServices;
namespace NWkHtmlToX.Core.Native.Win32 {

#if !ASPNETCORE50
    [System.Security.SuppressUnmanagedCodeSecurity]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
#endif
    public static class WindowsApi {

        private const string KERNEL_DLL_PATH = "kernel32";

        [DllImport(KERNEL_DLL_PATH, EntryPoint = "LoadLibrary", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)]string dllToLoad);

        [DllImport(KERNEL_DLL_PATH, EntryPoint = "FreeLibrary", SetLastError = true)]
        public static extern bool FreeLibrary(IntPtr hModule);

        [DllImport(KERNEL_DLL_PATH, EntryPoint = "GetProcAddress", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);
    }
}