using System;
using NWkHtmlToX.Common.SafeHandles;

namespace NWkHtmlToX.Common.Native {
    internal interface ILibraryLoader {

        SafeLibraryHandle LoadLibrary(string dllPath);

        bool FreeLibrary(SafeLibraryHandle handle);

        IntPtr GetProcAddress(SafeLibraryHandle handle, string procedureName);
    }
}