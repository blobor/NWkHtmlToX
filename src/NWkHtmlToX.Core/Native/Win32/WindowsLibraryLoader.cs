﻿using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;

namespace NWkHtmlToX.Core.Native.Win32 {
    public class WindowsLibraryLoader : ILibraryLoader {

        public IntPtr LoadLibrary(string dllPath) {
            if (dllPath == null)
                throw new ArgumentNullException(nameof(dllPath));
            if (!File.Exists(dllPath))
                throw new FileNotFoundException("Could not locate library.", dllPath);

            var handler = WindowsApi.LoadLibrary(dllPath);

            if (handler == IntPtr.Zero) {
                throw new Win32Exception(Marshal.GetLastWin32Error());
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

            if (procedure == IntPtr.Zero) {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            return procedure;
        }
    }
}