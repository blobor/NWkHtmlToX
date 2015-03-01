using Microsoft.Win32.SafeHandles;

namespace NWkHtmlToX.Common.SafeHandles {
    internal abstract class SafeLibraryHandle : SafeHandleZeroOrMinusOneIsInvalid {
        protected SafeLibraryHandle() : base(true) {
        }
    }
}