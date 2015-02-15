using System;
using System.Globalization;
using System.IO;
using NWkHtmlToX.Core.Native;
using Xunit;

namespace NWkHtmlToX.Core.Tests.Native {
    public class WindowsLibraryLoaderTests {

        private readonly ILibraryLoader _windowsLibraryLoader;

        public WindowsLibraryLoaderTests() {
            _windowsLibraryLoader = new WindowsLibraryLoader();
        }

        [Fact]
        public void LoadLibraryMethodShoudThrowArgumentNullExceptionOnNullArgument() {
            // Arrange
            string testArgument = null;
            Action testCode = () => _windowsLibraryLoader.LoadLibrary(testArgument);

            // Act, Assert
            Assert.Throws<ArgumentNullException>(testCode);
        }

        [Fact]
        public void LoadLibraryMethodShoudThrowFileNotFoundExceptionIfLibraryWasNotFound() {
            // Arrange
            var testArgument = String.Format(@"C:\NotExistingFile_{0}.dll", DateTime.Now.Ticks.ToString(NumberFormatInfo.InvariantInfo));
            Action testCode = () => _windowsLibraryLoader.LoadLibrary(testArgument);

            // Act, Assert
            Assert.Throws<FileNotFoundException>(testCode);
        }

        [Fact]
        public void GetProcAddressMethodShoudThrowArgumentExceptionOnInvalidHandle() {
            // Arrange
            var handle = IntPtr.Zero;
            var procedureName = String.Empty;
            Action testCode = () => _windowsLibraryLoader.GetProcAddress(handle, procedureName);

            // Act, Assert
            Assert.Throws<ArgumentException>(nameof(handle), testCode);
        }

        [Fact]
        public void GetProcAddressMethodShoudThrowArgumentNullExceptionOnNullProcedureNameArgument() {
            // Arrange
            var handle = new IntPtr(int.MinValue);
            string procedureName = null;
            Action testCode = () => _windowsLibraryLoader.GetProcAddress(handle, procedureName);

            // Act, Assert
            Assert.Throws<ArgumentNullException>(testCode);
        }
    }
}