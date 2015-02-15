using System;

namespace NWkHtmlToX.Core.Native {
    public interface ILibraryLoader {

        IntPtr LoadLibrary(string dllPath);

        bool FreeLibrary(IntPtr handle);

        IntPtr GetProcAddress(IntPtr handle, string procedureName);
    }
}