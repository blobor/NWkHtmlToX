using System;
using System.Runtime.InteropServices;

namespace NWkHtmlToX.Core.Native.PlatformApi {

#if !ASPNETCORE50
    [System.Security.SuppressUnmanagedCodeSecurity]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
#endif
    public static class UnixApi {

        private const string LIBDL_DLL_PATH = "libdl";

        [DllImport(LIBDL_DLL_PATH, EntryPoint = "dlopen", ExactSpelling = true, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern IntPtr LoadLibrary(string fileName, int flags);

        [DllImport(LIBDL_DLL_PATH, EntryPoint = "dlclose", ExactSpelling = true, SetLastError = true)]
        public static extern int FreeLibrary(IntPtr handle);

        [DllImport(LIBDL_DLL_PATH, EntryPoint = "dlsym", ExactSpelling = true, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern IntPtr GetProcAddress(IntPtr handle, string symbol);

        [DllImport(LIBDL_DLL_PATH, EntryPoint = "dlerror", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetLastLibdlError();
    }
}