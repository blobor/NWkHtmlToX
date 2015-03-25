using System;
using System.IO;
using NWkHtmlToX.Common.Interop.Windows;
using NWkHtmlToX.Common.SafeHandles;
using NWkHtmlToX.Common.Utilities;

namespace NWkHtmlToX.Common.Native.Win32 {
    internal sealed class WindowsLibraryLoader : ILibraryLoader {

        public SafeLibraryHandle LoadLibrary(string dllPath) {
            ThrowIf.Argument.IsNull(dllPath, nameof(dllPath));
            if (!File.Exists(dllPath)) throw new FileNotFoundException("Could not locate library.", dllPath);
            
            var handle = Interlop.Kernel32.LoadLibrary(dllPath);
            ThrowIf.Handle.IsFailedToLoad(handle);

            return handle;
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
            ThrowIf.Argument.IsInvalidOrClosedHandle(handle, nameof(handle));

            var procedure = Interlop.Kernel32.GetProcAddress(handle, procedureName);

            ThrowIf.Handle.IsFailedToLoad(procedure);

            return procedure;
        }

        public bool TryGetProcAddress(SafeLibraryHandle handle, string procedureName, out IntPtr procedure) {
            ThrowIf.Argument.IsNull(handle, nameof(handle));
            ThrowIf.Argument.IsNullOrEmpty(procedureName, nameof(procedureName));
            ThrowIf.Argument.IsInvalidOrClosedHandle(handle, nameof(handle));

            procedure = Interlop.Kernel32.GetProcAddress(handle, procedureName);

            return procedure != IntPtr.Zero;
        }
    }
}