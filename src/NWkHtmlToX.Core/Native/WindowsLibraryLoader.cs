using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using NWkHtmlToX.Core.Native.PlatformApi;

namespace NWkHtmlToX.Core.Native {
    public class WindowsLibraryLoader : ILibraryLoader {

        public IntPtr LoadLibrary(string dllPath) {
            if (dllPath == null)
                throw new ArgumentNullException(nameof(dllPath));
            if (!File.Exists(dllPath))
                throw new FileNotFoundException("Could not locate library.", dllPath);

            var handler = WindowsApi.LoadLibrary(dllPath);

            if (handler == IntPtr.Zero) {
                var errorCode = Marshal.GetLastWin32Error();
                var message = String.Format("Could not load library. Error code: {0}", errorCode.ToString(NumberFormatInfo.InvariantInfo));

                throw new InvalidOperationException(message);
            }

            return handler;
        }

        public bool FreeLibrary(IntPtr handle) => handle != IntPtr.Zero && WindowsApi.FreeLibrary(handle);

        public IntPtr GetProcAddress(IntPtr handle, string procedureName) {
            if (handle == IntPtr.Zero)
                throw new ArgumentException("Invalid library handle.", nameof(handle));
            if (procedureName == null)
                throw new ArgumentNullException(nameof(procedureName));

            var procedure = WindowsApi.GetProcAddress(handle, procedureName);

            if(procedure == IntPtr.Zero) {
                var errorCode = Marshal.GetLastWin32Error();
                var message = String.Format("Could not find procedure. Error code: {0}", errorCode.ToString(NumberFormatInfo.InvariantInfo));

                throw new InvalidOperationException(message);
            }

            return procedure;
        }
    }
}