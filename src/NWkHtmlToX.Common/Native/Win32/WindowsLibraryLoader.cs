using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using NWkHtmlToX.Common.Interop.Windows;
using NWkHtmlToX.Common.SafeHandles;
using NWkHtmlToX.Common.Utilities;

namespace NWkHtmlToX.Common.Native.Win32 {
    internal sealed class WindowsLibraryLoader : ILibraryLoader {

        public SafeLibraryHandle LoadLibrary(string dllPath) {
            ThrowIf.Argument.IsNull(dllPath, nameof(dllPath));
            if (!File.Exists(dllPath)) throw new FileNotFoundException("Could not locate library.", dllPath);
            
            var handler = Interlop.Kernel32.LoadLibrary(dllPath);

            if (handler.IsInvalid) {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            return handler;
        }

        public bool FreeLibrary(SafeLibraryHandle handle) {
            ThrowIf.Argument.IsNull(handle, nameof(handle));
            if (handle.IsInvalid || handle.IsClosed) return false;

            handle.Dispose();
            return handle.IsClosed;
        }

        public IntPtr GetProcAddress(SafeLibraryHandle handle, string procedureName) {
            ThrowIf.Argument.IsNull(handle, nameof(handle));
            ThrowIf.Argument.IsNullOrEmpty(procedureName, nameof(procedureName));
            if (handle.IsInvalid || handle.IsClosed) throw new ArgumentException("Invalid library handle.", nameof(handle));

            var procedure = Interlop.Kernel32.GetProcAddress(handle, procedureName);

            if (procedure == IntPtr.Zero) {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            return procedure;
        }

        public bool TryGetProcAddress(SafeLibraryHandle handle, string procedureName, out IntPtr procedure) {
            ThrowIf.Argument.IsNull(handle, nameof(handle));
            ThrowIf.Argument.IsNullOrEmpty(procedureName, nameof(procedureName));
            if (handle.IsInvalid || handle.IsClosed) throw new ArgumentException("Invalid library handle.", nameof(handle));

            procedure = Interlop.Kernel32.GetProcAddress(handle, procedureName);

            return procedure != IntPtr.Zero;
        }
    }
}