using NWkHtmlToX.Common.Interop.Windows;

namespace NWkHtmlToX.Common.SafeHandles {
    internal sealed class SafeWindowsLibraryHandle : SafeLibraryHandle {
        protected override bool ReleaseHandle() {
            return Interlop.Kernel32.FreeLibrary(handle);
        }
    }
}