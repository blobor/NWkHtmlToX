using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using NWkHtmlToX.Common.Interop.Windows;
using NWkHtmlToX.Common.SafeHandles;

namespace NWkHtmlToX.Common.Native.Win32 {
    internal class WindowsLibraryLoader : ILibraryLoader {

        public SafeLibraryHandle LoadLibrary(string dllPath) {
            
            if (dllPath == null)
                throw new ArgumentNullException(nameof(dllPath));
            if (!File.Exists(dllPath))
                throw new FileNotFoundException("Could not locate library.", dllPath);
            
            var handler = Interlop.Kernel32.LoadLibrary(dllPath);

            if (handler.IsInvalid) {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            return handler;
        }

        public bool FreeLibrary(SafeLibraryHandle handle) {
            if (handle.IsInvalid || handle.IsClosed) return false;

            handle.Dispose();
            return handle.IsClosed;
        }

        public IntPtr GetProcAddress(SafeLibraryHandle handle, string procedureName) {
            if (handle.IsInvalid || handle.IsClosed)
                throw new ArgumentException("Invalid library handle.", nameof(handle));
            if (procedureName == null)
                throw new ArgumentNullException(nameof(procedureName));

            var procedure = Interlop.Kernel32.GetProcAddress(handle, procedureName);

            if (procedure == IntPtr.Zero) {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            return procedure;
        }
    }
}